using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameStats defaultStats;
    public int scoreBase = 5;
    public double currentScore = 0;
    public int currentLife = 0;
    public int currentCombo = 0;
    public bool IsInGameplay {get; private set;}
    private GameStats loadedGameStats = null;
    private void Awake()
    {
        instance = this;
        EventManager.Subscribe(EventNames.OnScorePair,OnScoreAPair);
        EventManager.Subscribe(EventNames.OnFalsePair,OnFalseToPair);
        EventManager.Subscribe(EventNames.OnGameOver, OnGameOver);
        EventManager.Subscribe(EventNames.OnReplay, OnReplay);
        EventManager.Subscribe(EventNames.OnSaveAndQuit, OnSaveAndQuit);
        EventManager.Subscribe(EventNames.OnContinue, OnContinue);
        EventManager.Subscribe(EventNames.OnGameStateLoaded,OnGameStateLoaded);
    }

    private void OnGameStateLoaded(object obj)
    {
       var gamestate= obj as GameStats;
       if (gamestate != null)
       {
           scoreBase = gamestate.scoreBase;
           currentScore = gamestate.score;
           currentLife = gamestate.life;
           currentCombo = gamestate.combo;
       }
    }


    private void OnSaveAndQuit(object obj)
    {
        GameStats currentStats = new GameStats();
        currentStats.score = currentScore;
        currentStats.life = currentLife;
        currentStats.combo = currentCombo;
        currentStats.scoreBase = scoreBase;
        currentStats.cards = BoardManager.instance.GetCurrentCardsState();
        currentStats.boardSize = BoardManager.instance.boardSize;
        GameStatsManager.SaveCurrentStats(currentStats);
        EventManager.Trigger(EventNames.OnToTitle);
        IsInGameplay = false;
    }

    private void OnGameOver(object obj)
    {
        IsInGameplay = false;
    }

    private void OnReplay(object obj)
    { 
        InitGameState();
        IsInGameplay = true;
    }


    void OnContinue(object o)
    {
        loadedGameStats = GameStatsManager.LoadCurrentStats();
        EventManager.Trigger(EventNames.OnGameStateLoaded, loadedGameStats);
        IsInGameplay = true;
    }

    void InitGameState()
    {
        defaultStats = GameStatsManager.LoadDefaultStats();
        if (defaultStats != null)
        {
            scoreBase = defaultStats.scoreBase;
            currentScore = defaultStats.score;
            currentLife = defaultStats.life;
            currentCombo = defaultStats.combo;
        }

        //Load game stats and trigger update without React framework
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
            currentLife++;
            EventManager.Trigger(EventNames.OnLifeUpdate,currentLife);
            EventManager.Trigger(EventNames.OnScoreUpdate,currentScore);
            EventManager.Trigger(EventNames.OnComboUpdate,currentCombo);
            currentCombo++;
        }

        if (CheckForVictory())
        {
            EventManager.Trigger(EventNames.OnVictorious);
        }
    }

   

    private void OnFalseToPair(object obj)
    {
        currentCombo = 0;
        currentLife--;
        EventManager.Trigger(EventNames.OnLifeUpdate,currentLife);
        EventManager.Trigger(EventNames.OnComboUpdate,currentCombo);
        if (currentLife <= 0)
        {
            EventManager.Trigger(EventNames.OnGameOver,currentScore);
        }
    }
    
    bool CheckForVictory()
    {
        var cardsState = BoardManager.instance.GetCurrentCardsState();
        bool notAllPaired = cardsState.Any(card => !card.paired && card.cardID!= -1);
        return !notAllPaired;
    }
}

[System.Serializable]
public class GameStats
{
    public int scoreBase = 5;
    public double score = 0;
    public int life = 10;
    public int combo = 0;
    public Vector2Int boardSize= new Vector2Int(5,5);
    public List<CardSlotController.CardState> cards=null;
}



public static class GameStatsManager
{
    private static GameStats LoadedStats ;

    public static GameStats LoadDefaultStats()
    {
        string path = Application.streamingAssetsPath + "/game_stats.json";
        LoadedStats = new GameStats();
        if (!File.Exists(path)) return LoadedStats;
#if UNITY_EDITOR || UNITY_STANDALONE
       
        string json = File.ReadAllText(path);
#elif UNITY_ANDROID
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        string json = reader.text;
#endif

        LoadedStats = JsonUtility.FromJson<GameStats>(json);
        return LoadedStats;
    }
    
    public static void SaveCurrentStats(GameStats currentStats)
    {
        string json = JsonUtility.ToJson(currentStats, true);
        string filePath = Application.persistentDataPath + "/saved_stats.json";

        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("✅ Stats saved to: " + filePath);

    }
    
    public static GameStats LoadCurrentStats()
    {
        string path = Application.persistentDataPath + "/saved_stats.json";
        if (!File.Exists(path)) return null;
#if UNITY_EDITOR || UNITY_STANDALONE
        string json = File.ReadAllText(path);
#elif UNITY_ANDROID
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        string json = reader.text;
#endif

        LoadedStats = JsonUtility.FromJson<GameStats>(json);
        return LoadedStats;
    }

    
   
}
