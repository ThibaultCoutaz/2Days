using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDEndGame : HUDElement {

    public Text scoreText;
    public Text scoreTotalText;
    public Text scoreCurrentBest;

    public GameObject medalScore;
    public GameObject medalScoreTotal;
    public GameObject medalScoreCurrent;

    public Button[] listDesactivateButton;

    void Start()
    {
        medalScore.SetActive(false);
        medalScoreTotal.SetActive(false);
    }

	
    public void Init(int nbBallUse, int score)
    {
        for(int i=0; i< listDesactivateButton.Length; i++)
        {
            listDesactivateButton[i].interactable = false;
        }

        scoreText.text = score.ToString();

        int currentScoreTotal = score - nbBallUse;
        if (currentScoreTotal < 0)
            currentScoreTotal = 0;

        scoreTotalText.text = currentScoreTotal.ToString();

        int previousScore = PlayerPrefs.GetInt("HightScore");
        if (previousScore < score)
        {
            medalScore.SetActive(true);
            PlayerPrefs.SetInt("HightScore", score);
        }

        int previousScoreTotal = PlayerPrefs.GetInt("ScoreTotal");
        if(previousScoreTotal < currentScoreTotal)
        {
            medalScoreTotal.SetActive(true);
            PlayerPrefs.SetInt("ScoreTotal", currentScoreTotal);
        }

        int previousCurrentBest = PlayerPrefs.GetInt("CurrentScore");
        if (previousCurrentBest < score)
        {
            medalScoreCurrent.SetActive(true);
            PlayerPrefs.SetInt("CurrentScore", score);
        }
        scoreCurrentBest.text = PlayerPrefs.GetInt("CurrentScore").ToString();
    }

    public void BackToMenu()
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
