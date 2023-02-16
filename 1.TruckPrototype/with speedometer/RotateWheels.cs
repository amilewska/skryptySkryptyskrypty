using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour
{
    private float horizontalInput;
    private float turnSpeed = 100.0f;
    private Rigidbody rb;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        angle = horizontalInput * Time.deltaTime * turnSpeed;
        transform.Rotate(Vector3.up, angle);

       /* if (transform.rotation.eulerAngles.y > -45 && transform.rotation.eulerAngles.y < 45)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, -45, transform.position.z);
            transform.rotation = Quaternion.Euler(transform.position.x, 45, transform.position.z);
        }*/




    }
}
