using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameManager GM;
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("실행 중");
        //GM.check = true;
    }
}
