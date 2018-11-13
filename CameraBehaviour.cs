using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Vector2 limitY;
    
    private Vector3 firstpoint; //change type on Vector3
    private Vector3 secondpoint;
    private float xAngle = 0.0f; //angle for axes x for rotation
    private float yAngle = 0.0f;
    private float xAngTemp = 0.0f; //temp variable for angle
    private float yAngTemp = 0.0f;
    

    // Use this for initialization
    void Start () {
        HUDManager.Instance.InitJauge();
	}
    
    private int fingerTOUCHMove = -1;

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.IsPause && !GameManager.Instance.IsEnd)
            if (Input.touchCount > 0)
            {
                //Touch began, save position
                for (int i = 0; i < Input.touches.Length; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        if (Input.GetTouch(i).position.x < Screen.width / 2)
                        {
                            firstpoint = Input.GetTouch(i).position;
                            fingerTOUCHMove = i;
                            xAngTemp = xAngle;
                            yAngTemp = yAngle;
                            break;
                        }
                    }
                }

                    //Move finger by screen
                if(Input.touchCount > fingerTOUCHMove && fingerTOUCHMove != -1)
                    if (Input.GetTouch(fingerTOUCHMove).phase == TouchPhase.Moved)
                    {
                        secondpoint = Input.GetTouch(fingerTOUCHMove).position;
                        //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
                        xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
                        yAngle = yAngTemp - (secondpoint.y - firstpoint.y) * 90.0f / Screen.height;
                        //Rotate camera
                        this.transform.rotation = Quaternion.Euler(Mathf.Clamp(yAngle, limitY.x, limitY.y), xAngle, 0.0f);
                    }

                for (int i = 0; i < Input.touches.Length; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Ended)
                    {
                        if (i == fingerTOUCHMove)
                            fingerTOUCHMove = -1;
                    }
                }
            }
    }

   
}
