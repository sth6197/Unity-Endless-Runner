using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : State
{
    [SerializeField] GameObject rotationGameObject;

    [SerializeField] float speed;

    private new void OnEnable()
    {
        base.OnEnable();

        rotationGameObject = GameObject.Find("Rotation GameObject");
        
        speed = rotationGameObject.GetComponent<RotationGameObject>().Speed;

        transform.localRotation = rotationGameObject.transform.localRotation;
    }

    private void Update()
    {
        if (state == false) return;

        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
