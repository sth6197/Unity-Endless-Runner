using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseManager : Singleton<MouseManager>
{
    [SerializeField] Texture2D texture2D;

    private void Start()
    {
        texture2D = ResourcesManager.Instance.Load<Texture2D>("Default");

        Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void State(int state)
    {
        switch (state)
        {
            case 0: 
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                break;
            case 1:
                    Cursor.visible = false; 
                    Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        State(scene.buildIndex);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
