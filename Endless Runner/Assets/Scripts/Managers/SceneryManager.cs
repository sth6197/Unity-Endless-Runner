using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneryManager : Singleton<SceneryManager>
{
    [SerializeField] private Image FadeImage;
    [SerializeField] private Image ScreenImage;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public IEnumerator FadeIn()
    {
        FadeImage.gameObject.SetActive(true); // 검은 화면 활성화

        Color color = FadeImage.color;
    
        color.a = 1f;

        while (color.a <= 1f) // 검어지면서 페이드 아웃
        {
            color.a += Time.deltaTime;
    
            FadeImage.color = color;
    
            yield return null;
        }
        
        ScreenImage.gameObject.SetActive(false); //배경화면 비활성화

        while (color.a >= 0f) // 투명해지면서 페이드 인
        {
            color.a -= Time.deltaTime;
            FadeImage.color = color;
            yield return null;
        }

        FadeImage.gameObject.SetActive(false); // 페이드 이미지 비활성화
    }

    public IEnumerator AsyncLoad(int index)
    {
        FadeImage.gameObject.SetActive(true); // 페이드 이미지 활성화

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index); // 로딩 상태 점검, 씬 전환, 씬 로딩중에도 게임이 계속 실행

        // { allowSceneActivation }
        // 장면이 준비된 즉시 장면이 활성화되는 것을 허용하는 변수입니다. 
        asyncOperation.allowSceneActivation = false;

        Color color = FadeImage.color;

        // { isDone }
        // 해당 동작이 완료되었는지 나타내는 변수입니다. (읽기 전용) 
        while (asyncOperation.isDone == false)
        {
            color.a += Time.deltaTime;
            FadeImage.color = color;

            // { progress }
            // 작업의 진행 상태를 나타내는 변수입니다. (읽기 전용) 
            if (asyncOperation.progress >= 0.9f) // 게임씬 로딩이 90퍼 이상인 경우
            {
                color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime); // 화면이 서서히 보이게

                FadeImage.color = color;

                if (color.a >= 1.0f) // 페이드 인이 끝난경우
                {
                    asyncOperation.allowSceneActivation = true; // 게임씬 활성화

                    yield break;
                }
            }
            yield return null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "Title")   // 타이틀 씬에서는 페이드 효과가 없도록
        {
            ScreenImage.gameObject.SetActive(true);
            Color color = ScreenImage.color;
            color.a = 1f;
            ScreenImage.color = color;
        }
        else // 게임씬으로 넘어갈때만 페이드 효과가 나타나도록
        {
            StartCoroutine(FadeIn());
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
