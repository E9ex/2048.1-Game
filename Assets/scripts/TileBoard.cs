using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public tile tileprfab;
    public tilestats[] tileStates;
    private TileGrid Grid;
    private List<tile> _tiles;

    private void Awake()
    {
        Grid = GetComponentInChildren<TileGrid>();
        _tiles = new List<tile>(16);
    }
    private void Start()
    {
        createTile();
        createTile();
    }
    void createTile()
    {
       tile Tile= Instantiate(tileprfab, Grid.transform);
       Tile.setstate(tileStates[0],2);
       Tile.Spawn(Grid.getrandomemptycell());
       _tiles.Add(Tile);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {

        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            
        }
    }

    private void movetiles(Vector2Int direction,int starx,int incrementx,int stary,int incrementy)
    {
        for (int x = starx; x>=0&& x < Grid.width; x+=incrementx)
        {
            for (int y = stary; y < Grid.height; y+=incrementy)
            {
                
            }
        }
    }
}
