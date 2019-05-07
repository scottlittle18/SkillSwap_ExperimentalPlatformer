using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the logic behind getting the name and description of each InventoryObject
/// </summary>
public class InventoryMenuItemToggle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The image component used to show the associated object's icon.")]
    private Image iconImage;

    public static event Action<InventoryObject> InventoryMenuItemSelected;
    
    private InventoryObject associatedInventoryObject;

    public InventoryObject AssociatedInventoryObject
    {
        get
        {
            return associatedInventoryObject;
        }
        set
        {
            associatedInventoryObject = value;
            iconImage.sprite = associatedInventoryObject.Icon;
        }
    }

    /// <summary>
    /// This will be plugged into the toggle's "OnValueChanged" property in the editor
    /// and called whenever the toggle is clicked.
    /// </summary>
    /// <param name="isOn"></param>
    public void InventoryMenuItemWasToggled(bool isOn)
    {
        //We only want to do the stuff when the toggle has been slected. Not when it has been deselected
        if (isOn)
        {
            InventoryMenuItemSelected?.Invoke(AssociatedInventoryObject);
        }

        //TODO: Debugging for InventoryMenuItem Toggling
        Debug.Log($"Toggled: {isOn}");
    }

    private void Awake()
    {
        Toggle toggle = GetComponent<Toggle>();
        ToggleGroup toggleGroup = GetComponentInParent<ToggleGroup>();
        toggle.group = toggleGroup;
    }
}
