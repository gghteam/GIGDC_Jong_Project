using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public bool canSpawn;
	public float spawnTime;
	public GameObject robotObj;
	private void Start()
	{
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			canSpawn = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			canSpawn = false;
		}
	}
	private void Update()
	{
		StartCoroutine(SpawnRobot());
	}
	IEnumerator SpawnRobot()
	{
		if (canSpawn)
		{
			robotObj.SetActive(true);
			yield return new WaitForSeconds(spawnTime);
			
		}
	}
}
