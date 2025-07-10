using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    
    private CardSlotController selectingCard = null;

    public void Awake()
    {
        instance = this;
    }


    public void OnClickCard(object obj)
    {
        var cardObj = (CardSlotController)obj;
        if (!selectingCard) //No selectedCard
        {
            SelectCard(cardObj);
            selectingCard = cardObj;
            Debug.Log("Clicking " + cardObj.cardID);
            
        }
        else // Selected a card
        {
            if (CheckForSuccess(cardObj)) 
            {
                //TODO: scoreUp
                //TODO: Animate
                cardObj.PlayAnimationScore();
                selectingCard.PlayAnimationScore();
                //TODO: update data
                selectingCard = null;
                EventManager.Trigger(EventNames.OnScorePair, cardObj);
            }
            else
            {
               
                UnSelectCard(cardObj); 
                UnSelectCard(selectingCard);
                selectingCard = null;
                EventManager.Trigger(EventNames.OnFalsePair, cardObj);
            }
        }

    }

    public void SelectCard(CardSlotController cardObj)
    {
        cardObj.SetSelected(true);
    }

    public void UnSelectCard(CardSlotController cardObj)
    {
        cardObj.SetSelected(false);
    }

    public bool CheckForSuccess(CardSlotController cardObj)
    {
        return cardObj!=selectingCard && cardObj.cardID == selectingCard.cardID;
    }
    
}
