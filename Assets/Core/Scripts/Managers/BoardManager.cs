using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    [SerializeField] public GameObject CardPrefab;
    [SerializeField] public float cardOffsetX = 1.33f;
    [SerializeField] public float cardOffsetY = 1.33f;
    [SerializeField] public Vector2Int boardSize = new Vector2Int(1, 1);

  
    private List<CardSlotController> listCardObjects = new List<CardSlotController>(); 
    private Dictionary<int, int> cardIndexWithCount = new Dictionary<int, int>();

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupBoard(this.boardSize);
    }

    private void OnEnable()
    {
        EventManager.Subscribe(EventNames.OnClickObject,GameplayManager.instance.OnClickCard);

    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EventNames.OnClickObject,GameplayManager.instance.OnClickCard);

    }


    /// <summary>
    /// Generate card objects for a new game
    /// </summary>
    /// <param name="boardSize"></param>
    public void SetupBoard(Vector2Int boardSize)
    {
        SetupBoard(boardSize.x, boardSize.y);
        EventManager.Trigger(EventNames.OnSetupBoard,boardSize);
    }

    /// <summary>
    /// Generate board of card based on boardSize, from index 0
    /// If the number of card is odd, do not generate the last card
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    public void SetupBoard(int column, int row)
    {
        cardIndexWithCount = GenerateCardIndex(column * row / 2);
        var cardPositions = GenerateCardPositions(column, row, this.cardOffsetX,this.cardOffsetY);
        foreach (var position in cardPositions)
        {
            var cardObj= Instantiate(CardPrefab, new Vector3(position.x,0,position.y), Quaternion.identity);

            if (cardObj.TryGetComponent(out CardSlotController slot))
            {
                listCardObjects.Add(slot);
                var cardInfo= PullRandomIndexFromCardList(cardIndexWithCount);
                slot.Setup(cardInfo);
              
               
            }

           
        }

    }
    
/// <summary>
/// Dictionary<cardInfo, cardCount>
/// </summary>
/// <param name="list"></param>
/// <returns></returns>
    public int PullRandomIndexFromCardList(Dictionary<int, int> list)
    {
        foreach (var index in list.Keys)
        {
            if (list[index] > 0)
            {
                list[index]--;
                return index;
            }
        }
        return CardSlotController.UninitalizeID;
    }

    private static List<Vector2> GenerateCardPositions(int cols, int rows, float spacingX, float spacingY)
    {
        var positions = new List<Vector2>();

        float offsetX = (cols - 1) * spacingX / 2f;
        float offsetY = (rows - 1) * spacingY / 2f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                float x = col * spacingX - offsetX;
                float y = offsetY - row * spacingY;
                positions.Add(new Vector2(x, y));
            }
        }
        Shuffle(positions);
        return positions;
    }

    private static Dictionary<int, int> GenerateCardIndex(int indexCount)
    {
        Dictionary<int, int> indexes = new Dictionary<int, int>();
        int indexTotal = 0;
        while (indexTotal < indexCount)
        {
            if (indexes.TryAdd(Random.Range(0, indexCount*3), 2))
            {
                indexTotal++;
            }
        }
        return indexes;
    }
    
    public static void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1); 
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

}
