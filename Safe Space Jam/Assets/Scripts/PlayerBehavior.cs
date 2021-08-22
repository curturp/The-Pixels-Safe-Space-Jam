using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] [Range(5, 20)] private int playerSpeed;

    private bool goingLeft = false;
    private bool goingRight = false;


    private void Update()
    {
        PlayerInput();
        FlipSprite();
    }

    private void PlayerInput()
    {
        // Player Input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            transform.position += Time.deltaTime * playerSpeed * Vector3.up;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Time.deltaTime * playerSpeed * Vector3.left;
            goingLeft = true;
            goingRight = false;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.position += Time.deltaTime * playerSpeed * Vector3.down;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Time.deltaTime * playerSpeed * Vector3.right;
            goingRight = true;
            goingLeft = false;
        }
            
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
}
