using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] CandySpawner[] candySpawns;
    int spawner;
    [SerializeField]  int spawnerCountMin;
    [SerializeField]  int spawnerCountMax;

    [SerializeField] float waveTimeMin;
    [SerializeField] float waveTimeMax;

    [SerializeField] float duration = 20f;
    [SerializeField] float durationDecay = 0.1f;
    float waveDelayTime = 5f;

    float lightRangeMin;
    float lightRangeMax;
    float flashingSpeed;
    float dispenseCooldown = 0;

    public bool timeRunningOut = false;
    bool doubleWave = false;

    [SerializeField] AudioClip alarmNoise;
    [SerializeField] Light[] alarmLights;
    [SerializeField] AudioSource[] alarmSources;
    [SerializeField] Animator[] lightAnimations;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunningOut)
        {
            print("Time's running out...");
            duration = 1;
            waveDelayTime = waveDelayTime = 1;
            timeRunningOut = false;
            doubleWave = true;
        }
    }

    public void WaveBegin()
    {
        if (gameManager.gameActive)
        {
            duration = RandomTime();
            spawner = RandomSpawner();
            StartCoroutine(StartCandyDispense());
            StartCoroutine(BeginAlarm(spawner));
            if (doubleWave)
            {
                duration = RandomTime();
                spawner = RandomSpawner();
                StartCoroutine(StartCandyDispense());
                StartCoroutine(BeginAlarm(spawner));
            }
        }
    }

    IEnumerator BeginAlarm(int s)
    {
        alarmSources[s].PlayOneShot(alarmNoise, .5f);
        lightAnimations[s].SetBool("Play", true);
        yield return new WaitForSeconds(1);
        alarmSources[s].PlayOneShot(alarmNoise, .5f);
        yield return new WaitForSeconds(waveDelayTime);
        lightAnimations[s].SetBool("Play", false);
    }

    float RandomTime()
    {
        float time;
        time = Random.Range(waveTimeMin, waveTimeMax);
        return time;
    }

    int RandomSpawner()
    {
        int spawner;
        spawner = Random.Range(spawnerCountMin, spawnerCountMax);
        return spawner;
    }

    public IEnumerator StartCandyDispense()
    {
        yield return new WaitForSeconds(duration);
        candySpawns[spawner].DispenseCandy();
        yield return new WaitForSeconds(waveDelayTime);
        WaveBegin();
    }
}
