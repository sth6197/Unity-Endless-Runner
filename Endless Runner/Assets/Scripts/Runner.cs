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

public class Runner : State
{
    [SerializeField] Animator animator;
    [SerializeField] RoadLine roadline;
    [SerializeField] Rigidbody rigidBody;

    [SerializeField] float speed = 25.0f;
    [SerializeField] float positionX = 2.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private new void OnEnable()
    {
        base.OnEnable();

        InputManager.Instance.action += OnkeyUpdate;
    }

    void Start()
    {
        roadline = RoadLine.MIDDLE;
    }

    void OnkeyUpdate()
    {
        if (state == false) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(roadline != RoadLine.LEFT)
            {
                animator.Play("left avoid");
                roadline--;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(roadline != RoadLine.RIGHT)
            {
                animator.Play("right avoid");
                roadline++;
            }
        }
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
        if (state == false) return;

        Move();
    }

    private new void OnDisable()
    {
        base.OnDisable();

        InputManager.Instance.action -= OnkeyUpdate;
    }

    private void OnTriggerEnter(Collider other)
    {
        IHitable hitable = other.GetComponent<IHitable>();

        if (hitable != null)
        {
            hitable.Activate();
        }
    }
}