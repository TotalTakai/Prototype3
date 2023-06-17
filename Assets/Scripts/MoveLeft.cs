using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15.0f;
    private float leftBoundary = -15;
    private float dashSpeed = 2f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.gameOver == false)
        {
            if(playerControllerScript.Dash() == true)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * dashSpeed);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }       

        if (transform.position.x < leftBoundary && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
