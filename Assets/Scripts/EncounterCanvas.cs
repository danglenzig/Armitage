using System.Collections.Generic;
using UnityEngine;

public class EncounterCanvas : MonoBehaviour
{

    [SerializeField] private List<CardUI> cardUIs;

    public void DisplayInHand(Deck deck)
    {
        if (deck.InHand.Count != 5) { Debug.LogError("Five cards expected in hand"); return; }
        for (int i = 0; i < 5; i++)
        {
            string thisCardID = deck.InHand[i];
            CardUI thisCardUI = cardUIs[i];
            StructCardData thisCardData = deck.GetCardDataByID(thisCardID).Value;

            string cardTypeString = thisCardData.cardType.ToString();
            string cardNameString = thisCardData.cardName;
            string cardFaceTexturePath = thisCardData.faceTexturePath;
            string cardActionCostString = thisCardData.actionCost.ToString();
        }
    }
}
