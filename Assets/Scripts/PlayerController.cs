using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float accelerationForce;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    [Tooltip("The transform of the groundCheck GameObject")]
    private Transform groundCheck;
    #endregion
    #region Non-Serialized Fields
    private Rigidbody2D playerRigidBody;
    private CircleCollider2D isTriggerCollision;
    private float moveInput;
    private bool jumpInput, canJump, onGround;
    private float playerMovement;
    private bool canMove;
    private LayerMask whatIsGround;
    #endregion
    #region Active Skill Related Fields
    //Double Jump Skill
    private bool skill_doubleJumpEnabled;
    private DoubleJumpSkill doubleJumpSkill;
    //Floating Platform Skill
    private bool skill_floatingPlatformEnabled;
    private bool isSuspended, suspensionInput;
    private FloatingPlatformSkill floatingPlatformSkill;
    #endregion

    #region Properties
    public bool CanMove
    {
        get{ return canMove; }
        set=> canMove = value;
    }

    public bool Skill_DoubleJumpEnabled
    {
        get { return skill_doubleJumpEnabled; }
        set => skill_doubleJumpEnabled = value;
    }

    public bool Skill_FloatingPlatformEnabled
    {
        get { return skill_floatingPlatformEnabled; }
        set => skill_floatingPlatformEnabled = value;
    }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        InitializePlayer();
    }

    private void FixedUpdate()
    {
        CheckIfOnGround();        

        if(CanMove && !isSuspended)
        {
            MovementHandler();

            if (onGround)
            {
                canJump = true;
            }
            else if (!onGround)
            {
                canJump = false;
            }
        }
        else if (!CanMove)
        {
            if(!onGround)
            {
                //Allows the newly inactive Character to fall to the ground before stopping all movement if Characters are swapped in midair
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else if(onGround || isSuspended)
            {
                //Stop all movement if the Character is no longer the Active Player and is on the Ground
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }            
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (CanMove && !isSuspended)
        {
            //TODO: onGround Check
            Debug.Log(gameObject.name + "'s grounded variable = " + onGround);

            GetMovementInput();

            if (onGround)
            {
                canJump = true;

                if (Skill_DoubleJumpEnabled)
                {
                    doubleJumpSkill.HasDoubleJumped = false;
                }
                else if(Skill_FloatingPlatformEnabled)
                {
                    floatingPlatformSkill.FirstJump = true;
                }
            }
            else if (!onGround)
            {
                canJump = false;
            }
        }
        else if (!CanMove || isSuspended)
        {
            if (!onGround && !isSuspended)
            {
                //Allows the newly inactive Character to fall to the ground before 
                //  stopping all movement if Characters are swapped in midair
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else if (onGround || isSuspended)
            {
                //Stop all movement if the Character is no longer the Active Player and is on the Ground
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }        
    }

    private void InitializePlayer()
    {
        Skill_DoubleJumpEnabled = false;
        Skill_FloatingPlatformEnabled = false;

        whatIsGround = LayerMask.GetMask("Ground");
        groundCheck = gameObject.transform.GetChild(0); //Retrieves the transform component from the child named GroundCheck
        playerRigidBody = GetComponent<Rigidbody2D>();
        isTriggerCollision = GetComponent<CircleCollider2D>();

        doubleJumpSkill = GameObject.Find("Skill_DoubleJump").GetComponent<DoubleJumpSkill>();
        floatingPlatformSkill = GameObject.Find("Skill_FloatingPlatform").GetComponent<FloatingPlatformSkill>();
    }

    private void CheckIfOnGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    #region Public Skill Switch Handlers
    public void CheckCurrentSkills(Component skillToActivate)
    {
        //TODO: Debug SkillName Check
        Debug.Log(skillToActivate.name + " was the component recieved by the player character");
        if (skillToActivate.name == "Skill_FloatingPlatform")
        {
            Skill_FloatingPlatformEnabled = true;

            //TODO: Debug Log
            Debug.Log("Connection was successful. The PLAYERCONTROLLER now has an attached skill called " + skillToActivate);
        }
        else if (skillToActivate.name == "Skill_DoubleJump")
        {
            Skill_DoubleJumpEnabled = true;

            //TODO: Debug Log
            Debug.Log("Connection was successful. The PLAYERCONTROLLER now has an attached skill called " + skillToActivate);
            Debug.Log("DoubleJumpSkill >> HasDoubleJumped = " + doubleJumpSkill.HasDoubleJumped);
        }
    }

    public void DisableAndRemoveSkill(GameObject skillToDeactivate)
    {
        if (skillToDeactivate.name == "Skill_FloatingPlatform")
        {
            Skill_FloatingPlatformEnabled = false;
            //TODO: Debug Log
            Debug.Log("Removal was successful. The PLAYERCONTROLLER has now Deactivated the skill called " + skillToDeactivate);
        }
        else if (skillToDeactivate.name == "Skill_DoubleJump")
        {
            Skill_DoubleJumpEnabled = false;
            //TODO: Debug Log
            Debug.Log("Deactivation of " + skillToDeactivate + " was successful.");
        }
    }
    #endregion

    private void GetMovementInput()
    {
        //Initialize Movement Variables
        moveInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        suspensionInput = Input.GetButtonUp("Jump");
        //TODO: Debug
        Debug.Log("jumpInput = " + jumpInput); //<-- TODO: Keep this to use for testing the Midair Ability

        

        //Only Accept this input if the FloatingPlatformSkill is Enabled
        if (Skill_FloatingPlatformEnabled && onGround && jumpInput)
        {
            //TODO: Debug
            Debug.Log("suspensionInput = " + suspensionInput + "If Statement Nest Level 1");
            //TODO: Debug
            Debug.Log("floatingPlatformSkill - FirstJump = " + floatingPlatformSkill.FirstJump);

            JumpHandler();

            if (floatingPlatformSkill.FirstJump && suspensionInput)
            {
                //TODO: Debug
                Debug.Log("If Statement Nest Level 2");
                isSuspended = true;
                floatingPlatformSkill.FirstJump = false;             
            }
            if(!floatingPlatformSkill.FirstJump && jumpInput && isSuspended)
            {
                //TODO: Debug
                Debug.Log("If Statement Nest Level 3");
                //TODO: Debug
                Debug.Log("If Statement Nest Level 4 - Final");
                //isSuspended = false;

                //playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

                JumpHandler();                
            }
        }

        //Regular Jump - Let the other If-Else handle the position freezing and
        else if (jumpInput && onGround && canJump && !Skill_FloatingPlatformEnabled)
        {
            //TODO: Debug
            Debug.Log("Regular Jump check_ jumpInput = " + jumpInput);
            JumpHandler();
        }        

        //For Double Jump Skill
        if (jumpInput && Skill_DoubleJumpEnabled && !onGround && !doubleJumpSkill.HasDoubleJumped && !Skill_FloatingPlatformEnabled)
        {
            DoubleJumpHandler();
        }
    }
    
    private void JumpHandler()
    {        
        playerRigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }

    private void DoubleJumpHandler()
    {
        AddDoubleJumpForce();
        doubleJumpSkill.HasDoubleJumped = true;
    }

    private void AddDoubleJumpForce()
    {
        playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpHeight);
    }

    private void MovementHandler()
    {
        playerRigidBody.AddForce(Vector2.right * moveInput * accelerationForce);
        Vector2 clampedVelocity = playerRigidBody.velocity;
        clampedVelocity.x = Mathf.Clamp(playerRigidBody.velocity.x, -maxSpeed, maxSpeed);
        playerRigidBody.velocity = clampedVelocity;

        //Sprite Flipping
        if (playerRigidBody.velocity.x > 0.1)
        {
            transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (playerRigidBody.velocity.x < -0.1)
        {
            //TODO: IF art that needs to be flipped is added, remember to flip the children of this object as well***
            /*
            transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            */
        }
        else
        {
            transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }
    }
}
