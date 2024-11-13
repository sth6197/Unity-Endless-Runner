using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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

    public Text txt;
    public int score;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        txt = GameObject.Find("ScoreShow").GetComponent<Text>();
    }

    private new void OnEnable()
    {
        base.OnEnable();

        InputManager.Instance.action += OnkeyUpdate;

        score = 0;
        txt.text = "Score : " + score;
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Jump");
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
        
        if(other.CompareTag("Coin"))
        {
            score += 1;
            txt.text = "Score : " + score;
        }
    }
}