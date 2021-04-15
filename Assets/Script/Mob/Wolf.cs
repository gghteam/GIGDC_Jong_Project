using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Wolf : MonoBehaviour
{
    private Stat stat;
    private AttackRangeCheck check;
    private BoxCollider2D range;
    public float speed = 10f;
    public BoxCollider2D attackZone;
    public bool isAttack = false;


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
        if (check.canAttack)
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
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !isAttack)
        {
            for (int i = 0; i < 10; i++)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            //플레이어가 몹의 위치보다 오른쪽에 있다면 오른쪽 바라보게
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            //플레이어가 몹의 위치보다 왼쪽에 있다면 왼쪽 바라보게
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //플레이어가 어그로 범위 밖으로 나가면 인식 범위 줄어듬
        if (collision.gameObject.tag.Equals("Player"))
        {
            range.size = new Vector2(3f, 1.5f);
        }
    }
    IEnumerator WolfAttack()
    {
        yield return new WaitForSeconds(0.5f);
        //공격 반복을 막기위해 false
        check.canAttack = false;
        Debug.Log("공격");
        
    }
}
