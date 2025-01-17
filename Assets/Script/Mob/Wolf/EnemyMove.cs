﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }
    public State state = State.PATROL;//에너미 상태
    public bool facingright = true;
    private Rigidbody2D rigid;

    public bool canAttack = false;
    public bool isMoving = true;
    public bool isTargeting = false;
    public float patrolDistance = 5f;
    public float movingSpeed = 2f;
    public float stayDelay = 3f;

    public Vector3[] patrolPoint = { new Vector3(0, 0, 0), new Vector3(5, 0, 0), new Vector3(10, 0, 0) };
    private Vector3 targetPoint;
    private List<Vector2> patrolList = new List<Vector2>();
    private int patrolIndex = 0;
    public Vector3 destination;    // 현재 목표 지점
    private EnemyFOV enemyFOV;


    private bool isStop = false;
    // Start is called before the first frame update
    private void Awake()
	{
        enemyFOV = GetComponent<EnemyFOV>();
        rigid = GetComponent<Rigidbody2D>();
        patrolPoint[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (int i = 0; i < patrolPoint.Length; i++)
        {
            patrolPoint[i] = new Vector3(patrolPoint[0].x+ patrolDistance*(i), patrolPoint[0].y, patrolPoint[0].z);
            patrolList.Add(patrolPoint[i]);
        }
        destination = patrolPoint[0];


    }
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isStop) return;

        //if ((destination - transform.position).sqrMagnitude <= 0.5f)
        //{
        //    isStop = true;
        //    StartCoroutine(StayPoint()); //목표 지점에 도달하면 일정시간 멈춤
        //}
        //rigid.velocity = (destination - transform.position).normalized * GetSpeed();
        CheckState();
        DoAction();
        if (state != State.ATTACK)
            rigid.velocity = (destination - transform.position).normalized * GetSpeed();

        if ((!facingright && rigid.velocity.x < 0) || (facingright && rigid.velocity.x > 0))
        {
            Flip();
        }
    }
    public void Stop()
    {
        rigid.velocity = Vector2.zero;
        StopAllCoroutines(); //모든 코루틴 종료
    }
    IEnumerator StayPoint()
    {
        yield return new WaitForSeconds(stayDelay);
        MoveNextPosition();
    }
    private void MoveNextPosition()
    {
        patrolIndex = (++patrolIndex) % patrolList.Count; //다음 패트롤 인덱스 구함
        destination = patrolList[patrolIndex];// 다음 인덱스를 패트롤리스트에 넣음
        isStop = false;
    }
    private float GetSpeed()
    {
        return movingSpeed * GameManager.GetScale();
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        facingright = !facingright;
        transform.localScale = scale;
    }
    private void DoAction()
    {
		switch (state)
		{
            case State.DIE:
                break;

            case State.PATROL:
				if ((destination - transform.position).sqrMagnitude <= 0.5f)
				{
					isStop = true;
					StartCoroutine(StayPoint()); //목표 지점에 도달하면 일정시간 멈춤
				}
				
				break;

			case State.TRACE:
                destination = targetPoint;
                if ((destination - transform.position).sqrMagnitude <= 0.5f)
                {
                    isStop = true;
                    StartCoroutine(TraceDelay()); //목표 지점에 도달하면 일정시간 멈춤
                }
                break;

			case State.ATTACK:
				break;
			
			default:
				break;
		}
	}
    private void CheckState()
	{
        if (enemyFOV.IsTracePlayer() && !isTargeting)       //어그로 안끌린상태
        {
            if (enemyFOV.IsViewPlayer())
            {
                targetPoint = enemyFOV.targetPos;
                isTargeting = true;
            }
        }
        else if (enemyFOV.IsTracePlayer() && isTargeting)       //어그로 끌린상태
        {
            state = State.TRACE;
            //공격 거리 체크
        }
        else if (!enemyFOV.IsTracePlayer())     //원안에 안들어가있는 상태
        {
            isTargeting = false;
            state = State.PATROL;
        }

    }
    private void Attack()
    {

    }
    private IEnumerator AttackDelay()
    {
        //공격
        yield return new WaitForSeconds(0.1f); 
    }
    private IEnumerator TraceDelay()
    {
        targetPoint = enemyFOV.targetPos;
        destination = targetPoint;
        yield return new WaitForSeconds(stayDelay);
        isStop = false;
    }
}
