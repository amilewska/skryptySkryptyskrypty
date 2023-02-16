using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Private variables
    [SerializeField] private float horsePower = 10;
    private float turnSpeed = 100.0f;
    [SerializeField] private float speed;
    private float horizontalInput;
    private float verticalInput;
    public KeyCode cameraSwitch;
    public Camera mainCamera;
    public Camera seatCamera;
    private Rigidbody rb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float rpm;
    [SerializeField] List<WheelCollider> allWheels;
    int wheelsOnGround;

    void Awake()
    {
        //must give each wheel a little torque or the wheel colliders will be stuck/not work properly
        foreach (WheelCollider w in allWheels)
            w.motorTorque = 0.000001f;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //This is where we get player input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(cameraSwitch))
        {
            mainCamera.enabled = !mainCamera.enabled;
            seatCamera.enabled = !seatCamera.enabled;
        }

        if (IsGrounded())
        {
            //Move the vehicle forward based on vertical input
            rb.AddRelativeForce(Vector3.forward * verticalInput * horsePower);

            //Rotate the vehicle based on horizontal input
            transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turnSpeed);

            //print the speed
            speed = Mathf.Round(rb.velocity.magnitude * 3.6f);
            speedText.SetText("Speed: " + speed);

            //print the rpm
            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);
        }
        
        
        
    }

    bool IsGrounded()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if(wheel.isGrounded) wheelsOnGround++;
        }

        if (wheelsOnGround == 4) return true;
        else return false;
    }
}
