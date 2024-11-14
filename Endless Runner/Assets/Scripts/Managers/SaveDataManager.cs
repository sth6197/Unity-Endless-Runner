using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;

    private Text totalScoreText;
    private int totalScore;

    private string saveFilepath; // 저장 파일 경로
    private SaveData saveData;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);  // 객체 유지
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
        
        LoadData(); // 게임이 시작될 때 데이터를 불러온다.
    }

    
    public void SetText(Text scoretext)
    {
        totalScoreText = scoretext;
        if(totalScoreText != null)
        {
            // 초기값
            totalScoreText.text = "Total Score : " + saveData.totalScore.ToString();
        }
    }

    [ContextMenu("To Json Data")]
    public void SaveData()
    {
        string json = JsonUtility.ToJson(saveData, true);
        saveFilepath = Path.Combine(Application.dataPath, "SaveData.json"); // 저장 경로
        File.WriteAllText(saveFilepath, json);
    }

    public void LoadData()
    {
        saveFilepath = Path.Combine(Application.persistentDataPath, "SaveData.json");

        if (File.Exists(saveFilepath))
        {
            string json = File.ReadAllText(saveFilepath); // 파일에서 json 읽어오기
            saveData = JsonUtility.FromJson<SaveData>(json); // json 데이터를 객체로 변환
        }
        else
        {
            saveData = new SaveData();  // 세이브 파일이 없으면 새로운 데이터 생성
            saveData.totalScore = 0; // 초기 총점
        }
    }

    public void AddScore(int score)
    {
        saveData.totalScore += score; // 점수 누적

        SaveData();
    }

    public void UpdateTotalScore()
    {
        if(totalScoreText != null)
        {
            totalScoreText.text = "Total Score \n" + saveData.totalScore.ToString();
        }
    }

    public int GetTotalScore()
    {
        return saveData.totalScore; // 누적된 점수 반환
    }

}

[System.Serializable]
public class SaveData
{
    public int totalScore; // 총 점수 저장할 변수
}
