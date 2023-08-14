using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard board;

    private void Start()
    {
       newgame();
    }
    public void newgame()
    {
        board.clearboard();
        board.createTile(); 
        board.createTile();
        board.enabled = true;
    }

    public void gameover()
    {
        board.enabled = false;
    }
}
