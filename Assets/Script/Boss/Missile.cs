using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    GameObject target;
    [SerializeField]
    GameObject missilePrepab;
    [SerializeField]
    float speed = 2f, rotSpeed = 2f, missileDeleteTime = 0f;

    Quaternion rotTarget;
    Vector3 dir;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame 시름
    void Update()
    {
        GuidMissile();
        Invoke("DeleteMissile", missileDeleteTime);
    }
    void GuidMissile()
    {
        dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * rotSpeed);
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }
    void DeleteMissile()
    {
        this.gameObject.SetActive(false);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DeleteMissile();
        }
    }
}
