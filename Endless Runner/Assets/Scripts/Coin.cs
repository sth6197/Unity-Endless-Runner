using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : State, IHitable
{
    [SerializeField] GameObject rotationGameObject;
    [SerializeField] AudioClip audioClip;

    [SerializeField] float speed;

    public void Activate()
    {
        gameObject.SetActive(false);

        AudioManager.Instance.Listen(audioClip);
    }
    
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
