using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] HudController hudController;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button replayButton;
    [SerializeField] Button saveAndQuitButton;
    private void Awake()
    {
        EventManager.Subscribe(EventNames.OnScoreUpdate,OnScoreUpdate);
        EventManager.Subscribe(EventNames.OnLifeUpdate,OnLifeUpdate);
        EventManager.Subscribe(EventNames.OnComboUpdate,OnComboUpdate);
        EventManager.Subscribe(EventNames.OnGameOver,OnGameOver);
        EventManager.Subscribe(EventNames.OnReplay,OnReplay);
        replayButton.onClick.AddListener(()=>EventManager.Trigger(EventNames.OnReplay));
        saveAndQuitButton.onClick.AddListener(()=>EventManager.Trigger(EventNames.OnSaveAndQuit));
    }

    private void OnReplay(object obj)
    {
        gameOverPanel.SetActive(false);
    }

    private void OnGameOver(object obj)
    {
        gameOverPanel.SetActive(true);
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
