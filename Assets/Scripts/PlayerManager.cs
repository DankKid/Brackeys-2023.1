using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    int p0Money, p0Wave, p1Money, p1Wave;
    public int currentPhase, currentDollar;

    [SerializeField] TMP_Text currentMoney, finalScore, bestScoreText;
    [SerializeField] Image currentCurrency;
    [SerializeField] List<Sprite> currencies = new List<Sprite>();
    [SerializeField] UnityEngine.Rendering.Universal.Light2D globalLight;
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] List<SpriteRenderer> currentDefenderSprites;
    [SerializeField] List<Sprite> plantDefenderSprites, zombieDefenderSprites;




    Color zombieLight = new Color32(0,202,255,255);
    Color defaultLight = new Color32(255, 255, 255, 255);

    private void Start()
    {
        currentDollar = 0;
        currentPhase = 0;

        for (int i = 0; i < currentDefenderSprites.Count; i++)
        {
            currentDefenderSprites[i].sprite = plantDefenderSprites[i];
        }
    }



    public void NextPhase()
    {
        FindObjectsOfType<Placeable>().ToList().ForEach(p => Destroy(p.gameObject));
        FindObjectsOfType<Attacker>().ToList().ForEach(p => Destroy(p.gameObject));

        #region
        /*
        if(currentPhase < 2)
        {
            currentCurrency.sprite = currencies[currentPhase - 1];
            currentMoney.text = "0";
            currentPhase++;

            if(currentPhase == 1)
            {
                for (int i = 0; i < currentDefenderSprites.Count; i++)
                {
                    currentDefenderSprites[i].sprite = zombieDefenderSprites[i];
                }
            }



        }
        else
        {
            p1Money = currentDollar;
            currentDollar = 0;
            gameOver();
        }*/
        #endregion


        if (currentPhase == 0)
        {
            globalLight.color = zombieLight;
            p0Money = currentDollar;
            currentDollar = 0;
            currentPhase++;
            currentCurrency.sprite = currencies[1];
        }
        else
        {
            p1Money = currentDollar;
            currentDollar = 0;
            gameOver();
            
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
        currentMoney.text = currentDollar +"";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            getMoney(10);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            NextPhase();
        }
    }

    void calculateScore()
    {
        float bestScore = PlayerPrefs.GetFloat("bestScore");
        float score;
        score = (p0Money + p0Wave * 1000) + (p1Money + p1Wave * 1000);
        finalScore.text = "Final Score: " + score;
        if(score > bestScore)
        {
            PlayerPrefs.SetFloat("bestScore", score);
        }
        bestScoreText.text = "Best Score: " + bestScore;
    }


    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

}
