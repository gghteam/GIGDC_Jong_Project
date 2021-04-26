using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bat : MonoBehaviour
{
	public GameObject target;
	[SerializeField]
	private float speed = 10f;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			target = collision.gameObject;
		}
	}
	private void Update()
	{
		if (target != null)
		{
			Vector2 targetPos = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y).normalized;
			transform.Translate(targetPos * speed*Time.deltaTime);
		}
	}
}
