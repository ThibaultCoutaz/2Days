using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTarget : MonoBehaviour {

    public int scoreWinning = 1;
    public bool destroyBall = false;
    public bool stopParticules = false;
    private bool alreadyTouch = false;

    private bool indicatorUp = false;
    public GameObject helpIndicator;

    void Start()
    {
        GameManager.Instance.Registertarget(transform);
    }

    void Update()
    {
        if (indicatorUp)
            helpIndicator.transform.rotation = Camera.main.transform.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ball" && !alreadyTouch)
        {
            GameManager.Instance.EditScore(scoreWinning);
            alreadyTouch = true;
            if (destroyBall)
                GameManager.Instance.DestroyBall(collision.gameObject.GetComponent<BehaviourBall>().IDinList);

            if (stopParticules)
                GetComponent<ParticleSystem>().Stop();

            activateHelp(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball" && !alreadyTouch)
        {
            GameManager.Instance.EditScore(scoreWinning);
            alreadyTouch = true;
            if (destroyBall)
                GameManager.Instance.DestroyBall(other.gameObject.GetComponent<BehaviourBall>().IDinList);

            if (stopParticules)
                GetComponent<ParticleSystem>().Stop();

            activateHelp(false);
        }
    }

    public void activateHelp(bool activate)
    {
        helpIndicator.SetActive(activate);
        indicatorUp = activate;
        GameManager.Instance.RemoveTarget(transform);
    }

}
