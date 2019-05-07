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
    [Tooltip("This determines the maximum number of skills a character can have at any given time")]
    private int maxNumberOfSkills;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    [Tooltip("The transform of the groundCheck GameObject")]
    private Transform groundCheck;
    #endregion

    #region Non-Serialized Fields
    private Rigidbody2D playerRigidBody;
    private CircleCollider2D isTriggerCollision;
    private int currentNumberOfSkills;
    private float moveInput;
    private bool jumpInput, canJump, onGround;
    private float playerMovement;
    private bool canMove;
    private LayerMask whatIsGround;
    #endregion
    #region Active Skill Related Fields
    private bool doubleJumpEnabled;
    private DoubleJumpSkill doubleJumpSkill;
    #endregion

    #region Properties
    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }

    public bool DoubleJumpEnabled
    {
        get { return doubleJumpEnabled; }
        set => doubleJumpEnabled = value;
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
        /*
        if(onGround)
        {
            canJump = true;

            if(DoubleJumpEnabled)
            {
                doubleJumpSkill.HasDoubleJumped = false;
            }
            
        }
        else if(!onGround)
        {
            canJump = false;

            if (DoubleJumpEnabled)
            {
                doubleJumpSkill.HasDoubleJumped = true;
            }
        }
        */
        if(CanMove)
        {
            //Unlock all movement restraints EXCEPT Z-Rotation
            playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            MovementHandler();
            if (onGround)
            {
                canJump = true;

                if (DoubleJumpEnabled)
                {
                    //doubleJumpSkill.HasDoubleJumped = false;
                }

            }
            else if (!onGround)
            {
                canJump = false;

                if (DoubleJumpEnabled)
                {
                    //doubleJumpSkill.HasDoubleJumped = true;
                }
            }
        }
        else if (!CanMove)
        {
            if(!onGround)
            {
                //Allows the newly inactive Character to fall to the ground before 
                //  stopping all movement if Characters are swapped in midair
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else if(onGround)
            {
                //Stop all movement if the Character is no longer the Active Player and is on the Ground
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }            
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO: Debug Log
        Debug.Log(gameObject.name + "'s grounded variable = " + onGround);
        //TODO: DebugLog
        Debug.Log("DoubleJumpEnabled = " + DoubleJumpEnabled);
        //CheckCurrentSkills();
        
        if (CanMove)
        {
            //Unlock all movement restraints EXCEPT Z-Rotation
            playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetMovementInput();

            if (onGround)
            {
                canJump = true;

                if (DoubleJumpEnabled)
                {
                    doubleJumpSkill.HasDoubleJumped = false;
                }
            }
            else if (!onGround)
            {
                canJump = false;

                if (DoubleJumpEnabled)
                {
                    //doubleJumpSkill.HasDoubleJumped = ;
                }
            }
        }
        else if (!CanMove)
        {
            if (!onGround)
            {
                //Allows the newly inactive Character to fall to the ground before 
                //  stopping all movement if Characters are swapped in midair
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else if (onGround)
            {
                //Stop all movement if the Character is no longer the Active Player and is on the Ground
                playerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        
    }

    private void InitializePlayer()
    {
        DoubleJumpEnabled = false;
        whatIsGround = LayerMask.GetMask("Ground");
        groundCheck = gameObject.transform.GetChild(0); //Retrieves the transform component from the child named GroundCheck
        playerRigidBody = GetComponent<Rigidbody2D>();
        isTriggerCollision = GetComponent<CircleCollider2D>();

        doubleJumpSkill = GameObject.Find("Skill_DoubleJump").GetComponent<DoubleJumpSkill>();
    }

    private void CheckIfOnGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position,
            groundCheckRadius, whatIsGround);
    }

    public void CheckCurrentSkills(Component skillToActivate)
    {
        //TODO: Debug SkillName Check
        Debug.Log(skillToActivate.name + " was the component recieved by the player character");
        if (skillToActivate.name == "SkillConnectionTest")
        {
            //skillToActivate = GetComponent<SkillConnectionTest>();
            //TODO: Debug Log
            Debug.Log("Connection was successful. The PLAYERCONTROLLER now has an attached skill called " + skillToActivate);
        }
        else if (skillToActivate.name == "Skill_DoubleJump")
        {
            doubleJumpSkill = skillToActivate.GetComponent<DoubleJumpSkill>();
            DoubleJumpEnabled = true;

            //TODO: Debug Log
            Debug.Log("Connection was successful. The PLAYERCONTROLLER now has an attached skill called " + skillToActivate);
            Debug.Log("DoubleJumpSkill >> HasDoubleJumped = " + doubleJumpSkill.HasDoubleJumped);
        }
    }

    public void DisableAndRemoveSkill(GameObject skillToDeactivate)
    {
        if (skillToDeactivate.name == "SkillConnectionTest")
        {
            //skillToDeactivate = GetComponent<SkillConnectionTest>();
            //TODO: Debug Log
            Debug.Log("Connection was successful. The PLAYERCONTROLLER has now Deactivated the skill called " + skillToDeactivate);
        }
        else if (skillToDeactivate.name == "DoubleJumpSkill")
        {
            doubleJumpSkill = skillToDeactivate.GetComponent<DoubleJumpSkill>();
            DoubleJumpEnabled = false;
            //TODO: Debug Log
            Debug.Log("Connection was successful. The PLAYERCONTROLLER has now Deactivated the skill called " + skillToDeactivate);
        }
    }

    private void GetMovementInput()
    {
        //Initialize Movement Variables
        moveInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        //TODO: Debug
        Debug.Log("jumpInput = " + jumpInput);
        //Regular Jump
        if (jumpInput && onGround && canJump)
        {
            //TODO: Debug
            Debug.Log("Regular Jump check_ jumpInput = " + jumpInput);
            JumpHandler();
        }
        //For Double Jump Skill
        if (jumpInput && DoubleJumpEnabled && !onGround && !doubleJumpSkill.HasDoubleJumped)
        {
            //TODO: Debug.Log - DoubleJump - DepthTest_L1
            Debug.Log("DoubleJump - DepthTest_L1 - SUCCESS");
            DoubleJumpHandler();
        }
    }

    private void JumpHandler()
    {        
        playerRigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }

    private void DoubleJumpHandler()
    {
        //TODO: Debug.Log - DoubleJump - DepthTest_L3 - Inside Method
        Debug.Log("DoubleJump - DepthTest_L3 - Inside Method - SUCCESS");
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
            //TODO: Temporarily Disabling Sprite Flipping
            /*
            transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            */
        }
        else
        {
            transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }
    }

    private void AddSkill(GameObject skill)
    {
        if (currentNumberOfSkills < maxNumberOfSkills)
        {
            currentNumberOfSkills++;

            //Activate Skill Here via Switch-Case - use the script component that holds the skill abilities
        }
        else if(currentNumberOfSkills == maxNumberOfSkills)
        {
            Debug.Log("Max Number Of Skills Reached");

            //Deny Placement Of Skill onto Player and Move Skill to The SkillContainer
        }
    }

    private void RemoveSkill(GameObject skill)
    {
        currentNumberOfSkills--;

        //Deactivate Skill Here via Switch-Case
    }
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            onGround = true;
            canJump = true;
            //TODO: Debug Log
            Debug.Log("The Player has Detected the ground");
        }
    }

    //This may be affecting the Double Jump detection variable set by only reading the
    //  collision of the frame in which the player leaves the collision detection zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            if (DoubleJumpEnabled)
                doubleJumpSkill.HasDoubleJumped = false;
            onGround = false;
            //TODO: Debug Log
            Debug.Log("The Player has Left the Ground");
        }
    }*/
}
