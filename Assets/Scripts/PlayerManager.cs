using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    int p1Money, p1Wave, p2Money, p2Wave, p3Money, p3Wave;
    public int currentPhase;

    [SerializeField] TMP_Text currentMoney, finalScore, bestScoreText;
    [SerializeField] Image currentCurrency;
    [SerializeField] List<Sprite> currencies = new List<Sprite>();
    void NextPhase()
    {
        if(currentPhase < 3)
        {
            currentCurrency.sprite = currencies[currentPhase - 1];
            currentMoney.text = "0";
            currentPhase++;
        }
        else
        {
            gameOver();
        }
    }

    void gameOver()
    {

    }

    void calculateScore()
    {
        float bestScore = PlayerPrefs.GetFloat("bestScore");
        float score;
        score = (p1Money + p1Wave * 1000) + (p2Money + p2Wave * 1000) + (p3Money + p3Wave * 1000);
        finalScore.text = "Final Score: " + score;
        if(score > bestScore)
        {
            PlayerPrefs.SetFloat("bestScore", score);
        }
        bestScoreText.text = "Best Score: " + bestScore;
    }
}
