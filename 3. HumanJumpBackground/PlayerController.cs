using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource soundEffect;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip crashSound;
    public AudioClip jumpSound;

    public float jumpForce = 600;
    public float doubleJumpForce = 500;
    public float physicsModifier;
    public bool isOnGround = true;
    public bool isGameOver = false;
    public bool canDoubleJump;
    public bool pressedShift;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= physicsModifier;
        playerAnim = GetComponent<Animator>();
        soundEffect = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGameOver && isOnGround )
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDoubleJump = true;
            isOnGround = false;
            playerAnim.Play("Running_Jump", -1, 0.0f);
            soundEffect.PlayOneShot(jumpSound);
            dirtParticle.Stop();
        }
            //double jump
        else if (Input.GetKeyDown(KeyCode.Space) && !isGameOver && canDoubleJump)
        {
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            canDoubleJump = false;
            isOnGround = false;
            playerAnim.Play("Running_Jump", -1, 0.0f);
            soundEffect.PlayOneShot(jumpSound);
        }
            
         
        //faster animation running
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isGameOver)
        {
            pressedShift = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isGameOver) 
        {
            pressedShift = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("Gameover");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int",1);
            explosionParticle.Play();
            dirtParticle.Stop();
            soundEffect.PlayOneShot(crashSound);

        }
    }
}
