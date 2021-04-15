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
		if(!canAttack)
		time += Time.deltaTime;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag.Equals("Player")&&attackDelay < time)
		{
			canAttack = true;
			time = 0f;
		}
		//if (collision.gameObject.tag == null)
		//{
		//	canAttack = false;
		//}
	}
}
