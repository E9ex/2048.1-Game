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
            movetiles(Vector2Int.up,0,1,1,1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movetiles(Vector2Int.down,0,1,Grid.height-2,-1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movetiles(Vector2Int.left,1,1,0,1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movetiles(Vector2Int.right,Grid.height-2,-1,0,1);
        }
    }

    private void movetiles(Vector2Int direction,int starx,int incrementx,int stary,int incrementy)
    {
        for (int x = starx; x>=0&& x < Grid.width; x+=incrementx)
        {
            for (int y = stary; y < Grid.height; y+=incrementy)
            {
                tilecell cell = Grid.getcell(x, y);
                if (cell.occupied)
                {
                    movetile(cell.Tile,direction);
                }
            }
        }
    }
    void movetile(tile Tile ,Vector2Int direction)
    {
        tilecell newscell = null;
        tilecell adjacent = Grid.getadjacent(Tile.cell,direction);
        while (adjacent!=null)
        {
            if (adjacent.occupied)
            {
                break;
            }
            newscell = adjacent;
            adjacent = Grid.getadjacent(adjacent,direction); 
        }
    }
}
