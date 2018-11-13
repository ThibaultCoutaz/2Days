using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScore : HUDElement {

    public Text scoreText;
    public Text BallThrow;

    public void EditScore(float score)
    {
        scoreText.text = score.ToString();
    }

    public void EditballThrow(float ball)
    {
        BallThrow.text = ball.ToString();
    }
}
