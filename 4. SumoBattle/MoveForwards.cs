using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    public float speed = 10;
    public float forceStrength = 20;
    public GameObject enemy;
    public ParticleSystem smoke;
    private float aliveTimer = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (enemy !=null)
        {
            transform.LookAt(enemy.transform.position);
            //transform.Translate(Vector3.forward*speed*Time.deltaTime);
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            //GetComponent<Rigidbody>().AddForce(direction * speed);
            Destroy(gameObject, aliveTimer);
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (enemy !=null)
        {
            if (collision.gameObject == enemy)
            {
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
                enemy.GetComponent<Rigidbody>().AddForce(awayFromPlayer * forceStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }

        
        
    }
}
