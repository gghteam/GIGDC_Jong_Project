using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TpBar tpBar = null;

    [SerializeField]
    private float changeTP = 1f;

    private void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            tpBar.UpdateTPBar(-changeTP * Time.deltaTime);
        }
    }
}
