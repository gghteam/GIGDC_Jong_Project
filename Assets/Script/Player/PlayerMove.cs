﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float xMove;
    private PlayerInput playerInput;
    private PlayerTP playerTP;

    [Header("이동관련")]
    public bool facingRight = true;
    public float moveSpeed = 8f;

    [Header("점프관련")]
    public bool isGround = false;
    public float maxJumpHeight = 5f; //최대 점프 높이
    public LayerMask whatIsGround; //무엇이 바닥인가?
    public Transform groundChecker;
    public int maxJumpCount = 2; //최대 2회 점프
    public float jumpSpeed = 5f;
    public float jumpTime = 0.5f; //.5초동안 누르고 있을 수 있다.


    private int jumpCount;
    private float stayJumpKey = 0;
    private float startYPos = 0; //점프를 시작했을 때 y포지션

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerTP = GetComponent<PlayerTP>();
    }

    void Update()
    {
        if (playerTP.state == TPState.CHARGING)
        {
            xMove = 0;
            return; //차징중일때는 움직일 수 없고 오로지 차저만을 작동시킨다.
        }
        xMove = playerInput.xMove;

        if ((facingRight && xMove < 0) || (!facingRight && xMove > 0))
        {
            Flip();
        }

        if (playerInput.jump)
        { //점프키가 눌렸을 때
            JumpStart();
        }
    }

    private void JumpStart()
    {
        if (isGround || jumpCount > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
            stayJumpKey = jumpTime; //점프시간만큼만 점프키를 누를 수 있다.
            startYPos = transform.position.y;
            StartCoroutine(JumpKeyHolding());
        }
    }

    IEnumerator JumpKeyHolding()
    {
        //키가 계속 눌리고 있거나, 점프키다운시간이 다되었거나 점프높이를 초과했다면 그만
        while (playerInput.jumpHolding
                && stayJumpKey > 0
                && transform.position.y <= startYPos + maxJumpHeight)
        {
            stayJumpKey -= Time.deltaTime;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
            yield return null;
        }
        rigid.velocity = new Vector2(rigid.velocity.x, 0); //가속 제거
        jumpCount--;
    }

    void FixedUpdate()
    {

        rigid.velocity = new Vector3(xMove * moveSpeed, rigid.velocity.y);


        isGround = Physics2D.OverlapCircle(groundChecker.position, 0.1f, whatIsGround);

        if (isGround && rigid.velocity.y < 0)
        { //땅바닥에 착지했다면 점프카운트 리셋
            jumpCount = maxJumpCount;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        facingRight = !facingRight;
        transform.localScale = scale;
    }
}