using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] float JumpPower = 60f;

    bool isJump;

    private PopUpManager popUpManager;

    public Text txt;
    public int score;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        txt = GameObject.Find("ScoreShow").GetComponent<Text>(); // 점수판을 보여줄 UI 찾기

        popUpManager = GameObject.FindObjectOfType<PopUpManager>();

        rigidBody.useGravity = true; //게임 시작시 중력 활성화
    }

    private new void OnEnable() // 활성화
    {
        base.OnEnable();

        InputManager.Instance.action += OnkeyUpdate;    // 게임 시작시 키 입력 가능

        score = 0;  // 점수 0부터 시작
        txt.text = "Score : " + score; // 점수 증가
    }

    void Start()
    {
        roadline = RoadLine.MIDDLE; // 기본 위치
    }

    void OnkeyUpdate()  // 키 누르면 이동과 애니메이션 재생
    {
        if (state == false) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))    // 방향키 ← 를 눌렀을 때
        {
            if(roadline != RoadLine.LEFT)   // 현재 위치가 RoadLine.LEFT가 아니라면
            {
                animator.Play("left avoid"); // 왼쪽이동 애니메이션 재생
                roadline--; // 왼쪽으로 이동, roadline 배열 인덱스 감소, 딱 RoadLine.LEFT까지만 이동 가능하도록
            }
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 방향키 → 를 눌렀을 때
        {
            if (roadline != RoadLine.RIGHT)  // 현재위치가 RoadLine.RIGHT가 아니라면
            {
                animator.Play("right avoid"); // 오른쪽 이동 애니메이션 재생
                roadline++; // 오른쪽으로 이동, roadline 배열 인덱스 증가, 딱 RoadLine.RIGHT까지만 이동 가능하도록
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isJump == false) //점프가 false 라면
            {
                isJump = true; //점프를 true 하고
                rigidBody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse); //순간적으로 힘을 줘서 점프
                animator.Play("Jump"); //점프 애니메이션 재생
            }
        }
    }

    private void Move()
    {
        rigidBody.position = Vector3.Lerp // 부드럽게 이동, Vector3 간의 선형 보간 이동
        (
            rigidBody.position, 
            new Vector3((int)roadline * positionX, 0, 0), // roadline의 int 형변환, X축 이동
            speed * Time.fixedDeltaTime // 속도를 FixedUpdate 에서 참조할 것이므로 fixedDeltaTime 사용해서 속도 보정
        );
    }
    private void FixedUpdate()
    {
        if (state == false) return;

        Move(); // 리지드바디, 일정한 간격으로 움직임을 업데이트 하기위해 fixedupdate 사용

        if (rigidBody.velocity.y < 0) // 캐릭터가 하강 중일 때
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * 2f * Time.fixedDeltaTime; // 중력 가속도 추가
        }
    }

    private new void OnDisable()    // 비활성화 호출
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

            SaveDataManager.instance.AddScore(1);

            if(score == 1000)
            {
                if(popUpManager != null)
                {
                    popUpManager.ShowGameWinUI();
                }
            }
        }

        if (other.CompareTag("Cone"))
        {
            animator.Play("Flying Back Death"); //사망 애니메이션 재생

            //장애물에 부딪히고 게임오버 될 때 캐릭터 y값, 회전값 고정
            rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground") //캐릭터 콜라이더와 닿는 오브젝트의 태그가 Ground일 경우
        {
            isJump = false; //연속해서 점프할 수 없도록 false
        }
    }
}
