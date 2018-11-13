using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireButton : HUDElement, IPointerUpHandler, IPointerDownHandler
{

    public GameObject parentjaugePower;
    public Image jaugePower;
    public GameObject parentjaugePrec;
    public Image jaugePrec;

    public RectTransform JaugeLimitPrec;

    #region variable Bool
    private bool pressingPower = false;
    private bool pressingPrec = false;

    private bool wayBack = false;
    private bool canShoot = true;
    #endregion

    private Vector2 posRandomLimit;
    private float posLimit;
    private float largeurPrec;
    private float largeurLimit;

    private Vector2 LimitWhereGood;

    public void Init()
    {
        jaugePower.fillAmount = 0;
        jaugePrec.fillAmount = 0;


        #region initPosLimit
        largeurPrec = jaugePrec.rectTransform.rect.width/2;
        largeurLimit = JaugeLimitPrec.rect.width / 2;
        

        posRandomLimit = new Vector2(-largeurPrec / 5, largeurPrec);
        
        parentjaugePower.SetActive(false);
        parentjaugePrec.SetActive(false);
        #endregion

    }

    // Update is called once per frame
    void Update () {
        if(!GameManager.Instance.IsPause && !GameManager.Instance.IsEnd)
            if (pressingPower)
            {
                if (jaugePower.fillAmount < 1)
                    jaugePower.fillAmount += Time.deltaTime;
            }
            else if (pressingPrec)
            {
                if (!wayBack && jaugePrec.fillAmount < 1)
                {
                    jaugePrec.fillAmount += Time.deltaTime;
                }
                else
                {
                    if (!wayBack)
                        wayBack = true;

                    if (jaugePrec.fillAmount > 0)
                    {
                        jaugePrec.fillAmount -= Time.deltaTime;
                    }
                    else
                    {
                        jaugePower.fillAmount = 0;
                        jaugePrec.fillAmount = 0;
                        parentjaugePower.SetActive(false);
                        parentjaugePrec.SetActive(false);
                        wayBack = false;
                        pressingPrec = false;
                        GameManager.Instance.sendBall(jaugePower.fillAmount,false);
                    }
                }
            }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canShoot && !GameManager.Instance.IsPause && !GameManager.Instance.IsEnd)
            if (!pressingPower && !pressingPrec)
            {
                parentjaugePower.SetActive(true);
                pressingPower = true;
            }
            else if (pressingPrec)
            {
                canShoot = false;
                Invoke("Restart", 0.5f);
                wayBack = false;
                pressingPrec = false;
                #region check Prec
                float amount = jaugePrec.fillAmount;
                bool goodShoot = false;

                if (amount >= LimitWhereGood.x && amount <= LimitWhereGood.y)
                    goodShoot = true;
                #endregion

                GameManager.Instance.sendBall(jaugePower.fillAmount, goodShoot);
            }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!GameManager.Instance.IsPause && !GameManager.Instance.IsEnd)
            if (pressingPower)
            {
                pressingPower = false;
                pressingPrec = true;

                float posX = Random.Range(posRandomLimit.x, posRandomLimit.y);

                if (posX > largeurPrec - largeurLimit)
                    posX -= largeurLimit;

                JaugeLimitPrec.localPosition = new Vector3(posX, 0, 0);
                posLimit = largeurPrec + posX;

                //JaugeLimitPrec.gameObject.SetActive(true);

                Vector2 tmp = new Vector2(posLimit - largeurLimit, posLimit + largeurLimit);

                LimitWhereGood = new Vector2(tmp.x / (largeurPrec * 2), tmp.y / (largeurPrec * 2));

                parentjaugePrec.SetActive(true);
            }
    }

    private void Restart()
    {
        jaugePower.fillAmount = 0;
        jaugePrec.fillAmount = 0;
        parentjaugePower.SetActive(false);
        parentjaugePrec.SetActive(false);
        canShoot = true;
    }
}
