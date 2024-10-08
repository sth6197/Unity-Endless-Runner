using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Obstacle obstacle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.activeSelf)
        {
            obstacle = other.GetComponent<Obstacle>();
            other.gameObject.SetActive(false);
        }
    }
}
