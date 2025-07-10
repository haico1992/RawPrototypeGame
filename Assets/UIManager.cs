using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
   
    [SerializeField] HudController hudController;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameTitlePanel;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] Button replayButton;
    [SerializeField] Button saveAndQuitButton;
    [SerializeField] Button gameStartButton;
    [SerializeField] Button continueButton;
    [SerializeField] Button victoryButton;
    [SerializeField] TMPro.TMP_Dropdown boardSizeSelection;
    private void Awake()
    {
        instance = this;
        EventManager.Subscribe(EventNames.OnScoreUpdate,OnScoreUpdate);
        EventManager.Subscribe(EventNames.OnLifeUpdate,OnLifeUpdate);
        EventManager.Subscribe(EventNames.OnComboUpdate,OnComboUpdate);
        EventManager.Subscribe(EventNames.OnGameOver,OnGameOver);
        EventManager.Subscribe(EventNames.OnReplay,OnReplay);
        EventManager.Subscribe(EventNames.OnContinue,OnReplay);
        EventManager.Subscribe(EventNames.OnSaveAndQuit,OnSaveAndQuit);
        EventManager.Subscribe(EventNames.OnGameStateLoaded,OnGameStateLoaded);
        EventManager.Subscribe(EventNames.OnVictorious,OnVictorious);
        
        replayButton?.onClick.AddListener(()=>EventManager.Trigger(EventNames.OnReplay));
        victoryButton?.onClick.AddListener(()=>EventManager.Trigger(EventNames.OnReplay));
        saveAndQuitButton?.onClick.AddListener(()=>EventManager.Trigger(EventNames.OnSaveAndQuit));
        gameStartButton?.onClick.AddListener(()=>EventManager.Trigger(EventNames.OnReplay));
        continueButton?.onClick.AddListener(()=>EventManager.Trigger(EventNames.OnContinue));
        boardSizeSelection?.onValueChanged.AddListener(OnSelectBoardSize);
    }

    private void OnVictorious(object obj)
    {
       victoryPanel.gameObject.SetActive(true);
    }

    private void OnContinue(object obj)
    {
        gameTitlePanel.gameObject.SetActive(false);
    }

    private void OnSaveAndQuit(object obj)
    {
        continueButton.gameObject.SetActive(true);
        gameTitlePanel.SetActive(true);
        
    }

    private void OnSelectBoardSize(int index)
    {
        BoardManager.instance.boardSize = GetBoardSizeFromUI();
    }

    public Vector2Int GetBoardSizeFromUI()
    {
        int size = 5-boardSizeSelection.value;
        return  new Vector2Int(size, size);
    }

    private void OnGameStateLoaded(object obj)
    {
        GameStats gameStats = obj as  GameStats;
        continueButton.gameObject.SetActive(obj != null);
        hudController.SetCombo(gameStats.combo);
        hudController.SetLife(gameStats.life);
        hudController.SetScore(gameStats.score);
    }

    private void OnReplay(object obj)
    {
        gameOverPanel.SetActive(false);
        gameTitlePanel.SetActive(false);
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
