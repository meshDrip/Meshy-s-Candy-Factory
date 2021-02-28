using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] string candyTag;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Basket>())
        {
            foreach (Transform child in other.transform)
            {
                if (child.tag == candyTag)
                {
                    gameManager.score += 1;
                    if(child.tag == "CandyOne")
                    {
                        gameManager.chocolateScore += 1;
                    }
                    if(child.tag == "CandyTwo")
                    {
                        gameManager.sourScore += 1;
                    }
                    else if(child.tag == "CandyThree")
                    {
                        gameManager.sweetScore += 1;
                    }
                }
            }
            gameManager.UpdateScore();
            Destroy(other.gameObject);
            print("Score is " + gameManager.score + ".");
        }
    }
}
