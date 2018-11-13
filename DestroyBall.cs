using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ball")
        {
            GameManager.Instance.DestroyBall(collision.gameObject.GetComponent<BehaviourBall>().IDinList);
        }
    }
}
