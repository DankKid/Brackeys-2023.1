using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    int p1Money, p1Wave, p2Money, p2Wave, p3Money, p3Wave;
    public int currentPhase, currentDollar;

    [SerializeField] TMP_Text currentMoney, finalScore, bestScoreText;
    [SerializeField] Image currentCurrency;
    [SerializeField] List<Sprite> currencies = new List<Sprite>();
    [SerializeField] Light globalLight;
    [SerializeField] GameObject gameOverScreen;

    Color zombieLight = new Color(0,202,255,255);
    Color machineLight = new Color(255,89,0,255);
    Color defaultLight = new Color(255, 255, 255, 255);


    public void NextPhase()
    {
        if(currentPhase < 3)
        {
            currentCurrency.sprite = currencies[currentPhase - 1];
            currentMoney.text = "0";
            currentPhase++;
        }
        else
        {
            p3Money = currentDollar;
            currentDollar = 0;
            gameOver();
        }


        if(currentPhase == 2)
        {
            globalLight.color = zombieLight;
            p1Money = currentDollar;
            currentDollar = 0;

        }else if(currentPhase == 3)
        {
            globalLight.color = machineLight;
            p2Money = currentDollar;
            currentDollar = 0;

        }


    }

    void gameOver()
    {
        calculateScore();
        globalLight.color = defaultLight;
        gameOverScreen.SetActive(true);
    }

    public void getMoney(int value)
    {
        currentDollar += value;
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
