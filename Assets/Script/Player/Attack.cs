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

    //첫번째 공격 애니메이션이 끝나면 실행되는 함수
    public void Attack1()
    {
        if (Attacking)
        {
            anime.SetInteger("Stack", 2);

        }
        else
        {
            anime.SetBool("EndAttack", true);
        }     
        Attacking = false;
    }
    //두번째 공격 애니메이션이 끝나면 실행되는 함수
    public void Attack2()
    {
        if (Attacking)
        {
            anime.SetInteger("Stack", 3);
        }
        else
        {
            anime.SetBool("EndAttack", true);
        }  
        Attacking = false;
    }
    //세번째 공격 애니메이션이 끝나면 실행되는 함수
    public void Attack3()
    {
        anime.SetBool("EndAttack", true);
        Attacking = false;
    }
    void Func()
    {
        //공격이 끝나면 실행
        if(anime.GetBool("EndAttack"))
        {
            StartCoroutine(Change());
            anime.SetInteger("Stack", 0);
        }

        //달리면서 처음 공격할 때 실행
        if (anime.GetBool("IsRunning"))
        {
            if (Input.GetKeyDown(KeyCode.Z) && anime.GetInteger("Stack") == 0)
            {
                anime.Play("RunAttack1");
                anime.SetInteger("Stack", 1);
            }
        }
        else if(Input.GetKey(KeyCode.UpArrow))  // 윗 방향키를 누르고 공격할 시 실행
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("윗 공격");
                //윗 공격 실행
            }
        }
        else if(Input.GetKey(KeyCode.DownArrow)) // 아랫 방향키를 누르고 공격할 시 실행
        {
            if (Input.GetKeyDown(KeyCode.Z) && anime.GetBool("IsJumping"))
            {
                Debug.Log("아랫 공격");
                // 아랫 공격 실행
            }
        }
        else // 걷거나 움직이지 않을 때 처음 공격하면 실행
        {
            if (Input.GetKeyDown(KeyCode.Z) && anime.GetInteger("Stack") == 0)
            {
                anime.Play("IdleAttack1");
                anime.SetInteger("Stack", 1);
            }
        }

        // 처음 공격이 아닐시에 실행
        if (Input.GetKeyDown(KeyCode.Z) && anime.GetInteger("Stack") != 0)
        {
            Attacking = true;
        }
    }
    void Update()
    {
        Func();
    }

    //잠시 딜레이만 주는 코루틴
    private IEnumerator Change()
    {
        yield return new WaitForSeconds(0.01f);
        anime.SetBool("EndAttack", false);
    }
}
