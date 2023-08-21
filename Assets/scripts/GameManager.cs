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
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        hiscoretext.text = LoadHiscore().ToString();
        Gameover.alpha = 0f;
        Gameover.interactable = false;
        board.clearboard();
        board.createTile();
        board.createTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        Gameover.interactable = true;

        StartCoroutine(Fade(Gameover, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoretext.text = score.ToString();

        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if (score > hiscore) {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }
    int loadhisscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }


}
