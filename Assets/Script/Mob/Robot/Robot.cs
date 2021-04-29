using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] private GameObject spawnerObj;
    private float dir;
    [SerializeField] private float speed;
    public float lifeTime;
    private float time;
    private Vector2 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (spawnerObj.transform.localScale.x > 0)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(speed*dir,rigid.velocity.y);
        time += Time.deltaTime;
        if (time > lifeTime)
        {
            transform.position = spawnPos;
            time = 0;
            gameObject.SetActive(false);
        }
    }
}
