using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGameObject : MonoBehaviour
{
    [SerializeField] float speed = 50f;

    public float Speed
    {
        get { return speed; }
    }
   
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
