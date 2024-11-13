using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] Text buttonText;
    [SerializeField] AudioClip enterAudioClip;
    [SerializeField] AudioClip selectAudioClip;

    private void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
    }

    public void OnEnter()   // 마우스가 버튼에 올라가면 
    {
        // 마우스가 올라갔을 때 버튼 글자 크기 100 
        buttonText.fontSize = 80;

        // 마우스가 올라갔을 때 버튼 소리
        AudioManager.Instance.Listen(enterAudioClip);
    }

    public void OnLeave()   // 마우스가 버튼을 떠나면 
    {
        // 마우스가 올라갔을 때 버튼 글자 크기 75 
        buttonText.fontSize = 75;
    }

    public void OnSelect()  // 마우스가 버튼을 선택하면 
    {
        // 마우스가 올라갔을 때 버튼 글자 크기 50 
        buttonText.fontSize = 50;

        AudioManager.Instance.Listen(selectAudioClip);
    }
}
