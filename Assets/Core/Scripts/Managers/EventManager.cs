using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<string, Action<object>> eventTable = new();

    public static void Subscribe(string eventName, Action<object> listener)
    {
        if (eventTable.ContainsKey(eventName))
            eventTable[eventName] += listener;
        else
            eventTable[eventName] = listener;
    }

    public static void Unsubscribe(string eventName, Action<object> listener)
    {
        if (eventTable.ContainsKey(eventName))
            eventTable[eventName] -= listener;
    }

    public static void Trigger(string eventName, object parameter = null)
    {
        if (eventTable.ContainsKey(eventName))
            eventTable[eventName]?.Invoke(parameter);
    }
}

public static class EventNames
{
    public const string OnClickObject = "OnClickObject";
    public const string OnSetupBoard = "OnSetupBoard";
    public const string OnScorePair = "OnScorePair";
    public const string OnFalsePair = "OnFalsePair";
    public const string OnScoreUpdate = "OnScoreUpdate";
    public const string OnLifeUpdate = "OnLifeUpdate";
    public const string OnComboUpdate = "OnComboUpdate";
}