using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for interactive objects
/// </summary>
public interface IInteractive
{
    string DisplayText { get; }
    void InteractWithObject();
}
