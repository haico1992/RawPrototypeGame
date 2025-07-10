using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] HudController hudController;
    private void Awake()
    {
        EventManager.Subscribe(EventNames.OnScoreUpdate,OnScoreUpdate);
        EventManager.Subscribe(EventNames.OnLifeUpdate,OnLifeUpdate);
        EventManager.Subscribe(EventNames.OnComboUpdate,OnComboUpdate);
    }

    private void OnComboUpdate(object obj)
    {
        hudController.SetCombo((int)obj);
    }

    private void OnLifeUpdate(object obj)
    {
        hudController.SetLife((int)obj);
    }

    void OnScoreUpdate(object obj)
    {
        hudController.SetScore((double)obj);
    }
}
