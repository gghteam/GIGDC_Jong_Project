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
    private GameObject dashAttackObj;
    private BoxCollider2D dashAttackObjRig;
    private bool isPattern = false;
    private GameObject Missile;
    private float time = 0f;
    private SpriteRenderer dashRenderer;


    // Start is called before the first frame update
    void Start()
    { 
        dashAttackObjRig = dashAttackObj.GetComponent<BoxCollider2D>();
        dashRenderer = dashAttackObj.GetComponent<SpriteRenderer>();
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
                        ShootMissile360();
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
                    if (phase2)
                    {
                        StartCoroutine(DashAttack_2());
                        isPattern = false;
                    }
                    //패턴 1 미사일 발사
                    else
                    {
                        StartCoroutine(DashAttack());
                        isPattern = false;
                    }
                    break;
                case 3:
                    
                    Invoke("DashAttack", 0.5f);
                    
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
    void ShootMissile360()
    {
        for (int i = 0; i < 360; i += 36)
        {
            Missile = MissileSpawn.instance.GetMissile(this.transform.position);
            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            Missile.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    IEnumerator DashAttack()
    {
        dashAttackObj.transform.position = targetObj.transform.position;
        dashAttackObj.transform.Rotate(0, 0, Random.Range(45, 130));
        yield return new WaitForSeconds(1f);
        dashAttackObj.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        dashRenderer.color = Color.red;
        dashAttackObjRig.enabled = true;
        yield return new WaitForSeconds(0.2f);
        dashRenderer.color = Color.white;
        dashAttackObjRig.enabled = false;
        dashAttackObj.SetActive(false);
    }
    IEnumerator DashAttack_2()
    {
        StartCoroutine(DashAttack());
        yield return new WaitForSeconds(1f);
        StartCoroutine(DashAttack());
        yield return new WaitForSeconds(1f);
        StartCoroutine(DashAttack());
        yield return new WaitForSeconds(1f);
    }
}
