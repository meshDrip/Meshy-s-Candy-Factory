using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    Pickup pickup;

    bool collidingWithRoom = false;

    private void Start()
    {
        pickup = GetComponent<Pickup>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
            other.transform.parent = gameObject.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Room") && !pickup.firstPickup)
        {
            print("Destroying crate!");
            pickup.ReleaseObject();
            StartCoroutine(BasketDestruction());
            collidingWithRoom = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collidingWithRoom = false;
    }

    IEnumerator BasketDestruction()
    {
        yield return new WaitForSeconds(3);
        if (collidingWithRoom == true)
        {
            Destroy(gameObject);
        }
    }
}
