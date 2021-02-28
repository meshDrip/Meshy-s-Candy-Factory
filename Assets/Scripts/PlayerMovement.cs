using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float x;
    float z;
    [SerializeField] float minXBound;
    [SerializeField] float minZBound;
    [SerializeField] float maxXBound;
    [SerializeField] float maxZBound;
    float playerXVal;
    float playerZVal;

    [SerializeField] float speed = 10;
    [SerializeField] float stopSpeed = 0;

    [SerializeField] CharacterController player;

    GameManager gm;

    public bool stop;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gm.gameActive)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            playerXVal = player.transform.position.x;
            playerZVal = player.transform.position.z;
            Vector3 move = transform.right * x + transform.forward * z;
            player.SimpleMove(move * speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        //LimitMovement();
    }

    //private void LimitMovement()
    //{
    //    if (minXBounds(playerXVal) == true)
    //    {
    //        transform.position = new Vector3(minXBound, transform.position.y, transform.position.z);
    //    }
    //    else if (maxXBounds(playerXVal) == true)
    //    {
    //        transform.position = new Vector3(maxXBound, transform.position.y, transform.position.z);
    //    }
    //    else if (minZBounds(playerZVal) == true)
    //    {
    //        transform.position = new Vector3(transform.position.x, transform.position.y, minZBound);
    //    }
    //    else if (minZBounds(playerZVal) == true)
    //    {
    //        transform.position = new Vector3(transform.position.x, transform.position.y, maxZBound);
    //    }
    //}

    //bool minXBounds(float x)
    //{
    //    if (x < minXBound)
    //    {
    //        return true;
    //    }
    //    else return false;
    //}
    //bool maxXBounds(float x)
    //{
    //    if (x > minXBound)
    //    {
    //        return true;
    //    }
    //    else return false;
    //}
    //bool minZBounds(float z)
    //{
    //    if (z < minZBound)
    //    {
    //        return true;
    //    }
    //    else return false;
    //}
    //bool maxZBounds(float z)
    //{
    //    if (z > minZBound)
    //    {
    //        return true;
    //    }
    //    else return false;
    //}

}
