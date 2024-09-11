using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum RoadLine
{
    LEFT = -1,
    MIDDLE,
    RIGHT
}

public class Runner : MonoBehaviour
{
    [SerializeField] RoadLine roadline;
    [SerializeField] Rigidbody rigidBody;

    [SerializeField] float speed = 25.0f;
    [SerializeField] float positionX = 2.5f;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        roadline = RoadLine.MIDDLE;
    }

    void OnkeyUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(roadline != RoadLine.LEFT)
            {
                roadline--;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(roadline != RoadLine.RIGHT)
            {
                roadline++;
            }
        }
    }

    void Update()
    {
        OnkeyUpdate();   
    }

    private void Move()
    {
        rigidBody.position = Vector3.Lerp
        (
            rigidBody.position, 
            new Vector3((int)roadline * positionX, 0, 0), 
            speed * Time.fixedDeltaTime
        );
    }

    private void FixedUpdate()
    {
        Move();
    }
}
