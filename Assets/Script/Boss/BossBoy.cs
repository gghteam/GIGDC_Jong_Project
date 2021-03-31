using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BossBoy : MonoBehaviour
{
    public int bossPattern = 0;
    public bool phase2 = false;
    public bool isDashAttack = false;
    public GameObject targetObj;
    public bool a = true;

    [SerializeField]
    private float patternDelay = 0f;
    [SerializeField]
    private GameObject DashAttackObj;
    private BoxCollider2D DashAttackObjRig;
    private bool isPattern = false;
    private GameObject Missile;
    private float time = 0f;


    // Start is called before the first frame update
    void Start()
    { 
        DashAttackObjRig = DashAttackObj.GetComponent<BoxCollider2D>();
        StartCoroutine("test");
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        //보스 패턴 증가
        if (patternDelay < time)
        {
            isPattern = true;
            time = 0f;
            bossPattern++;
        }
        
        if (isPattern)
        {
            switch (bossPattern)
            {
                case 1:
                    //패턴 1 페이즈2
                    if (phase2)
                    {
                        Invoke("ShootMissile", 0.1f);
                        Invoke("ShootMissile", 0.3f);
                        Invoke("ShootMissile", 0.5f);
                        Invoke("ShootMissile", 0.7f);
                        isPattern = false;
                    }
                    //패턴 1 미사일 발사
                    else
                    {
                        ShootMissile();
                        isPattern = false;
                    }
                    break;
                case 2:
                    ShootBullet360();
                    Debug.Log("공격2");
                    isPattern = false;
                    break;
                case 3:
                    //DashAttackObj.SetActive(true);
                    //Invoke("DashAttack", 0.5f);
                    Debug.Log("공격3");
                    isPattern = false;
                    break;
                case 4:
                    Debug.Log("공격4");
                    isPattern = false;
                    break;

            }

        }

        //패턴 4까지 다 돌면 0으로 초기화
        if (bossPattern > 4)
        {
            bossPattern = 0;
        }
    }
    void ShootMissile()
    {
        Missile = MissileSpawn.instance.GetMissile(this.transform.position);

    }
    void ShootBullet360()
    {
        for (int i = 0; i < 360; i += 36)
        {
            Missile = MissileSpawn.instance.GetMissile(this.transform.position);
            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            Missile.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    void DashAttack()
    {
        DashAttackObjRig.enabled = true;

    }
}
