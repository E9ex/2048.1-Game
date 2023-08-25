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
  // public int Number { get; private set; }
   public bool locked { get;   set; }
   public Image background;
   public TextMeshProUGUI text;


   private void Awake()
   {
      background = GetComponent<Image>();
      text = GetComponentInChildren<TextMeshProUGUI>();
   }

   public void setstate(tilestats state)
   {
      State = state;
      
      background.color = state.backgroundColor;
      text.color = State.textColor;
      text.text = State.number.ToString();
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
      StartCoroutine(Animate(cell.transform.position,false)); 
   }

   public void merge(tilecell tilecell)
   {
      if (this.cell != null) {
         this.cell.Tile = null;
      }

      this.cell = null;
      tilecell.Tile.locked = true;
      StartCoroutine(Animate(tilecell.transform.position,true));
   }

   private IEnumerator Animate(Vector3 to, bool merging)
   {
      float elapsed = 0f;
      float duration = 0.1f;

      Vector3 from = transform.position;

      while (elapsed < duration)
      {
         transform.position = Vector3.Lerp(from, to, elapsed / duration);
         elapsed += Time.deltaTime;
         yield return null;
      }

      transform.position = to;

      if (merging) {
         Destroy(gameObject);
      }
   }
}
