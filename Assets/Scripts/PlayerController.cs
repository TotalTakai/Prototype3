using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidRb;
    private float jumpForce = 10.0f;
    private bool isOnGround = true;
    public float gravityModifier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        jump();
    }

    // Player will jump when space bar is pressed and player is grounded
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rigidRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    // Checks if the player is grounded
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
