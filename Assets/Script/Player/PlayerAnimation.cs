﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigid;
    private PlayerMove playerMove;
    private PlayerHit playerHit;
    private Stat playerStat;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerHit = GetComponent<PlayerHit>();
        playerStat = GetComponent<Stat>();
    }

    void Update()
    {
        //애니메이터 값 설정
        animator.SetFloat("moveSpeed", Mathf.Abs(rigid.velocity.x));
        animator.SetBool("isGround", playerMove.isGround);
        animator.SetFloat("ySpeed", rigid.velocity.y);
        animator.SetBool("isHit", playerHit.ishit);
        animator.SetFloat("hp", playerStat.hp);
    }
}
