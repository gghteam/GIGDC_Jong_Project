using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfRange : MonoBehaviour
{
    public Wolf wolf;
    public AttackRangeCheck check;
    public GameObject wolfObj;
    public BoxCollider2D range;
    private GameObject target;
    private void Start()
    {
        wolf = GetComponentInParent<Wolf>();
        check = GetComponentInChildren<AttackRangeCheck>();
        range = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어가 어그로 범위 안으로 들어오면 인식 범위 늘어남
        if (collision.gameObject.CompareTag("Player"))
        {
            range.size = new Vector2(range.size.x * 2,range.size.y * 2);
            wolf.onTarget = true;
            target = collision.gameObject;
            Debug.Log(target.name);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !check.canAttack && wolf.canMove&& target !=null)
        {

            if (!wolf.inWall)
                wolfObj.transform.Translate(Vector2.left * wolf.speed * wolf.dir * Time.deltaTime);
            //플레이어가 몹의 위치보다 오른쪽에 있다면 오른쪽 바라보게
            if (target.transform.position.x > transform.position.x)
            {
                wolfObj.transform.localScale = new Vector3(7, 7, 1);
                wolf.dir = 1;
            }
            //플레이어가 몹의 위치보다 왼쪽에 있다면 왼쪽 바라보게
            else
            {
                wolfObj.transform.localScale = new Vector3(-7, 7, 1);
                wolf.dir = -1;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //플레이어가 어그로 범위 밖으로 나가면 인식 범위 줄어듬
        if (collision.gameObject.CompareTag("Player"))
        {
            range.size = new Vector2(3f, 1.5f);
            wolf.onTarget = false;
        }
    }
}
