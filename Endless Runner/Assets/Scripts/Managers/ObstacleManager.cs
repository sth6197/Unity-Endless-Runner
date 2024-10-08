using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacleList;

    [SerializeField] int createCount = 5;
    [SerializeField] int random;


    void Start()
    {
        obstacleList.Capacity = 20;

        Create();

        StartCoroutine(ActiveObstacle());
    }

    public void Create()
    {
        for (int i = 0; i < createCount; i++)
        {
            GameObject prefab = ResourcesManager.Instance.Instantiate("Cone", gameObject.transform);

            prefab.SetActive(false);

            obstacleList.Add(prefab);
        }
    }

    public bool ExamineActive()
    {
        for (int i = 0; i < obstacleList.Count; i++)
        {
            if (obstacleList[i].activeSelf == false)
            {
                return false;
            }
        }

        return true;
    }

    public IEnumerator ActiveObstacle()
    {
        while (true)
        {
            yield return CoroutineCache.WaitForSecond(2.5f);

            random = Random.Range(0, obstacleList.Count);

            // ���� ���� ������Ʈ�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���մϴ�.

            while (obstacleList[random].activeSelf == true)
            {
                // ���� ����Ʈ�� �ִ� ��� ���� ������Ʈ�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���մϴ�.
                if (ExamineActive())
                {
                    // ��� ���� ������Ʈ�� Ȱ��ȭ�Ǿ� �ִٸ� ���� ������Ʈ�� ���� ������ ����
                    // ObstacleList�� �־��ݴϴ�.
                    GameObject clone = ResourcesManager.Instance.Instantiate("Cone", gameObject.transform);

                    clone.SetActive(false);

                    obstacleList.Add(clone);
                }

                // ���� �ε����� �ִ� ���� ������Ʈ�� Ȱ��ȭ�Ǿ� ������
                // random ������ ���� +1 �ؼ� �ٽ� �˻��մϴ�.

                random = (random + 1) % obstacleList.Count;
            }
        }
    }

    public GameObject GetObstacle()
    {
        return obstacleList[random];
    }
}
