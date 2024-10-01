using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] protected bool state = true;

    protected void OnEnable()
    {
        Debug.Log("이벤트 발생");
    }

    protected void OnExecute()
    {
        state = true;
    }

    protected void OnStop()
    {
        state = false;
    }

    protected void OnDisable()
    {
        Debug.Log("이벤트 해제");
    }
}
