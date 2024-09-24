using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject rotationGameObject;

    [SerializeField] float speed;

    private void OnEnable()
    {
        rotationGameObject = GameObject.Find("Rotation GameObject");
        
        speed = rotationGameObject.GetComponent<RotationGameObject>().Speed;

        transform.localRotation = rotationGameObject.transform.localRotation;
    }

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
