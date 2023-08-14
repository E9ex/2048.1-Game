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
   public bool locked { get;  set; }
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

   public void MoveTo(tilecell tcell)
   {
      if (this.cell!=null)
      {
         this.cell.Tile = null;
      }
      this.cell = cell;
      this.cell.Tile = this;
      transform.position = cell.transform.position;
      StartCoroutine(animate(cell.transform.position,false)); 
   }

   public void merge(tilecell tilecell)
   {
      if (this.cell!=null)
      {
         this.cell.Tile = null;
      }

      this.cell = null;
      cell.Tile.locked = true;
      StartCoroutine(animate(cell.transform.position,true));
   }

   private IEnumerator animate(Vector3 to ,bool merging)
   {
      float elapsed = 0f;
      float duration = 0.1f;
      Vector3 from = transform.position;
      while (elapsed<duration)
      {
         transform.position = Vector3.Lerp(from, to, elapsed / duration);
         elapsed += Time.deltaTime;
         yield return null;
      }
      transform.position = to;
      if (merging )
      {
         Destroy(gameObject);
      }
   }
}
