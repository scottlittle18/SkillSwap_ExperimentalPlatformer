using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenFunctionality : MonoBehaviour
{
    public void StartButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene(0);
    }
}
