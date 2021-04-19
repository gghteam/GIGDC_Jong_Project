using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCheck : MonoBehaviour
{
	public bool canAttack;
	public float attackDelay = 2f;
	public float time;
	private void Update()
	{
		//타임
		if(!canAttack)
		time += Time.deltaTime;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//공격 범위안에 플레이어가 들어오면 공격할때 필요한 bool true로 바꿈
		//canAttack이 꺼지고 부터 쿨타임 계산해서 시간이 지나면 시작
		if (collision.gameObject.CompareTag("Player") &&attackDelay < time)
		{
			canAttack = true;
			time = 0f;
		}
		//if (collision.gameObject.tag == null)
		//{
		//	canAttack = false;
		//}
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Player") && attackDelay < time)
		{
			canAttack = true;
			time = 0f;
		}
	}
}
