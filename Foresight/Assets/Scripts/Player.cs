using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpSpeed;
    
    [SerializeField] TrailRenderer dashTrail;
    Vector2 playerInput;
    Rigidbody2D playerRb;
    private bool isDashing = false;
    private bool canDash = true;
    BoxCollider2D playerFeetCollider;
    private bool isFacingRight = true;

    [Header("Ability")]
    public int ability;
    AbilitySelector abilitySelector;
    public bool isAbilitySelected;

    [Header("Dash Ability")]
    [SerializeField] float dashAmount;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCoolDownTime;

    [Header("Double Jump")]
    int numberOfMaxJumps = 2;
    int numberOfRemainingJumps;

    [Header("Attack")]
    public float attackDistance;
    public GameObject attackOrigin;

    [Header("Animations and Effects")]
    Animator playerAnim;
    public GameObject nullifyEffect;
    public GameObject lightParticle;

    [Header("Player Audio")]
    AudioSource playerAudioSource;
    public AudioClip jumpSound;
    public float jumpVolume;
    public AudioClip attackSound;
    public float attackVolume;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        abilitySelector = FindObjectOfType<AbilitySelector>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAbilitySelected)
        {
            return;
        }
        if (isDashing)
        {
            return;
        }
        Run();
        Flip();
        Enlighten();
    }

    void OnMove(InputValue input)
    {
        playerInput = input.Get<Vector2>();
    }

    void Run()
    {
        if (!isAbilitySelected)
        {
            return;
        }
        if(playerInput.x != 0)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
        playerRb.velocity = new Vector2(playerInput.x * playerSpeed, playerRb.velocity.y);
    }

    void OnJump(InputValue value)
    {
        if (ability != 1)
        {
            SingleJump(value);
        }
        else
        {
            DoubleJump(value);
        }
    }

    void SingleJump(InputValue value)
    {
        if (!isAbilitySelected)
        {
            return;
        }
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            playerAnim.SetTrigger("jump");
            playerAudioSource.PlayOneShot(jumpSound, jumpVolume);
            playerRb.velocity = new Vector2(0f, jumpSpeed);
        }

    }

    void DoubleJump(InputValue value)
    {   
        if (!isAbilitySelected)
        {
            return;
        }
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if(numberOfRemainingJumps == 0)
            {
                return;
            }
            else if(numberOfRemainingJumps > 0 && value.isPressed)
            {
                playerAnim.SetTrigger("jump");
                playerAudioSource.PlayOneShot(jumpSound, jumpVolume);
                playerRb.velocity = new Vector2(0f, jumpSpeed);
                numberOfRemainingJumps--;
            }
        }
        else
        {
            numberOfRemainingJumps = numberOfMaxJumps;
            if (value.isPressed)
            {
                playerAnim.SetTrigger("jump");
                playerAudioSource.PlayOneShot(jumpSound, jumpVolume);
                playerRb.velocity = new Vector2(0f, jumpSpeed);
                numberOfRemainingJumps--;
            }
        }

    }

    void Flip()
    {
 
        if(playerRb.velocity.x < 0 && isFacingRight == true)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFacingRight = false;
        }
        else if(playerRb.velocity.x > 0 && isFacingRight == false)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isFacingRight = true;
        }

    }

    void OnDash(InputValue value)
    {
        if (!isAbilitySelected || ability!=0)
        {
            return;
        }
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
        playerAnim.SetTrigger("dash");
        playerRb.velocity = new Vector2(dashAmount * transform.localScale.x , 0f);
        dashTrail.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        dashTrail.emitting = false;
        playerRb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDownTime);
        canDash = true;

    }

    void OnFire(InputValue value)
    {
        if (!isAbilitySelected || ability != 2)
        {
            return;
        }
        if (value.isPressed)
        {
            playerAnim.SetTrigger("attack");
            playerAudioSource.PlayOneShot(attackSound, attackVolume);
            Collider2D col = Physics2D.OverlapCircle(attackOrigin.transform.position, attackDistance, LayerMask.GetMask("Ground"));
            if(col == null)
            {
                return;
            }
            BreakableWall wall = col.GetComponent<BreakableWall>();

            if(wall == null)
            {
                return;
            }
            else
            {
                wall.BreakOnAttack();
            }
        }

    }

    void Enlighten()
    {
        if(!isAbilitySelected || ability != 4)
        {
            return;
        }
        
        Collider2D col = Physics2D.OverlapCircle(lightParticle.transform.position, 0.5f, LayerMask.GetMask("Cloud"));
        if(col == null)
        {
            return;
        }
        DarkCloud cloud = col.GetComponent<DarkCloud>();
        if(cloud == null)
        {
            return;
        }
        else
        {
            cloud.RemoveClouds();
        }
    }

}
