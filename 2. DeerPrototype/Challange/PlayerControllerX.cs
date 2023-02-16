using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float timer = 0;
    private float delay = 1.0f;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.Space)) && (timer > delay))
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            timer = 0.0f;


        }


    }
    
}
