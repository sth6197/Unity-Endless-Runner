using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject Popup;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject GameWinUI;
    [SerializeField] AudioClip pauseSound;

    private SaveDataManager saveDataManager;

    private bool isVisible = false;
    private bool isGameOver = false;
    private bool isGameWin = false;

    private void Awake()
    {
        saveDataManager = FindObjectOfType<SaveDataManager>(); // 세이브 데이터 매니저를 찾아서 저장
    }

    private void Start()
    {
        if(Popup != null)   // UI 기본 비활성화 
        {
            Popup.SetActive(isVisible);
        }

        if(GameOverUI != null)  // UI 기본 비활성화 
        {
            GameOverUI.SetActive(isGameOver);
        }

        if(GameWinUI != null)   // UI 기본 비활성화
        {
            GameWinUI.SetActive(isGameWin);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  // ESC키를 누르면 
        {
            if(isGameOver)  // GameOverUI가 활성화 됐다면
            {
                GameOverUI.SetActive(false);    // 비활성화
                isGameOver = false;
            }

            if (isGameWin) // GameWinUI가 활성화 됐다면
            {
                GameWinUI.SetActive(false); // 비활성화
                isGameWin = false;
                OnMainMenu();   // title 씬으로 돌아가기
            }

            // UI를 비활성화 하면서 팝업창 오픈 
            TogglePopUp();
        }
    }

    private void TogglePopUp()  // 팝업창 오픈 함수 
    {
        isVisible = !isVisible;
        Popup.SetActive(isVisible);

        if(isVisible)   // UI가 비활성화일 때 
        {
            EventManager.Publish(EventType.PAUSE);  // 이벤트 호출 

            AudioManager.Instance.Listen(pauseSound);   // 퍼즈 소리 재생

            Time.timeScale = 0f;    // 게임 정지 

            MouseManager.Instance.State(0); // 마우스 보이고, 고정 해제 
        }
        else  // UI가 활성화일 때 
        {
            EventManager.Publish(EventType.CONTINUE);   // 이벤트 호출 

            Time.timeScale = 1f;    // 게임 재개 

            MouseManager.Instance.State(1); // 마우스 숨기고, 고정 
        }
    }

    private void ToggleGameOverUI() // 게임 오버 UI 호출 함수 
    {
        isGameOver = !isGameOver;

        if (isGameOver && GameOverUI != null && !GameOverUI.activeSelf)
        {
            // 게임 오버시 게임 오버 UI 활성화, 게임 정지 
            GameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!isGameOver && GameOverUI != null && GameOverUI.activeSelf)
        {
            // 게임 오버 UI 비활성화, 게임 재개 
            GameOverUI.SetActive(false);

            AudioManager.Instance.Listen(pauseSound); // 퍼즈 소리 재생

            Time.timeScale = 1f;
        }
    }

    private void ToggleGameWinUI() // 게임 승리 UI 호출 함수
    {
        isGameWin = !isGameWin;

        if (isGameWin && GameWinUI != null && !GameWinUI.activeSelf)
        {
            GameWinUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!isGameWin && GameWinUI != null && GameWinUI.activeSelf)
        {
            GameWinUI.SetActive(false);

            Time.timeScale = 1f;
        }
    }

    public void OnRestart() // 게임 다시 시작 함수 
    {
        Time.timeScale = 1f;

        // Game 씬 다시 호출 
        SceneManager.LoadScene("Game");
    }

    public void OnResume()  // 게임 재개 함수 
    {
        TogglePopUp();
    }

    public void OnMainMenu()    // 타이틀 화면으로 돌아가는 함수 
    {
        Time.timeScale = 1f;    // 게임 속도 1배속 

        StartCoroutine(LoadTitleScene());    // 타이틀 씬 호출 
    }

    private IEnumerator LoadTitleScene()
    {
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene("Title");
    }

    public void OnExit()    // 게임 종료 함수 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowGameOverUI()    // 게임 오버 UI 활성화 함수 
    {
        if(saveDataManager != null)
        {
            saveDataManager.AddScore(1); // 게임 오버시 점수 저장
            saveDataManager.SaveData(); // 저장
        }

        isGameOver = true;
        GameOverUI.SetActive(true);
        Time.timeScale = 0.5f;
    }

    public void ShowGameWinUI()
    {
        if(saveDataManager != null)
        {
            saveDataManager.AddScore(1); // 게임 승리시 점수 저장
            saveDataManager.SaveData(); // 저장
        }

        isGameWin = true;
        GameWinUI.SetActive(true);
        Time.timeScale = 0f;
    }
}