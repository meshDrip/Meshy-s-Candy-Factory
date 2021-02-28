using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    float mouseX;
    float mouseY;
    float xRotation = 0; 
    [SerializeField] float mouseRange = 200;

    [SerializeField] Transform playerBody;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameActive)
        {
            LookX();
            LookY();
        }
    }

    private void LookX()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseRange * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);
    }
    private void LookY()
    {
        mouseY = Input.GetAxis("Mouse Y") * mouseRange * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
