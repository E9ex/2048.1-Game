using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Grid
{
    public Vector2 Coord;
    public Transform Transform;
    public tile Tile;
}

public class M_Tile : MonoBehaviour
{
    [SerializeField] private List<Grid> Grids = new List<Grid>();

}
