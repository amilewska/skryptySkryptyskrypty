using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    bool isRewinding = false;
    List<PointInTime> pointsInTime;
    Rigidbody rb;
    public float recordTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //when shift is pressed then the balls and light should rewind
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartRewind();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopRewind();
        }

    }

    private void FixedUpdate()
    {
        //when shift is pressed rewind
        if (isRewinding)
        {
            Rewind();
        }
        //when shift isint pressed start record the position (to rewind it in the future)
        else
        {
            Record();
        }
    }

    void Record()
    {
        //check how many secunds is rewind (more than 5 is CPU killin) and remove THE OLDEST element
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }


        //add the current position and velocity to begin of the list (newest items are on the top)
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity, rb.angularVelocity));
    }

    void Rewind()
    {
        //check if there is at least one element in our list
        if (pointsInTime.Count > 0)
        {
            //read the first position of our list
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            rb.velocity = pointInTime.velocity;
            rb.angularVelocity = pointInTime.angularVelocity;
            
            //and remove that element
            pointsInTime.RemoveAt(0);
        }
        else StopRewind();

        
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }

}
