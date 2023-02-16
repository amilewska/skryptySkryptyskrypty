using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody playerRb;
    public AudioSource movingSource;
    public AudioClip movingSound;
    bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        movingSource = GetComponent<AudioSource>();
        GameManager.Instance.audioEffects.Add(movingSource);

    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        //move the box
        if (Input.GetKey(KeyCode.A))
        {
            playerRb.AddForce(-Vector3.forward * speed);
        }
       

        if (Input.GetKey(KeyCode.D))
        {
            playerRb.AddForce(Vector3.forward * speed);
        }

        PlaySoundOnMoving();



    }

    void PlaySoundOnMoving()
    {
        if (playerRb.velocity.magnitude > 0.3f)
        {
            isMoving = true;
        }
        else isMoving = false;

        if (isMoving)
        {
            if (!movingSource.isPlaying)
            {

                movingSource.PlayOneShot(movingSound);
                //StartCoroutine(StartFade(movingSource, 1, 1));

            }
        }
        else
        {
            if (movingSource.isPlaying)
            {
                movingSource.Stop();
                //StartCoroutine(StartFade(movingSource, 2, 0));  
            }
        }


    }

    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
        
    }
}


    

