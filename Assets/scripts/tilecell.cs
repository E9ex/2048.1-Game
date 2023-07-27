using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilecell : MonoBehaviour
{
   public Vector2Int coordinates { get; set; }
   public tile Tile { get; set; }
   public bool empty => Tile == null;
   public bool occupied => Tile != null;
}
