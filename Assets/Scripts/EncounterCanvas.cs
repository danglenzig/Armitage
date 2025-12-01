using System.Collections.Generic;
using UnityEngine;
using GameTools;

public class EncounterCanvas : MonoBehaviour
{

    [SerializeField] private List<CardUI> cardUIs;
    [SerializeField] private GameObject inHandCardsHolder;

    private void Awake()
    {
        inHandCardsHolder.SetActive(false);
    }

    public void FixOnSelectActionInHand(string selectedCardID)
    {
        foreach (CardUI card in cardUIs)
        {
            card.Selectable = false;
            if (card.CurrentCardID == selectedCardID)
            {
                card.SetIsSubdued(false);
            }
            else
            {
                card.SetIsSubdued(true);
            }
        }
        DisplaySelectedCardActionChoices(selectedCardID);
    }

    private void DisplaySelectedCardActionChoices(string selectedCardID)
    {
        
    }

    public void DisplayInHand(Deck deck)
    {
        if (deck.InHand.Count != 5) { Debug.LogError("Five cards expected in hand"); return; }
        for (int i = 0; i < deck.InHand.Count; i++)
        {
            string thisCardID = deck.InHand[i];
            CardUI thisCardUI = cardUIs[i];
            StructCardData thisCardData = deck.GetCardDataByID(thisCardID).Value;
            thisCardUI.FixCardUI(thisCardData, thisCardID);
            cardUIs[i].Selectable = true;
            cardUIs[i].SetIsSubdued(false);

            switch (thisCardData.cardType)
            {
                case EnumCardType.ATTACK:
                    thisCardUI.AttackCardUI.FixAttackCardUI(deck.GetAttackCardDataByID(thisCardID).Value);
                    break;
                case EnumCardType.STATUS_EFFECT:
                    thisCardUI.StatusEffectUI.FixStatusEffectUI(deck.GetStatusEffectCardDataByID(thisCardID).Value);
                    break;
                case EnumCardType.UTILITY:
                    thisCardUI.UtilityCardUI.FixUtilityCardUI(deck.GetUtilityCardDataByID(thisCardID).Value);
                    break;
            }
        }
        inHandCardsHolder.SetActive(true);
    }
}
