using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour, IDamageable
{
    private Stat playerStat;
    private PlayerMove playerMove;
    private Rigidbody2D rigid;
    private PlayerRenderer pr;
    private float dir;
    private int reqDamage;
    private bool evasion;
    private bool evasionTime;
    public bool ishit = false;
    float test;
    public void OnDamage(int damage) // 피격 당하면 함수 실행
    {
        reqDamage = damage - playerStat.def;
        if(reqDamage <= 0)
        {
            reqDamage = 1;
        }
        playerStat.hp -= reqDamage;
        if(playerStat.hp <= 0)
        {
            playerStat.hp = 0;
            //사망
        }
        else
        {
            //피격
            if (transform.localScale.x == Mathf.Abs(transform.localScale.x) && dir == 1 || transform.localScale.x != Mathf.Abs(transform.localScale.x) && dir == -1) // 맞는 방향에 따라서 바라보는 곳이 달리짐다
            {
                playerMove.facingRight = !playerMove.facingRight;
            }

            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -dir, transform.localScale.y);
            rigid.velocity = new Vector2(8 * dir, 7);
            Debug.Log("피격");
        }
    }

    private void Awake()
    {
        playerStat = GetComponent<Stat>();
        rigid = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
        pr = GetComponent<PlayerRenderer>();
        dir = 1;
        test = 0;
        evasion = false;
    }
    private void Update()
    {
        if (playerMove.dontMove && playerMove.isGround && ishit &&rigid.velocity.y <= 0) // 피격 당한뒤 땅에 닿았을 경우
        {
			StartCoroutine(delay());
            ishit = false;
            //playerMove.dontMove = false;
            rigid.velocity = new Vector2(5 * dir, 0);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerMove.dontMove || evasion) // 이미 맞은 상태라면 리턴 || 무적시간이라면 리턴
            return;
        if (collision.gameObject.CompareTag("Mob")) // 좌표 값에 따라서 dir 값이 변함
        {
            if (transform.position.x < collision.gameObject.transform.parent.position.x)
            {
                dir = -1;
            }
            else
            {
                dir = 1;
            }
            ishit = true;
            playerMove.dontMove = true;
            OnDamage(10);
        }
    }
    IEnumerator delay() // 뒤로 구르는 딜레이 넣기
    {
        yield return new WaitForSeconds(0.3f);
        playerMove.dontMove = false;
        evasion = true;
    }
    IEnumerator CoEvasion(float time)
    {
        yield return null; // 이부분은 코딩중.........
    }
}
