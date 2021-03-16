using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Scripts : MonoBehaviour
{
    public void Gameobject_SetActive_false(GameObject obj)
    {
        obj.SetActive(false);
    }
    
    public void Gameobject_SetActive_true(GameObject obj)
    {
        obj.SetActive(true);
    }
}
