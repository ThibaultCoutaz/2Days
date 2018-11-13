using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class HUDManager : Singleton<HUDManager>
{
    #region BaseClass
    protected HUDManager() { }

    Dictionary<GameTennis.UI_Types, HUDElement> elements;

    //public Sprite getSpriteByName(string name)
    //{
    //    Sprite item_sprite;
    //    if (_items_sprites.TryGetValue(name, out item_sprite))
    //        return item_sprite;
    //    else
    //        return null;
    //}

    public void registerElement(GameTennis.UI_Types key, HUDElement element)
    {
        if (key == GameTennis.UI_Types.NULL)
            return;

        if (elements == null)
            elements = new Dictionary<GameTennis.UI_Types, HUDElement>();

        if (!elements.ContainsKey(key))
            elements.Add(key, element);
        else
            Debug.LogError("HUDManager already contains key " + key);

    }

    void disableElement(GameTennis.UI_Types key, HUDElement element)
    {
        if (true)
            element.displayGroup(false, .0f, false, false);
    }

    //to get the gameObject we want from the dictionnary
    public GameObject getElement(GameTennis.UI_Types key)
    {
        HUDElement obj;
        if (elements.TryGetValue(key, out obj))
        {
            return obj.gameObject;
        }
        Debug.LogError("No Element with the Type :" + key);
        return null;
    }

    public void DisableAll()
    {
        foreach (KeyValuePair<GameTennis.UI_Types, HUDElement> element in elements)
        {
            disableElement(element.Key, element.Value);
        }
    }

    #endregion

    #region Tennis
    public void InitJauge()
    {
        HUDElement PowerHUD;
        if (elements.TryGetValue(GameTennis.UI_Types.PowerHUD, out PowerHUD))
        {
            ((FireButton)PowerHUD).Init();
        }
    }

    public void EditScore(float score)
    {
        HUDElement ScoreHUD;
        if (elements.TryGetValue(GameTennis.UI_Types.ScoreHUD, out ScoreHUD))
        {
            ((HUDScore)ScoreHUD).EditScore(score);
        }
    }

    public void EditBallThrow(float ball)
    {
        HUDElement ScoreHUD;
        if (elements.TryGetValue(GameTennis.UI_Types.ScoreHUD, out ScoreHUD))
        {
            ((HUDScore)ScoreHUD).EditballThrow(ball);
        }
    }

    public void SetChrono(float timer)
    {
        HUDElement Timer;
        if (elements.TryGetValue(GameTennis.UI_Types.TimerHUD, out Timer))
        {
            ((HUDTimer)Timer).EditChrono(timer);
        }
    }

    public void DisplayEndGame(int nbBallUse,int score)
    {
        HUDElement EndGame;
        if (elements.TryGetValue(GameTennis.UI_Types.EndGameHUD, out EndGame))
        {
            ((HUDEndGame)EndGame).Init(nbBallUse,score);
            EndGame.displayGroup(true);
        }
    }
    #endregion
}


