using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(string sceneToSwitchTo)
    {
        SceneManager.LoadScene(sceneToSwitchTo);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exited game");
    }
}
