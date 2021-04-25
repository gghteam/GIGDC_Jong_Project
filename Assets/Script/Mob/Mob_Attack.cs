using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Attack : MonoBehaviour
{
    private Stat stat = null;

    private void Start()
    {
        stat = GetComponent<Stat>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            IDamageable target = col.transform.GetComponent<IDamageable>();

            if (target != null)
            {
                target.OnDamage(stat.atk);
            }
        }
    }
}
