using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dashAmount;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCoolDownTime;
    [SerializeField] TrailRenderer dashTrail;
    Vector2 playerInput;
    Rigidbody2D playerRb;
    private bool isDashing = false;
    private bool canDash = true;
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
        if (isDashing)
        {
            return;
        }
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
            playerRb.velocity += new Vector2(0f, jumpSpeed);
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

    void OnDash(InputValue value)
    {
        if (value.isPressed)
        {
            if (!isDashing)
            {
                StartCoroutine(PlayerDash());
            }
        }
    }

    IEnumerator PlayerDash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.velocity = new Vector2(dashAmount * transform.localScale.x , 0f);
        dashTrail.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        dashTrail.emitting = false;
        playerRb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDownTime);
        canDash = true;

    }
}
