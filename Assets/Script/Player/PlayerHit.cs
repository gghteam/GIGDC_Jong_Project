using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour, IDamageable
{
    private Stat playerStat;
    private PlayerMove playerMove;
    private int reqDamage;
    private Rigidbody2D rigid;
    private float dir;
    public void OnDamage(int damage)
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
            playerMove.dontMove = true;
            if(transform.localScale.x == Mathf.Abs(transform.localScale.x) && dir == 1 || transform.localScale.x != Mathf.Abs(transform.localScale.x) && dir == -1)
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
        dir = 1;
    }
    private void Update()
    {
        if (playerMove.dontMove && playerMove.isGround && rigid.velocity.y <= 0)
        {
            //StartCoroutine(delay());

            playerMove.dontMove = false;
            rigid.velocity = new Vector2(8 * dir, 0);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            if (transform.position.x < collision.gameObject.transform.parent.position.x)
            {
                dir = -1;
            }
            else
            {
                dir = 1;
            }
            OnDamage(10);
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        playerMove.dontMove = false;
    }
}
