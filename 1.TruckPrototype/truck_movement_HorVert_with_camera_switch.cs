using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Private variables
    private float speed = 20.0f;
    private float turnSpeed = 100.0f;
    private float horizontalInput;
    private float verticalInput;
    public Camera mainCamera;
    public Camera seatCamera;
    public KeyCode cameraSwitch;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This is where we get player input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(cameraSwitch))
        {
            mainCamera.enabled = !mainCamera.enabled;
            seatCamera.enabled = !seatCamera.enabled;
        }

        //Move the vehicle forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        //Rotate the vehicle based on horizontal input
        transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turnSpeed);
        
    }
}
