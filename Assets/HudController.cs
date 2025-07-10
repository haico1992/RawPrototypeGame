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

   public void SetScore(double score)
   {
      scoreText.text = score.ToString();
   }

   public void SetCombo(int combo)
   {
      comboText.text = combo.ToString();
   }

   public void SetLife(int life)
   {
      lifeText.text = life.ToString();
   }
} 
