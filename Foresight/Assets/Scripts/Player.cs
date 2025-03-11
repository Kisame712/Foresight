using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpSpeed;

    Vector2 playerInput;
    Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void OnMove(InputValue input)
    {
        playerInput = input.Get<Vector2>();
    }

    void Run()
    {
        playerRb.velocity = new Vector2(playerInput.x * playerSpeed, playerRb.velocity.y);
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            playerRb.velocity += new Vector2(0f, jumpSpeed);
        }

    }
}
