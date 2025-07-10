using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI scoreText;
   [SerializeField] TextMeshProUGUI comboText;
   [SerializeField] TextMeshProUGUI lifeText;
   [SerializeField] Animation comboPanel;

   public void SetScore(double score)
   {
      scoreText.text = score.ToString();
   }

   public void SetCombo(int combo)
   {
      comboText.text = "x" + Math.Pow(2, combo);
      if (combo > 0)
      {
         comboPanel.gameObject.SetActive(true);
         comboPanel.Play();
      }
      else comboPanel.gameObject.SetActive(false);
   }

   public void SetLife(int life)
   {
      lifeText.text = life.ToString();
   }
} 
