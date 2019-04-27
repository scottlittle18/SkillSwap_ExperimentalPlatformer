using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Input specific to switching between characters and updates the camera position as needed
/// </summary>

public class CharacterSwappingInputController : MonoBehaviour
{
    #region Fields & Properties
    [SerializeField]
    [Tooltip("This is the activePlayer to be controlled when the game starts")]
    private GameObject activeCharacter;

    private PlayerMovementController activeCharacterController;

    public GameObject ActiveCharacter
    {
        get { return activeCharacter; }
        set {
            //DEACTIVATE the CHARACTER being SWAPPED FROM
            activeCharacterController.SetAsInactiveCharacter();

            activeCharacter = value;

            //ACTIVATE the CHARACTER being SWAPPED TO
            SwapPlayerMovementController(activeCharacter.GetComponent<PlayerMovementController>());
            activeCharacterController.SetAsActiveCharacter();

            //UPDATE the CAMERA to FOLLOW the NEWLY ACTIVATED CHARACTER
            transform.position = activeCharacter.transform.position;
        }
    }
    #endregion

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

    private void SetStartingCharacter()
    {
        activeCharacterController = ActiveCharacter.GetComponent<PlayerMovementController>();
        activeCharacterController.SetAsActiveCharacter();
    }

    private void HandleCharacterSwappingInput()
    {
        if (Input.GetButtonDown("SwitchToCharacter_1"))
        {
            ActiveCharacter = GameObject.Find("Character1");
        }
        else if (Input.GetButtonDown("SwitchToCharacter_2"))
        {
            ActiveCharacter = GameObject.Find("Character2");
        }
    }

    private void SwapPlayerMovementController(PlayerMovementController newPlayerMovementController)
    {
        activeCharacterController = newPlayerMovementController;
    }
}
