using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStat : MonoBehaviour, IDamageable
{
    public int maxHp = 100;
    public int hp = 100;
    private PlayerScript playerScript;
    [SerializeField]
    private HealthBar playerHealth;
    //이외의 스텟들

    private void Awake()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
        hp = maxHp;
    }

    public void OnDamage(int damage)
    {
        Debug.Log(damage);

        hp -= damage;
        playerHealth.UpdateHealthBar(hp, maxHp);
        playerScript.anime.Play("Hit");
        if(hp <= 0)
        {
            playerScript.anime.Play("Dead");
        }
    }

    public void OnHit()
    {

        //transform.DOJump(new Vector3(5 * (playerScript.SR.flipX ? -1 : 1), 0, 0), 1, 1, 1);

        playerScript.anime.Play("Idle");
    }
}
