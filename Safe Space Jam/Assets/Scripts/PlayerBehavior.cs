using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Range(8, 15)]
    public float playerSpeed;

    Animator animator;

    private bool goingLeft;
    private bool goingRight;
    private bool goingUp;
    private bool goingDown;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerInput();
        FlipSprite();
        Animate();
    }

    private void PlayerInput()
    {
        // Player Input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Time.deltaTime * playerSpeed * Vector3.up;
            goingUp = true;
        }
        else goingUp = false;


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Time.deltaTime * playerSpeed * Vector3.left;
            goingLeft = true;
        }
        else goingLeft = false;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Time.deltaTime * playerSpeed * Vector3.down;
            goingDown = true;
        }
        else goingDown = false;


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Time.deltaTime * playerSpeed * Vector3.right;
            goingRight = true;
        }
        else goingRight = false;
            
    }

    private void FlipSprite()
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        
        if (goingRight)
        {
            playerSprite.flipX = true;
        }
        else if (goingLeft)
        {
            playerSprite.flipX = false;
        }
    }

    private void Animate()
    {
        if (!goingUp && !goingDown && !goingLeft && !goingRight)
            animator.SetBool("isWalking", false);
        else animator.SetBool("isWalking", true);
    }
}
