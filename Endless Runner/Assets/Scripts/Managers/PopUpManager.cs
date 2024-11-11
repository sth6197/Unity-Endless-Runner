using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject Popup;
    [SerializeField] private GameObject GameOverUI;

    private bool isVisible = false;
    private bool isGameOver = false;

    private void Start()
    {
        if(Popup != null)
        {
            Popup.SetActive(isVisible);
        }

        if(GameOverUI != null)
        {
            GameOverUI.SetActive(isGameOver);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //TogglePopUp();

            if(isGameOver)
            {
                GameOverUI.SetActive(false);
                isGameOver = false;
            }

            TogglePopUp();
        }

        //else
        //{
        //    if(isGameOver && GameOverUI != null)
        //    {
        //        ToggleGameOverUI();
        //    }
        //    else if(Input.GetKeyDown(KeyCode.Escape)) 
        //    {
        //        GameOverUI.SetActive(false);
        //        isGameOver = false;
        //    }
        //}
    }

    private void TogglePopUp()
    {
        isVisible = !isVisible;
        Popup.SetActive(isVisible);

        if(isVisible)
        {
            EventManager.Publish(EventType.PAUSE);

            Time.timeScale = 0f;

            MouseManager.Instance.State(0);
        }
        else
        {
            EventManager.Publish(EventType.CONTINUE);

            Time.timeScale = 1f;

            MouseManager.Instance.State(1);
        }
    }

    private void ToggleGameOverUI()
    {
        isGameOver = !isGameOver;

        if (isGameOver && GameOverUI != null && !GameOverUI.activeSelf)
        {
            GameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!isGameOver && GameOverUI != null && GameOverUI.activeSelf)
        {
            GameOverUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void OnRestart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnResume()
    {
        TogglePopUp();
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Title");
    }

    public void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowGameOverUI()
    {
        isGameOver = true;
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}