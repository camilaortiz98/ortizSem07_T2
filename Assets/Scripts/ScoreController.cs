using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public Text liveText;

    private int Score = 0;
    private int lifes = 3;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Puntaje: " + Score;
        PrintLifes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return Score;
    }
    public void PlusScore(int score)
    {
        Score += score;
        scoreText.text = "Puntaje: " + Score;
    }

    public void LoseLife()
    {
        lifes -= 1;
    }

    public int GetLifes()
    {
        return lifes;
    }

    private void PrintLifes()
    {
        var text = "Vidas: ";
        for(var i = 0; i < lifes; i++)
        {
            text += "I";
        }
        liveText.text = text;
    }
}
