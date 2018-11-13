using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HUDElement : MonoBehaviour
{

    protected Text _text;
    protected Image _image;
    protected CanvasGroup _group;

    public GameTennis.UI_Types type;


    protected virtual void Awake()
    {
        _text = gameObject.GetComponent<Text>();
        _image = gameObject.GetComponent<Image>();
        _group = gameObject.GetComponent<CanvasGroup>();
        HUDManager.Instance.registerElement(type, this);
    }

    public float getWidth()
    {
        return GetComponent<RectTransform>().sizeDelta.x;
    }

    public void setGameTime(int hours, int minutes)
    {
        _text.text = string.Format("{0}:{1}", hours.ToString("00"), minutes.ToString("00"));
    }

    /*public void setChrono(double time)
    {
        float minutes = Mathf.Floor((float)time / 60);
        float seconds = Mathf.Floor((float)time % 60);

        _text.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }*/


    /*public void smoothAnimation(float x, float y, float z, float time, float delay = .0f)
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            "x", x,
            "y", y,
            "z", z,
            "time", time,
            "delay", delay,
            "easetype", iTween.EaseType.easeInOutExpo));
    }


    public void smoothScale(float x, float y, float z, float time, float delay = .0f)
    {
        iTween.ScaleTo(gameObject, iTween.Hash(
            "x", x,
            "y", y,
            "z", z,
            "time", time,
            "delay", delay,
            "easetype", iTween.EaseType.easeInOutExpo));
    }*/


    public void displayGroup(bool show = true, float time = 0.0f, bool interactable = true, bool block = true)
    {
        if (_group != null)
        {
            _group.interactable = interactable;
            _group.blocksRaycasts = block;

            if (time == .0f)
            {
                _group.alpha = show ? 1.0f : 0.0f;
            }
            else
            {
                /*iTween.ValueTo(gameObject, iTween.Hash(
                    "from", show ? .0f : 1.0f,
                    "to", !show ? .0f : 1.0f,
                    "time", time,
                    "onupdate", "changeGroupAlpha"));*/
                _group.alpha = show ? 0.0f : 1.0f;
            }
        }
    }


    private void changeGroupAlpha(float value)
    {
        if (_group != null)
            _group.alpha = value;
    }


    public void setText(string s)
    {
        if (_text != null)
            _text.text = s;
    }


    public Vector3 getPos()
    {
        return gameObject.transform.position;
    }

    public void SetImage(Sprite s)
    {
        if (_image != null)
            _image.sprite = s;
    }
}


