using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTutorial : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpSpeed;

    Vector2 playerInput;
    Rigidbody2D playerRb;
    BoxCollider2D playerFeetCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
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
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            playerRb.velocity = new Vector2(0f, jumpSpeed);
        }
    }


    void Flip()
    {
        bool playerHasHorizontalVelocity = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalVelocity)
        {

            transform.localScale = new Vector2(Mathf.Sign(playerRb.velocity.x), 1f);
        }

    }


}
