using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IHitable
{
    private PopUpManager popUpManager;

    void Start()
    {
        popUpManager = FindObjectOfType<PopUpManager>();  // 팝업매니저 자동 할당
    }

    public void Activate()
    {
        if (popUpManager != null)
        {      
            EventManager.Publish(EventType.STOP);
            popUpManager.ShowGameOverUI();  // 장애물에 닿으면 게임 오버 UI 호출 
        }

    }
}