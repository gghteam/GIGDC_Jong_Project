using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRobot : MonoBehaviour
{
    Rigidbody2D rigid;

    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity += new Vector2(speed,0);
        time += Time.deltaTime;
        if (lifeTime < time)
        {
            Destroy(this.gameObject);
        }
    }
}
