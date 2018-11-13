using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menumanager : MonoBehaviour {

    public string nameScene;
    public Text score;
    public Text ScoreTotal;
    public Text CurrentScore;

    public GameObject menu;
    public GameObject Tutorial;
    public GameObject Credit;

    public Image ImageSound;
    public Image ImageEffect;

    public Sprite soundActivate;
    public Sprite soundDesactivate;
    public Sprite effectActivate;
    public Sprite effectDesactivate;

    public GameObject tutoNext;
    public GameObject tutoprevious;

	void Start()
    {
        if (PlayerPrefs.HasKey("HightScore"))
        {
            score.text = PlayerPrefs.GetInt("HightScore").ToString();
        }
        else
        {
            score.text = "0";
            PlayerPrefs.SetInt("HightScore", 0);
        }

        if (PlayerPrefs.HasKey("ScoreTotal"))
        {
            ScoreTotal.text = PlayerPrefs.GetInt("ScoreTotal").ToString();
        }
        else
        {
            ScoreTotal.text = "0";
            PlayerPrefs.SetInt("ScoreTotal", 0);
        }

        if (PlayerPrefs.HasKey("CurrentScore"))
        {
            CurrentScore.text = PlayerPrefs.GetInt("CurrentScore").ToString();
        }
        else
        {
            CurrentScore.text = "0";
            PlayerPrefs.SetInt("CurrentScore", 0);
        }

        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetInt("Sound", 1);

        int sound = PlayerPrefs.GetInt("Sound");
        if (sound >= 1)
            ImageSound.sprite = soundActivate;
        else
            ImageSound.sprite = soundDesactivate;

        if (!PlayerPrefs.HasKey("Effect"))
            PlayerPrefs.SetInt("Effect", 1);

        int effect = PlayerPrefs.GetInt("Effect");
        if (effect >=1)
            ImageEffect.sprite = effectActivate;
        else
            ImageEffect.sprite = effectDesactivate;

    }

    public void RestartCurrentBest()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.3f);
        CurrentScore.text = "0";
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    public void play()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton,0.5f);
        SceneManager.LoadScene(nameScene);
    }

    public void GoTutorial()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton,0.5f);
        menu.SetActive(false);
        Credit.SetActive(false);
        Tutorial.SetActive(true);
    }

    public void GoMenu()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
        menu.SetActive(true);
        Credit.SetActive(false);
        Tutorial.SetActive(false);
    }

    public void GoCredit()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
        menu.SetActive(false);
        Credit.SetActive(true);
        Tutorial.SetActive(false);
    }

    public void OpenLink(string link)
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
        Application.OpenURL(link);
    }

    public void ChangeSound()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
        bool sound = SoundManager.Instance.ManageSound();
        if (sound)
            ImageSound.sprite = soundActivate;
        else
            ImageSound.sprite = soundDesactivate; 
    }

    public void ChangeEffect()
    {
        SoundManager.Instance.playerSoundOneTime(GameTennis.Sound_Type.PressButton, 0.5f);
        bool effect = SoundManager.Instance.ManageEffect();
        if (effect)
            ImageEffect.sprite = effectActivate;
        else
            ImageEffect.sprite = effectDesactivate;
    }

    public void passNextTuto()
    {
        tutoNext.SetActive(true);
        tutoprevious.SetActive(false);
    }

    public void passpreviousTuto()
    {
        tutoNext.SetActive(false);
        tutoprevious.SetActive(true);
    }

}
