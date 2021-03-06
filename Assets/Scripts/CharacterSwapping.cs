﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used by the ActiveCharacterManager GameObject to allow the player to swap between characters
/// </summary>

public class CharacterSwapping : MonoBehaviour
{

    //PlayerChar 1 is Green && PlayerChar 2 is Yellow
    private PlayerController yellowCharacter, greenCharacter;

    // Start is called before the first frame update
    void Start()
    {
        greenCharacter = GameObject.Find("P1_SqGreen").GetComponent<PlayerController>();
        yellowCharacter = GameObject.Find("P2_SqYellow").GetComponent<PlayerController>();

        greenCharacter.CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Character Swapping
        if (Input.GetKeyDown("1"))
        {
            yellowCharacter.CanMove = false;
            greenCharacter.CanMove = true;
            transform.position = greenCharacter.transform.position;
        }
        else if (Input.GetKeyDown("2"))
        {
            greenCharacter.CanMove = false;
            yellowCharacter.CanMove = true;
            transform.position = yellowCharacter.transform.position;
        }

        //Determine which player the Cinemachine Camera should follow
        if(greenCharacter.CanMove == true)
            transform.position = greenCharacter.transform.position;
        else if (yellowCharacter.CanMove == true)
            transform.position = yellowCharacter.transform.position;
            
    }
}
