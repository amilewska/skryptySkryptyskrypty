using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    public bool inTheBox = false;
    [SerializeField] AudioClip collisionSound;
    [SerializeField] AudioClip collisionBoxSound;
    public AudioSource soundEffect;


    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        soundEffect.clip = collisionSound;
        GameManager.Instance.audioEffects.Add(soundEffect);
            
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 0.8f)
        {
            Destroy(GetComponent<TimeBody>());

            if (transform.position.z < -58) Destroy(gameObject);
            if (transform.position.z > 58) Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //when ball fall into the box
        if (other.gameObject.CompareTag("Player") && !inTheBox)
        {
            inTheBox = true;
            GameManager.Instance.AddScore(1);
            Destroy(GetComponent<TimeBody>());
            GetComponent<SphereCollider>().material.bounciness = 0;
            soundEffect.PlayOneShot(collisionBoxSound);
            //Destroy(gameObject);
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) soundEffect.PlayOneShot(collisionSound);
    }



}
