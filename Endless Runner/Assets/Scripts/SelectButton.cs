using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] Text buttonText;
    [SerializeField] AudioClip enterAudioClip;


    private void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
    }

    public void OnEnter()
    {
        buttonText.fontSize = 100;

        AudioManager.Instance.Listen(enterAudioClip);
    }

    public void OnLeave()
    {
        buttonText.fontSize = 75;
    }

    public void OnSelect()
    {
        buttonText.fontSize = 50;
    }
}
