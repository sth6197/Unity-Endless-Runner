using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] private GameObject settingUI;

    // 해상도 리스트
    private Vector2Int[] resolutions = new Vector2Int[]
    {
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080),
        new Vector2Int(2560, 1440),
    };

    private void Start()
    {
        int width = PlayerPrefs.GetInt("ResolutionWidth", 2560);
        int height = PlayerPrefs.GetInt("ResolutionHeight", 1440);
        bool fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        Screen.SetResolution(width, height, fullscreen);
        Screen.fullScreen = fullscreen;

        DontDestroyOnLoad(gameObject);
    }

    public void ShowSettingUI()
    {
        if (settingUI != null)
        {
            settingUI.SetActive(true);
        }
    }

    public void HideSettingUI()
    {
        if(settingUI != null)
        {
            settingUI.SetActive(false);
        }
    }

    public void ChangeResolution(int index)
    {
        if(index < 0 || index >= resolutions.Length)
        {
            return;
        }

        var resolution = resolutions[index];
        bool isFullscreen = resolution.x == 2560 && resolution.y == 1440;

        Screen.SetResolution(resolution.x, resolution.y, isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionWidth", Screen.currentResolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", Screen.currentResolution.height);
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void Set1600x900() => ChangeResolution(0);
    public void Set1920x1080() => ChangeResolution(1);
    public void Set2560x1440() => ChangeResolution(2);
}