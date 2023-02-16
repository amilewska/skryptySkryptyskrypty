using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    float timer;
    public float forceRotation;
   
    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.Instance.timer <= 10) transform.RotateAround(transform.position, Vector3.right, forceRotation*Time.deltaTime);
        
    }
}
