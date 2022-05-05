using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using MoreMountains;
using MoreMountains.Feedbacks;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

    #region Vars

    [Header("Movement")]
    [SerializeField] public float playerSpeed;
    [SerializeField] public float movingTime;
    [SerializeField] public bool playerMoving;
    [Space]
    [SerializeField] public float playerJumpForce;
    [SerializeField] public float playerInAirTime;
    [Space]
    [SerializeField] public LayerMask defaultLayer;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask wallLayer;
    [Space]
    [SerializeField] public bool playerOnGround;
    [SerializeField] public bool playerOnWall;
    [SerializeField] public bool playerInAir;
    
    [Header("Effects")] 
    [SerializeField] public bool playerIsFlipped = false;
    [Space]
    [SerializeField] public float playerFallScale;
    [Space]
    [SerializeField] public MMFeedbacks playerLandFeedback;
    [SerializeField] public MMFeedbacks playerJumpFeedback;
    [SerializeField] public MMFeedbacks playerShootFeedback;

    [Header("Vectors")]
    [SerializeField] public Vector3 playerSpriteDefaultScale;
    [Space]
    [SerializeField] public Vector2 inputVector;

    [Header("Connections")]
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public Transform playerVisuals;
    [SerializeField] private SpriteRenderer playerSprite;
    [Space]
    [SerializeField] public GameObject landParticleSystem;

    private static readonly int PlayerIsMovingOnGround = Animator.StringToHash("playerIsMovingOnGround");

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        #region Get Components

        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerTransform = GetComponent<Transform>();

        #endregion

        playerSpriteDefaultScale = playerVisuals.localScale;
        
    }

    private void FixedUpdate()
    {
        // in air time
        if (playerInAir)
        {
            playerInAirTime += Time.deltaTime;
        }
        else
        {
            playerInAirTime = 0;
        }

        Movement();
    }

    // Update is called once per frame
    private void Update()
    {
        playerVisuals.localScale = playerInAir ? new Vector3(playerFallScale, playerSpriteDefaultScale.y + 0.1f, playerSpriteDefaultScale.z) : playerSpriteDefaultScale;
        
        // is moving
        playerMoving = inputVector != new Vector2(0, 0);

        if (playerOnGround)
        {
            playerAnimator.SetBool("playerIsOnGround", true);
            
            if (playerMoving)
            {
                playerAnimator.SetBool("playerIsMovingInAir", false);
                playerAnimator.SetBool("playerIsMovingOnGround", true);
            }
            else
            {
                playerAnimator.SetBool("playerIsMovingOnGround", false);
            }
        }
        else if (playerInAir)
        {
            playerAnimator.SetBool("playerIsOnGround", false);
            
            if (playerMoving)
            {
                playerAnimator.SetBool("playerIsMovingOnGround", false);
                playerAnimator.SetBool("playerIsMovingInAir", true);
            }
            else
            {
                playerAnimator.SetBool("playerIsMovingInAir", false);
            }
        }
        else
        {
            playerAnimator.SetBool("playerIsMovingOnGround", false);
        }
    }

    #region Jump

    #region Action

    public void PlayerJumpAction(InputAction.CallbackContext context)
    {
        if (context.performed && playerOnGround)
        {
            PlayerJump();
        }
    }

    #endregion

    private void PlayerJump()
    {
        //playerRigidbody.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
        playerJumpFeedback.PlayFeedbacks();
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerJumpForce);
        
        playerAnimator.SetTrigger("playerJumpAnimationTrigger");
    }

    #endregion

    #region Movement

    #region Action

    public void PlayerMovementAction(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();

        if (!playerIsFlipped)
        {
            if (inputVector != Vector2.left) return;
            playerVisuals.rotation = new Quaternion(0, 180, 0, 0);
            playerIsFlipped = true;
        }
        else
        {
            if (inputVector != Vector2.right) return;
            playerVisuals.rotation = new Quaternion(0, 0, 0, 0);
            playerIsFlipped = false;
        }
    }

    #endregion

    private void Movement()
    {
        var moveBy = inputVector.x * playerSpeed;

        playerRigidbody.velocity = new Vector2(moveBy, playerRigidbody.velocity.y);
    }

    #endregion

    #region Shooting

    public void PlayerShootAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerInputShoot();
        }
    }

    private void playerInputShoot()
    {
        Debug.Log("shoot! damn that bullet is so HOT!");
    }

    #endregion
}