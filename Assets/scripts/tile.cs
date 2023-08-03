using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tile : MonoBehaviour
{
   public tilestats State { get; private set; }
   public tilecell cell { get; private set; }
   public int number { get; private set; }
   public Image background;
   public TextMeshProUGUI text;


   private void Awake()
   {
      background = GetComponent<Image>();
      text = GetComponentInChildren<TextMeshProUGUI>();
   }

   public void setstate(tilestats state,int Number)
   {
      this.State = state;
      this.number = Number;
      background.color = state.backgroundColor;
      text.color = state.textColor;
      text.text = number.ToString();
   }

   public void Spawn(tilecell cell)
   {
      if (this.cell!=null)
      {
         this.cell.Tile = null;
      }
      this.cell = cell;
      this.cell.Tile = this;
      transform.position = cell.transform.position;
   }
}
