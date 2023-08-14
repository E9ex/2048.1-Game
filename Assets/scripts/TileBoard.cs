using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public GameManager GameManager;
    public tile tileprfab;
    public tilestats[] tileStates;
    private TileGrid Grid;
    private List<tile> _tiles;
    private bool waiting;

    private void Awake()
    {
        Grid = GetComponentInChildren<TileGrid>();
        _tiles = new List<tile>(16);
    }

    public void clearboard()
    {
        foreach (var tile in _tiles )
        {
            Destroy(tile.gameObject);
        }
        _tiles.Clear();
         
    }

    public  void createTile()
    {
       tile Tile= Instantiate(tileprfab, Grid.transform);
       Tile.setstate(tileStates[0],2);
       Tile.Spawn(Grid.getrandomemptycell());
       _tiles.Add(Tile);
    }

    private void Update()
    {
        if (!waiting)
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
       
    }

    private void movetiles(Vector2Int direction,int starx,int incrementx,int stary,int incrementy)
    {
        bool changed = false;
        for (int x = starx; x>=0&& x < Grid.width; x+=incrementx)
        {
            for (int y = stary; y>=0&&y < Grid.height; y+=incrementy)
            {
                tilecell cell = Grid.getcell(x, y);
                if (cell.occupied)
                {
                   changed= movetile(cell.Tile,direction);
                }
            }
        }
    }
    private bool movetile(tile Tile ,Vector2Int direction)
    {
        tilecell newscell = null;
        tilecell adjacent = Grid.getadjacent(Tile.cell,direction);
        while (adjacent!=null)
        {
            if (adjacent.occupied)
            {
                if (canMerge(Tile,adjacent.Tile))
                {
                    merge(Tile,adjacent.Tile);
                    return true;
                }

                break;
            }
            newscell = adjacent;
            adjacent = Grid.getadjacent(adjacent,direction); 
        }
        if (newscell!=null)
        {
            Tile.MoveTo(newscell);
            return true;
        }

        return false;
    }

    private bool canMerge(tile a,tile b )
    {
        return a.number == b.number&&!b.locked;
    }

    private void merge(tile a, tile b)
    {
        _tiles.Remove(a);
        a.merge(b.cell);
        int index = Mathf.Clamp(indexof(b.State) + 1, 0, tileStates.Length-1);
        int number = b.number * 2;
    }

    private int indexof(tilestats tilestate)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (tilestate==tileStates[i])
            {
                return i;
            }
        }

        return -1;
    }

    private IEnumerator WaitforChanges()
    {
        waiting = true;
        yield return new WaitForSeconds(0.1f);
        waiting = false;
        foreach (var tile in _tiles)
        {
            tile.locked = false;
            
        }
        if (_tiles.Count!=Grid.size)
        {
            createTile();
        }

        if (checkforGameOver())
        {
            GameManager.gameover();
        }
    }

    private bool checkforGameOver()
    {
        if (_tiles.Count!=Grid.size)
        {
            return false;
        }

        foreach (var tile in _tiles)
        {
            tilecell up = Grid.getadjacent(tile.cell, Vector2Int.up);
            tilecell down = Grid.getadjacent(tile.cell, Vector2Int.down);
            tilecell left = Grid.getadjacent(tile.cell, Vector2Int.left);
            tilecell right  = Grid.getadjacent(tile.cell, Vector2Int.right);

            if (up!=null&&canMerge(tile,up.Tile))
            {
                return false;
            }
            if (down!=null&&canMerge(tile,down.Tile))
            {
                return false;
            }
            if (left!=null&&canMerge(tile,left.Tile))
            {
                return false;
            }
            if (right!=null&&canMerge(tile,up.Tile))
            {
                return false;
            }
        }

        return true; 
    }
}
