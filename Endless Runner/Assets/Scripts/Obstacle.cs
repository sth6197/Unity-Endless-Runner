using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IHitable
{
    private PopUpManager popUpManager;

    void Start()
    {
        popUpManager = FindObjectOfType<PopUpManager>();
    }

    public void Activate()
    {
        if (popUpManager != null)
        {      
            EventManager.Publish(EventType.STOP);
            popUpManager.ShowGameOverUI();
        }

    }
}