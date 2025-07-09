using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardSlotController : MonoBehaviour,IClickable
{
    public const int UninitalizeID = -1;
    public bool isSelected = false;
    public UnityAction<CardSlotController> OnClickByPlayer;
    [SerializeField] private TMPro.TMP_Text cardText;
    [SerializeField] private Animation animation;

    public int cardID { get; private set; } = UninitalizeID;


    

    // Start is called before the first frame update
    void Start()
    {
        Setup(cardID);
    }

    /// <summary>
    /// Update the cardObj with cardInfo, card with UninitalizeID will be hidden;
    /// </summary>
    /// <param name="cardID"></param>
    public void Setup(int cardID,bool faceUp=false) {
        this.cardID = cardID;
        cardText.text = this.cardID.ToString();
        TurnCardInstantly(faceUp);
        this.gameObject.SetActive(cardID != UninitalizeID);
    }
    
    public void SetSelected(bool isSelected)
    {
        if (isSelected == this.isSelected)
        {
            Debug.LogWarning("Trying to set the card into the same state");
        }
        this.isSelected = isSelected;
        TurnCard(isSelected);
       
    }

    public void PlayAnimationScore()
    {
        animation.Play("FlyAway");
    }


    public void Interact()
    {
        if (OnClickByPlayer!=null)
        {
            OnClickByPlayer.Invoke(this);
        }
    }

    public void TurnCard(bool toFacingUp)
    {
        if (toFacingUp)
        {
            animation[animation.clip.name].speed = 1;
            if(animation[animation.clip.name].normalizedTime!=0)animation[animation.clip.name].normalizedTime = 1- animation[animation.clip.name].normalizedTime;
            animation.Play();
        }
        else
        {
            animation[animation.clip.name].speed = -1;
            if(animation[animation.clip.name].normalizedTime==0) animation[animation.clip.name].normalizedTime =  1;;
            animation.Play();
        }


    }
    
    public void TurnCardInstantly(bool toFacingUp)
    {
        if (toFacingUp)
        {
            animation[animation.clip.name].speed = 1;
            animation[animation.clip.name].normalizedTime = 1;
            animation.Play();
        }
        else
        {
            animation[animation.clip.name].speed = -1;
            animation[animation.clip.name].normalizedTime =  0;;
            animation.Play();
        }


    }
}

