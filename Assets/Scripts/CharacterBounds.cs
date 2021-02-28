using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBounds : MonoBehaviour
{
    CharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CandyOne")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision.collider);
        }
        if (collision.gameObject.tag == "CandyTwo")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision.collider);
        }
        if (collision.gameObject.tag == "CandyThree")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision.collider);
        }
        else if (collision.gameObject.tag == "Container")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision.collider);
        }
    }
}
