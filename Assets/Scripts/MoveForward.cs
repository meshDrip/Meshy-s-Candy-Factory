using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] float conveyorSpeed = 50f;

    [SerializeField] bool moving = false;
    Rigidbody targetObject;

    ContainerManager cm;

    private void Start()
    {
        cm = FindObjectOfType<ContainerManager>();
    }


    private void OnTriggerStay(Collider other)
    {
        print("Container detected by conveyor.");
        if (cm.activeBaskets != null)
        {
            for (int i = 0; i < cm.activeBaskets.Length; i++)
            {
                if(cm.activeBaskets[i] != null)
                {
                    if (other.gameObject == cm.activeBaskets[i].gameObject)
                    {
                        targetObject = other.GetComponent<Rigidbody>();
                        print("Found " + cm.activeBaskets[i].gameObject.name);
                        moving = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targetObject = null;
        moving = false;
    }

    private void FixedUpdate()
    {
        if (moving == true && targetObject != null)
        {
            Mover(targetObject);
        }
    }

    private void Mover(Rigidbody target)
    {
        target.gameObject.transform.Translate(conveyorSpeed * Time.deltaTime, 0, 0, Space.World);
    }
}
