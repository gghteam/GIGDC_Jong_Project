using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BossBoy : MonoBehaviour
{
    [SerializeField]
    private float patternDelay = 0f;
    private float time = 0f;

    private GameObject Missile;

    public int bossPattern = 0;
    private bool isPattern = false;
    public bool phase2 = false;

   
    // Start is called before the first frame update
    void Start()
    {
        
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
                    ShootBullet();
                    Debug.Log("공격2");
                    isPattern = false;
                    break;
                case 3:
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
    void ShootBullet()
    {
        for (int i = 0; i < 360; i += 36)
        {
            Missile = MissileSpawn.instance.GetMissile(this.transform.position);
            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            Missile.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
}
