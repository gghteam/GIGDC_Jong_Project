using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBoy : MonoBehaviour
{
    [SerializeField]
    private float patternDelay = 0f;
    private float time = 0f;
    private GameObject Missile;
    public int bossPattern = 0;
    private bool isPattern = false;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (patternDelay < time)
        {
            isPattern = true;
            time = 0f;
            bossPattern++;
        }
        if (bossPattern == 1&& isPattern)
        {
            Invoke("ShootMissile", patternDelay);
            isPattern = false;
        }
        if (bossPattern > 4)
        {
            bossPattern = 0;
        }
    }
    void ShootMissile()
    {
        Missile = MissileSpawn.instance.GetMissile(this.transform.position);

    }
}
