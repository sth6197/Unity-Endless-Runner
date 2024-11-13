using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public void ExeCute()
    {
        EventManager.Publish(EventType.START);

        StartCoroutine(SceneryManager.Instance.AsyncLoad(1));                        
    }

    public void Setting()
    {
        EventManager.Publish(EventType.SETTING);
    }

    public void OpenSettingUI()
    {
        SettingManager.Instance.ShowSettingUI();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}