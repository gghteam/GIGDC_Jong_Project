using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class Wolf : MonoBehaviour
{
    private Stat stat;
    private AttackRangeCheck check;
    private BoxCollider2D range;
    public float speed = 10f;
    public bool onTarget;
    public bool canMove;
    private int dir = 1;


    // Start is called before the first frame update
    void Start()
    {
        check = GetComponentInChildren<AttackRangeCheck>();
        stat = GetComponent<Stat>();
        range = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        //공격 범위 안에 들어오면 공격 시작
        if (check.canAttack&& onTarget)
        {
            StartCoroutine(WolfAttack());
        }

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어가 어그로 범위 안으로 들어오면 인식 범위 늘어남
        if (collision.gameObject.tag.Equals("Player"))
        {
            range.size = new Vector2(range.size.x*2, range.size.y * 2);
            onTarget = true;


        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !check.canAttack&&canMove)
        {
             transform.Translate(Vector2.left * speed * Time.deltaTime);
            //플레이어가 몹의 위치보다 오른쪽에 있다면 오른쪽 바라보게
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
                dir = 1;
            }
            //플레이어가 몹의 위치보다 왼쪽에 있다면 왼쪽 바라보게
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                dir = -1;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //플레이어가 어그로 범위 밖으로 나가면 인식 범위 줄어듬
        if (collision.gameObject.tag.Equals("Player"))
        {
            range.size = new Vector2(3f, 1.5f);
            onTarget = false;
        }
    }
    IEnumerator WolfAttack()
    {
        check.canAttack = false;
        canMove = false;
        yield return new WaitForSeconds(1f);

        DOTween.Clear();
        //점프 공격들
        transform.DOJump(new Vector3(transform.position.x + 20 * dir, 2, 0), 2, 1, 1f); // 이건 점프하는거 
        //transform.DOBlendableLocalMoveBy(new Vector3(20 * dir, 0, 0), 1);               // 이건 돌진하는거 - 원하는대로 바꿔쓰세연

        yield return new WaitForSeconds(1f);

        Debug.Log("공격");
        canMove = true;

    }
}
