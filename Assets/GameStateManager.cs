using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public int scoreBase = 5;
    public double currentScore = 0;
    public int currentLife = 0;
    public int currentCombo = 0;

    private void Awake()
    {
        EventManager.Subscribe(EventNames.OnScorePair,OnScoreAPair);
        EventManager.Subscribe(EventNames.OnFalsePair,OnFalseToPair);
    }

 

    void Start()
    {
        EventManager.Trigger(EventNames.OnScoreUpdate, currentScore);
        EventManager.Trigger(EventNames.OnLifeUpdate, currentLife);
        EventManager.Trigger(EventNames.OnComboUpdate, currentCombo);
        
    }

    void OnScoreAPair(object score)
    {
        CardSlotController cardSlotController = score as CardSlotController;
        if (cardSlotController)
        {
            
            currentScore += scoreBase * Math.Pow(2, currentCombo);
            EventManager.Trigger(EventNames.OnScoreUpdate,currentScore);
            EventManager.Trigger(EventNames.OnComboUpdate,currentCombo);
            currentCombo++;
            
        }
    }
    
    private void OnFalseToPair(object obj)
    {
        currentCombo = 0;
        currentLife -= 1;
        EventManager.Trigger(EventNames.OnLifeUpdate,currentLife);
        EventManager.Trigger(EventNames.OnComboUpdate,currentCombo);
        if (currentLife <= 0)
        {
            EventManager.Trigger(EventNames.OnGameOver,currentScore);
        }
    }
}
