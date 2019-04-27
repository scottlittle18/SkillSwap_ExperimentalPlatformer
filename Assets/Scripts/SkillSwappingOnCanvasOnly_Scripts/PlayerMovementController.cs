using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will only handle 2D horizontal Player Movement and will follow
/// the principles of Seperation of Concern
/// </summary>

public class PlayerMovementController : MonoBehaviour, ISwappableCharacter
{
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
    #endregion

    #region Non-Serialized Fields
    private Rigidbody2D characterRigidbody;
    private float moveInput;
    private bool jumpInput;
    private bool canMove;
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
        if (canMove)
            ListenForMovementInput();
    }

    private void FixedUpdate()
    {
        //Handle Character (Player) Movement
        CharacterMovementHandler();
    }

    private void InitializeCharacter()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
    }

    private void ListenForMovementInput()
    {
        //Input Variable Declarations
        moveInput = Input.GetAxisRaw("Horizontal");
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

    public void SetAsInactiveCharacter()
    {
        //TODO: Used to change between the currently controlled character
        canMove = false;
    }

    public void SetAsActiveCharacter()
    {
        //Used to make this the active character
        canMove = true;
    }
}
