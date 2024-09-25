using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] GameObject Coin;
    [SerializeField] List<GameObject> Coins;    

    [SerializeField] float offset = 2.5f;
    [SerializeField] int createCount = 16;

    [SerializeField] int positionX = 4;

    private void Awake()
    {
        Coins.Capacity = 20;

        Create();            
    }

    public void Create()
    {
        for (int i = 0; i < createCount; i++)
        {
            GameObject clone = Instantiate(Coin);

            clone.transform.SetParent(gameObject.transform);

            clone.transform.localPosition = new Vector3(0, Coin.transform.position.y, offset * i);

            clone.SetActive(false);

            Coins.Add(clone);
        }
    }

    public void InitializePosition()
    {
        transform.localPosition = new Vector3(positionX * Random.Range(-1, 2), 0, 0);
    }
}
