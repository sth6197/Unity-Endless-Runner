using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] GameObject Coin;

    [SerializeField] float offset = 2.5f;
    [SerializeField] int createCount = 16;

    private void Awake()
    {
        Create();            
    }

    public void Create()
    {
        for (int i = 0; i < createCount; i++)
        {
            GameObject clone = Instantiate(Coin);

            clone.transform.SetParent(gameObject.transform);

            clone.transform.localPosition = new Vector3(0, Coin.transform.position.y, offset * i);
        }
    }

    public void InitializePosition()
    {

    }
}
