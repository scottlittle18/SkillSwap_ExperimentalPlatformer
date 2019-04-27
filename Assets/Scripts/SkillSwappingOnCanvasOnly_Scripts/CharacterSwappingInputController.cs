using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to handle swapping between the currently controlled character and 
/// changes camera position based on which character in currently active
/// </summary>

public class CharacterSwappingInputController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is the activePlayer to be " +
        "controlled when the game starts")]
    private GameObject activeCharacter;

    private PlayerMovementController activeCharacterController;

    public GameObject ActiveCharacter
    {
        get { return activeCharacter; }
        set {
            //  When the activePlayer changes render 
            //the previously activePlayer immobile
            activeCharacterController.SetAsInactiveCharacter();

            activeCharacter = value;

            //Give the new activePlayer the ability to move
            SwapPlayerMovementController(activeCharacter.GetComponent<PlayerMovementController>());
            activeCharacterController.SetAsActiveCharacter();

            //Change the position of this transform to adjust the camera position
            transform.position = activeCharacter.transform.position;
        }
    }

    private void SwapPlayerMovementController(PlayerMovementController newPlayerMovementController)
    {
        activeCharacterController.SetAsInactiveCharacter();
        //activeCharacterController = null;
        activeCharacterController = newPlayerMovementController;
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetStartingCharacter();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleCharacterSwappingInput();
    }

    private void HandleCharacterSwappingInput()
    {
        if (Input.GetButtonDown("SwitchToCharacter_1"))
        {
            ActiveCharacter = GameObject.Find("Character1");
            //ActiveCharacter.GetComponent<PlayerMovementController>().CanMove = true;
        }
        else if (Input.GetButtonDown("SwitchToCharacter_2"))
        {
            ActiveCharacter = GameObject.Find("Character2");
        }
    }

    private void SetStartingCharacter()
    {
        activeCharacterController = ActiveCharacter.GetComponent<PlayerMovementController>();
        activeCharacterController.SetAsActiveCharacter();
    }
}
