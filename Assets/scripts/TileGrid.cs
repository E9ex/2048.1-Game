using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGrid : MonoBehaviour
{
   public TileRow[] Rows { get; private set; }
   public tilecell[] Tilecells { get; private set; }
   public int size => Tilecells.Length;
   public int height => Rows.Length;
   public int width => size / height;

   private void Awake()
   {
      Rows = GetComponentsInChildren<TileRow>();
      Tilecells = GetComponentsInChildren<tilecell>(); 
   }

   private void Start()
   {
      for (int y = 0; y < Rows.Length; y++)
      {
         for (int x = 0; x < Rows[y].cell.Length; x++)
         {
            Rows[y].cell[x].coordinates = new Vector2Int(x,y);
         }
      }
   }

   public tilecell getrandomemptycell()
   {
      int index = Random.Range(0,Tilecells.Length);
      int startingIndex = index;
      while (Tilecells[index].occupied)
      {
         index++;
         if (index >= Tilecells.Length)
         {
            index = 0;
         }
         if (index==startingIndex)
         {
            return null;
         }
      }

      return Tilecells[index];
   }
}
