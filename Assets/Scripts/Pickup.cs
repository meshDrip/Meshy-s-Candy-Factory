using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Transform dest;

    [SerializeField] GameObject containerToSpawn;
    [SerializeField] Transform containerSpawnLocation;

    ContainerManager cm;

    

    bool isPickedUp;
    public bool firstPickup = true;

    private void Start()
    {
        cm = FindObjectOfType<ContainerManager>();
        dest = GameObject.Find("PickupDestination").transform;
        containerSpawnLocation = GameObject.Find("ContainerSpawn").transform;
        containerToSpawn = cm.containerToInstantiate;
    }

    private void OnMouseDown()
    {
        //GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isPickedUp = true;
        transform.parent = GameObject.Find("PickupDestination").transform;
    }

    private void OnMouseUp()
    {
        ReleaseObject();
        //GetComponent<BoxCollider>().enabled = true;
    }

    public void ReleaseObject()
    {
        GetComponent<Rigidbody>().useGravity = true;

        GetComponent<Rigidbody>().isKinematic = false;
        isPickedUp = false;
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        if (isPickedUp)
        {
            transform.position = dest.position;
            if (firstPickup)
            {
                cm.activeBaskets[0] = gameObject.GetComponent<Basket>();
                StartCoroutine(SpawnNewContainer());
                firstPickup = false;
            }
            else return;
        }
    }

    IEnumerator SpawnNewContainer()
    {
        yield return new WaitForSeconds(3);
        cm.activeBaskets[1] = Instantiate(containerToSpawn, containerSpawnLocation.position, Quaternion.identity).GetComponent<Basket>();
    }
}
