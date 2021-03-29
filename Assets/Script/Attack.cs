using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator anime;
    public int a= 0;
    private bool anime1 = false; private bool anime2 = false;
    public void Attack2()
    {
        if(anime2)
        {
            anime.SetInteger("Stack", 3);
        }
    }
    public void Attack3()
    {
        
    }
    void Func()
    {
        if(Input.GetKeyDown(KeyCode.Z) && anime.GetInteger("Stack") == 0)
        {
            anime.SetInteger("Stack", 1);
        }
        if(anime.GetInteger("Stack") == 1)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                anime1 = true;
            }
        }
        if(anime.GetInteger("Stack") == 2)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                anime2 = true;
            }
        }

        if (anime1 && a == 1)
        {
            anime.SetInteger("Stack", 2);
        }

        if (anime2 && a == 2)
        {
            anime.SetInteger("Stack", 3);
        }

        if(a == 3)
            anime.SetInteger("Stack", 0); anime1 = false; anime2 = false; a = 0;
    }
    void Update()
    {
        Func();
    }
}
