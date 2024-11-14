using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    [SerializeField] private Text totalScoreText; // UI 텍스트 연결
    

    void Start()
    {
        SaveDataManager.instance.SetText(totalScoreText);
        SaveDataManager.instance.UpdateTotalScore(); // 초기 점수 표시
    }

}
