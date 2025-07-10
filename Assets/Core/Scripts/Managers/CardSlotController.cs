using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardSlotController : MonoBehaviour,IClickable
{
    public const int UninitalizeID = -1;
    public bool isSelected = false;
    [SerializeField] private TMPro.TMP_Text cardText;
    [SerializeField] private Animation anim;

    [SerializeField]public int cardID { get; private set; } = UninitalizeID;


    

    // Start is called before the first frame update
    

    public CardState GetCardState()
    {
        CardState cardState= new CardState();
        cardState.cardID = cardID;
        cardState.isSelected = isSelected;
        cardState.cardPos = new Vector2(this.transform.position.x,this.transform.position.z );
        return cardState;
    }

    /// <summary>
    /// Update the cardObj with cardInfo, card with UninitalizeID will be hidden;
    /// </summary>
    /// <param name="cardID"></param>
    public void Setup(int cardID,bool faceUp=false) {
        this.gameObject.SetActive(cardID != UninitalizeID);
        this.cardID = cardID;
        this.isSelected = faceUp;
        this.cardText.text = this.cardID.ToString();
        this.gameObject.name = "Card " + cardID;
        TurnCardInstantly(faceUp);
    }
    
    public void Setup(CardState cardInfo)
    {
        Setup(cardInfo.cardID, cardInfo.isSelected);
    }
    
    public void SetSelected(bool isSelected)
    {
        this.isSelected=isSelected;
        TurnCard(isSelected);
    }

    public void PlayAnimationScore()
    {
        this.isSelected = false;
        anim.Play("FlyAway");
    }


    public void Interact()
    {
        if (GameStateManager.instance.IsInGameplay)
        {
            EventManager.Trigger(EventNames.OnClickObject, (this));
        }
}

    public void TurnCard(bool toFacingUp)
    {
        if (toFacingUp)
        {
            anim[anim.clip.name].speed = 1;
            if(anim[anim.clip.name].normalizedTime!=0)anim[anim.clip.name].normalizedTime = 1- anim[anim.clip.name].normalizedTime;
            anim.Play();
        }
        else
        {
            anim[anim.clip.name].speed = -1;
            if(anim[anim.clip.name].normalizedTime==0) anim[anim.clip.name].normalizedTime =  1;;
            anim.Play();
        }


    }
    
    public void TurnCardInstantly(bool toFacingUp)
    {
        if(!toFacingUp)Debug.Log("Face down " +gameObject );
        this.gameObject.transform.eulerAngles = toFacingUp ? new Vector3(0, 0, 0) : new Vector3(180, 0, 0);
    } 
   [Serializable]
    public class CardState
    {
        public int cardID = UninitalizeID;
        public bool isSelected =false;
        public Vector2 cardPos = Vector2.zero;
    }

  
}


