using UnityEngine;
using GameTools;


public struct StructCardData
{
    public EnumCardType cardType;
    public string cardName;
    public string faceTexturePath;
    public string backTexturePath;
    public int actionCost;

    public StructCardData
        (
            EnumCardType _cardType,
            string _cardName,
            string _faceTexturePath,
            string _backTexturePath,
            int _actionCost
        )
    {
        cardType = _cardType;
        cardName = _cardName;
        faceTexturePath = _faceTexturePath;
        backTexturePath = _backTexturePath;
        actionCost = _actionCost;
    }

}

[CreateAssetMenu(fileName = "CardSO", menuName = "Cards/Generic Card")]
public class CardSO : ScriptableObject
{
    [SerializeField] private string cardName = "";
    [SerializeField] private string faceTexturePath = "Resources/...";
    [SerializeField] private string backTexturePath = "Resources/...";
    [Range(1,3)][SerializeField] private int actionCost = 1;

    private EnumCardType cardType;
    [HideInInspector] public EnumCardType CardType { get => cardType; set { cardType = value; } }


    public StructCardData GetCardData()
    {
        StructCardData data = new StructCardData
            (
                cardType,
                cardName,
                faceTexturePath,
                backTexturePath,actionCost
            );
        return data;
    }
}
