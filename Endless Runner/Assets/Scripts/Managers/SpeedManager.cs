using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    [SerializeField] float increaseValue = 5f;
    [SerializeField] float LimitSpeed = 50f;

    public float Speed
    {
        get { return speed; }
    }

    private void Awake()
    {
        StartCoroutine(Accelerate());
    }

    IEnumerator Accelerate()
    {
        while(speed < LimitSpeed)
        {
            yield return new WaitForSeconds(10);

            speed += increaseValue;
        }
    }
}
