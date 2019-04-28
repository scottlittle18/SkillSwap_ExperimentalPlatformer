using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will only handle 2D horizontal Player Movement and will follow
/// the principles of Seperation of Concern
/// </summary>

public class PlayerMovementController : MonoBehaviour, ISwappableCharacter
{
    #region Field Mega-Region

    #region Serialized Fields
    [SerializeField]
    [Tooltip("Maximum possible movement speed of the active character.")]
    private float maxSpeed;

    [SerializeField]
    [Tooltip("The rate at which the active character will speed up while the " +
        "move button is held down.")]
    private float accelerationForce;

    [SerializeField]
    [Tooltip("The amount of force that will be applied to the active character's jump.")]
    private float jumpForce;

    [SerializeField]
    [Tooltip("The detection radius of the ground check GameObject")]
    private float groundCheckRadius;

    [SerializeField]
    [Tooltip("The Transform of the ground check GameObject")]
    private Transform groundCheckObject;
    #endregion

    #region Non-Serialized Fields
    private Rigidbody2D characterRigidbody;
    private LayerMask whatIsGround;
    private float moveInput;
    private bool jumpInput;
    private bool isActiveCharacter;
    private bool onGround;
    #endregion
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        InitializeCharacter();
    }

    // Update is called once per frame
    private void Update()
    {
        //Listen For Player Input
        if (isActiveCharacter)
            ListenForMovementInput();
    }

    private void FixedUpdate()
    {
        //CHECK if the Character is ON the GROUND
        CheckIfCharacterIsOnGround();

        //HANDLE Character (Player) MOVEMENT
        CharacterMovementHandler();
    }

    private void CheckIfCharacterIsOnGround()
    {
        //Determine if the Character is on the ground
        onGround = Physics2D.OverlapCircle(groundCheckObject.position, groundCheckRadius, whatIsGround);
    }

    private void InitializeCharacter()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();

        //Define Ground Layer Mask
        whatIsGround = LayerMask.GetMask("Ground");

        //Define groundCheckObject
        groundCheckObject = gameObject.transform.GetChild(1);
    }

    private void ListenForMovementInput()
    {
        //Input Variable Declarations
        moveInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
    }

    private void CharacterMovementHandler()
    {
        //Move character
        characterRigidbody.AddForce(Vector2.right * moveInput * accelerationForce);

        //Clamp velocity to prevent constant acceleration
        Vector2 clampedVelocity = characterRigidbody.velocity;
        clampedVelocity.x = Mathf.Clamp(characterRigidbody.velocity.x, -maxSpeed, maxSpeed);
        characterRigidbody.velocity = clampedVelocity;
    }

    #region ISwappableCharacter Interface

    public void SetAsInactiveCharacter()
    {
        //TODO: Used to change between the currently controlled character
        isActiveCharacter = false;
    }

    public void SetAsActiveCharacter()
    {
        //Used to make this the active character
        isActiveCharacter = true;
    }
    #endregion
}
