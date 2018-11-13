using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourBall : MonoBehaviour
{ 
    public int IDinList = -1;
    public float timeBeforeDestroy = 5;

    void OnEnable()
    {
        Invoke("AutoDestruction", timeBeforeDestroy);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    private void AutoDestruction()
    {
        GameManager.Instance.DestroyBall(IDinList);
    }
}
