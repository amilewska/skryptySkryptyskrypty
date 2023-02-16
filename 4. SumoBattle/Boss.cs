using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject minionsPrefab;
    public float speed = 3.0f;

    private Rigidbody bossRb;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        bossRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        InvokeRepeating("SpawnMinions", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            bossRb.AddForce(lookDirection * speed);
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }


    }

    void SpawnMinions()
    {
        Vector3 offset = Vector3.forward * 3;
        Instantiate(minionsPrefab, transform.position + offset, minionsPrefab.transform.rotation);
    }
}
