using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator anime;
    public int a= 0;
    private float time;
    private bool Attacking = false;
    private bool anime1 = false; private bool anime2 = false;

    public void Attack1()
    {
        if (Attacking)
            anime.SetInteger("Stack", 2);
        else
            anime.SetBool("EndAttack", true);
        Attacking = false;
    }
    public void Attack2()
    {
        if (Attacking)
            anime.SetInteger("Stack", 3);
        else 
            anime.SetBool("EndAttack", true);
        Attacking = false;
    }
    public void Attack3()
    {
        anime.SetBool("EndAttack", true);
        Attacking = false;
    }
    void Func()
    {
        if(anime.GetBool("EndAttack"))
        {
            StartCoroutine(Change());
            anime.SetInteger("Stack", 0);
        }
        if (Input.GetKeyDown(KeyCode.Z) && anime.GetInteger("Stack") != 0)
        {
            Attacking = true;
        }
        if (anime.GetBool("IsRuning"))
        {
            if (Input.GetKeyDown(KeyCode.Z) && anime.GetInteger("Stack") == 0)
            {
                anime.Play("gRunAttack1");
                anime.SetInteger("Stack", 1);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z) && anime.GetInteger("Stack") == 0)
            {
                anime.Play("gIdleAttack1");
                anime.SetInteger("Stack", 1);
            }
        }

    }
    void Update()
    {
        Func();
    }

    private IEnumerator Change()
    {
        yield return new WaitForSeconds(0.01f);
        anime.SetBool("EndAttack", false);
    }
}
