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
            rigid.AddForce(new Vector2(250 * dir, 300));
            //rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            //rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            Debug.Log("피격");
        }
    }
    IEnumerator isGround()
    {
        while(true)
        {
            if (playerMove.isGround)
                break;
            yield return null;
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(playerStat.hp);
            OnDamage(10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("asdfasdfsadf");
        Debug.Log(collision.gameObject.transform.parent.transform.position.x);
        if (collision.gameObject.CompareTag("Mob"))
        {
            if (transform.position.x < collision.gameObject.transform.parent.transform.position.x)
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
}
