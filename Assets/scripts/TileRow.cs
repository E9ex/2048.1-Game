using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRow : MonoBehaviour
{
  public tilecell[] cell { get; private set; }

  private void Awake()
  {
    cell = GetComponentsInChildren<tilecell>();
  }
}
