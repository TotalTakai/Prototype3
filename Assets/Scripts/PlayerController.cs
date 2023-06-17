using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private float jumpForce = 700.0f;
    private float topBoundary = 6;
    private float playerScore = 0;
    private float walkSpawnSpeed = 0.01f;
    private bool isOnGround = true;
    private int jumpCounter = 0;
    private Vector3 startingPos;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float gravityModifier = 1.0f;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        startingPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        CheckTopBoundary();
        Dash();
        Debug.Log(Mathf.Floor(playerScore));
        WalkToSpawn();
    }

    // Player will jump when space bar is pressed - he can double jump in the air
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < 2 && !gameOver)
        {
            rigidRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            jumpCounter++;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound);
            isOnGround = false;
        }
        else if (isOnGround) jumpCounter = 0;
    }

    // Checks if the player is grounded and if he hit an obstacle
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (!gameOver)
            {
                dirtParticle.Play();
            }
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!gameOver)
            {
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSound);
            }
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            dirtParticle.Stop();
        }
    }

    // Checks that the player doesn't go over the top boundary
    private void CheckTopBoundary()
    {
        if(transform.position.y > topBoundary)
        {
            transform.position = new Vector3(transform.position.x, topBoundary, transform.position.z);
        }
    }

    // Checks if the player dashes or not, and keeps the total game score
    public bool Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !gameOver)
        {
            playerScore += Time.deltaTime * 2;
            return true;
        }
        else if (!gameOver)
        {
            playerScore += Time.deltaTime;
            return false;
        }
        else return false;
    }


    private void WalkToSpawn()
    {
        if (transform.position.x != startingPos.x) 
            transform.position = Vector3.MoveTowards(transform.position, startingPos, walkSpawnSpeed);
        else playerAnim.SetFloat("Speed_f", 1);
    }
}
