using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject transition;

    private string sceneName;

    public void Button(string s)
    {
        transition.SetActive(true);
        sceneName = s;
        Invoke("GoToScene", 0.5f);
    }

    private void GoToScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
