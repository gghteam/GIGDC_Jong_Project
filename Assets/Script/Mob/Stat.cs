using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour, IDamageable
{
	public int hp;
	public int exp;
	public int atk;
	public int def;

    private int reqDamage;

    private DamageTextPoolManager textPoolManager = null;

    private void Awake()
    {
        textPoolManager = GetComponent<DamageTextPoolManager>();
    }

    public void OnDamage(int damage)
    {
        reqDamage = damage - def;

        hp -= reqDamage;

        Debug.Log(reqDamage);
        textPoolManager.GetDamageText(transform.position, reqDamage);
    }
}
