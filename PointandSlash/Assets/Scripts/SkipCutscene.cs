using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCutscene : MonoBehaviour
{
    public MenuManager menu;
    public string sceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            menu.Button(sceneName);
        }
    }
}
