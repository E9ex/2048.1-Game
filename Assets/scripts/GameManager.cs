using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup Gameover;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI hiscoretext;
    private int score;

    private void Start()
    {
       newgame();
    }
    public void newgame()
    {
        setscore(0);
        hiscoretext.text = loadhisscore().ToString();
        Gameover.alpha = 0f;
        Gameover.interactable = false;
        board.clearboard();
        board.createTile(); 
        board.createTile();
        board.enabled = true;
    }

    public void gameover()
    {
        board.enabled = false;
        Gameover.interactable = true;
        StartCoroutine(fade(Gameover, 1f,1f));
    }

    private IEnumerator fade(CanvasGroup canvasGroup,float to,float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to,elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        setscore(score+points);
    }

    private void setscore(int score)
    {
        this.score = score;
        scoretext.text = score.ToString();
    }

    void savescore()
    {
        int hiscore = loadhisscore();
        if (score>hiscore)
        {
            PlayerPrefs.SetInt("hiscore",score);
        }
        
    }

    int loadhisscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }


}
