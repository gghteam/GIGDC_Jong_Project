using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lord_Button : MonoBehaviour
{
    public void Lord_File(int lord_number)
    {
        switch(lord_number)
        {
            case 1:
                Debug.Log("1번파일 로드");
                break;
            case 2:
                Debug.Log("2번파일 로드");
                break;
            case 3:
                Debug.Log("3번파일 로드");
                break;
        }
    }
}
