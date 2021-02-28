using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawner : MonoBehaviour
{
    [SerializeField] float candyAmount = 15;
    [SerializeField] GameObject[] candyList;
    [SerializeField] GameObject candy;
    Vector3 randomPoint;
    Vector3 offset;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DispenseCandy()
    {
        if (gameManager.gameActive)
        {
            StartCoroutine(CandyDelay());
        }
    }

    public IEnumerator CandyDelay()
    {
        candy = candyList[Random.Range(0, 6)];
        for (int i = 0; i < candyAmount; i++)
        {
            Instantiate(candy, new Vector3(transform.position.x - Random.Range(0.2f, 0.3f), transform.position.y, transform.position.z - Random.Range(0.4f, 0.5f)), Quaternion.identity);
            yield return new WaitForSeconds(.1f);
        }
    }

    private Vector3 RandomSpot(Vector3 target)
    {
        offset = new Vector3(target.x - Random.Range(0.01f, 0.03f), target.y, target.z - Random.Range(0.01f, 0.03f));
        randomPoint = target - offset;
        return randomPoint;
    }
}
