using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drons : MonoBehaviour
{
    public float Range;
    public float dronSpeed = 0f;
    public Animator anime;

    private float time1 = 0;
    private float time2 = 0;
    private float time3 = 0;
    
    static bool Attack1 = false;
    static bool Attack2 = false;
    static bool Attack3 = false;
    void Start()
    {

    }

    void Func()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !Attack1 && !Attack3)
        {
            Attack1 = true;
            // 공격 1 코드;
            anime.SetBool("Attack1", true);
            Debug.Log("공격 1");
        }
        if (Attack1)
        {
            time1 += Time.deltaTime;
        }
        if (time1 < 2f && time1 > 1f && Input.GetKeyDown(KeyCode.Z) && !Attack2 && !Attack3)
        {
            Attack2 = true; time1 = 0;
            //공격 2 코드
            anime.SetBool("Attack2", true);
            Debug.Log("공격 2");
        }
        if (Attack2)
        {
            time2 += Time.deltaTime;
        }
        if (time2 < 2f && time2 > 1f && Input.GetKeyDown(KeyCode.Z) && Attack2 && !Attack3)
        {
            Attack3 = true; time2 = 0;
            //공격 3 코드
            anime.SetBool("Attack3", true);
            Debug.Log("공격 3");
        }
        if (Attack3)
        {
            time3 += Time.deltaTime;
        }
        if(Attack3 && time3 > 1f)
        {
            Attack1 = false; Attack2 = false; Attack3 = false;
            anime.SetBool("Attack1", false); anime.SetBool("Attack2", false); anime.SetBool("Attack3", false);
            time1 = 0; time2 = 0; time3 = 0;
        }

    }

    void Update()
    {
        Func();
        if(Input.GetKeyDown(KeyCode.Z))
        {
            //공격 1타

            
        }


        //z클릭
        //공격
        //0.15초 안에 또 누르면
        //공격 2타
        //또 0.15초 안에 누르면
        //공격 3타 
        //반복



















        //if (Input.GetKey(KeyCode.Z))
        //    transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(x + Range, y), 5f * Time.deltaTime);
        //x = Player.transform.position.x; y = Player.transform.position.y;

        //if (p.SR.flipX)
        //{
        //    Range = -temp;
        //}
        //else if(!p.SR.flipX)
        //{
        //    Range = temp;
        //}
        //if (!attack1 && !attack3)
        //{
        //    Dron1.transform.position = 
        //}
        
        //if(attack_ing)
        //{
        //    Cooldown1 += Time.deltaTime;
        //}

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    attack_ing = true;
        //    attack1 = true;
        //    //0.15초안에 공격키 한번더 누르면
        //    if (Cooldown1 < 0.15f&& Input.GetKeyDown(KeyCode.Z))
        //    {
        //        attack2 = true; attack1 = false; Cooldown1 = 0;
                
        //    }
        //}
        

        //if (attack1)
        //{
        //    //드론 1 날리는거
        //    Dron1.transform.position = Vector2.SmoothDamp(new Vector2(transform.position.x, transform.position.y), new Vector2(x + Range, y), ref velocity, dronSpeed * Time.deltaTime);
        //    //0.15초가 지나면 드론이 돌아옴
        //    if(Cooldown1 > 0.15f)
        //    {
        //        attack1 = false; attack_ing = false; Cooldown1 = 0; 
        //    }
        //}

        //Debug.Log(Cooldown1);
        //if (Input.GetKeyDown(KeyCode.Z) && !attack1)
        //{
        //    attack1 = true;
        //    Distance = Mathf.Abs(Player.transform.position.y - transform.position.y);
        //    Debug.Log("돌아가는 중");
        //}
        //Debug.Log(attack1);
        //Debug.Log(Distance);
        //if(attack1 == true)
        //{
        //    if(transform.position.y < Player.transform.position.y)
        //    {
        //        transform.Translate(new Vector3(y, Distance + Distance) * Time.deltaTime);
        //    }
        //    else
        //    {
        //        transform.Translate(new Vector3(x, - (Distance + Distance)) * Time.deltaTime);
        //    }
        //}
    }
}
