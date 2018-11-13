using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDOption : HUDElement {

    public GameObject ButtonOption;
    public GameObject optionpanel;

    public Button[] listButtonDesactivate;

    public Image buttonSound;
    public Image buttonEffect;

    public Sprite soundActivate;
    public Sprite soundDesactivate;

    public Sprite effectActivate;
    public Sprite effectDesactivate;

    void Start()
    {
        bool tmp = SoundManager.Instance.GetBoolSound();
        if (tmp)
            buttonSound.sprite = soundActivate;
        else
            buttonSound.sprite = soundDesactivate;
        
        tmp = SoundManager.Instance.GetBoolEffect();
        if (tmp)
            buttonEffect.sprite = effectActivate;
        else
            buttonEffect.sprite = effectDesactivate;
    }

    public void DisplayOption(bool display)
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
        ButtonOption.SetActive(!display);
        optionpanel.SetActive(display);
        GameManager.Instance.IsPause = display;

        for (int i = 0; i < listButtonDesactivate.Length; i++)
            listButtonDesactivate[i].interactable = !display;
    }
    
    public void ManageSound()
    {
        bool tmp = SoundManager.Instance.ManageSound();
        if (tmp)
            buttonSound.sprite = soundActivate;
        else
            buttonSound.sprite = soundDesactivate;
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
    }

    public void ManageEffect()
    {
        bool tmp = SoundManager.Instance.ManageEffect();
        if (tmp)
            buttonEffect.sprite = effectActivate;
        else
            buttonEffect.sprite = effectDesactivate;
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
    }

    public void ExitGame()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
        Application.Quit();
    }

    public void ReturnMenu()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
        Destroy(SoundManager.Instance.gameObject);
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
        SceneManager.LoadScene("Tennis");
    }
}
