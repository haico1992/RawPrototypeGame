using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlotController : MonoBehaviour
{
    public const int UninitalizeID = -1;
    [SerializeField] private TMPro.TMP_Text cardText;
    private int cardID = UninitalizeID;

    // Start is called before the first frame update
    void Start()
    {
        Setup(cardID);
    }

    public void Setup(int cardID) {
        this.cardID = cardID;
        cardText.text = this.cardID.ToString();
    }
}
