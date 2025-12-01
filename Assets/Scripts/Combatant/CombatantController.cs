using UnityEngine;
using System.Collections.Generic;
using GameTools;
using GameEvents;




public class Deck
{
    private const int IN_HAND_MAX = 5;
    private Dictionary<string, CardSO> deckDictionary = new Dictionary<string, CardSO>();
    private List<string> cardIDs = new List<string>();
    private List<string> drawPile = new List<string>();
    private List<string> inHand = new List<string>();
    private List<string> discardPile = new List<string>();

    public List<string> CardIDs { get => cardIDs; }
    public List<string> DrawPile { get => drawPile; }
    public List<string> InHand { get => inHand; }
    public List<string> DiscardPile { get => discardPile; }
    public Deck(DeckDataSO deckData)
    {
        foreach (CardSO card in deckData.Cards)
        {
            string thisCardID = System.Guid.NewGuid().ToString();
            deckDictionary[thisCardID] = card;
            cardIDs.Add(thisCardID);
        }
        drawPile = new List<string>(cardIDs);
    }


    public StructCardData? GetCardDataByID(string cardID)
    {
        if (!cardIDs.Contains(cardID)) return null;
        return (deckDictionary[cardID].GetCardData());
    }
    public StructAttackCardData? GetAttackCardDataByID(string cardID)
    {
        if (!cardIDs.Contains(cardID)) return null;
        if (deckDictionary[cardID].GetCardData().cardType != EnumCardType.ATTACK) return null;
        return (deckDictionary[cardID] as AttackCard).GetAttackCardData();
    }
    public StructUtilityCardData? GetUtilityCardDataByID(string cardID)
    {
        if (!cardIDs.Contains(cardID)) return null;
        if (deckDictionary[cardID].GetCardData().cardType != EnumCardType.UTILITY) return null;
        return (deckDictionary[cardID] as UtilityCard).GetUtilityCardData();
    }
    public StructStatusEffectCardData? GetStatusEffectCardDataByID(string cardID)
    {
        if (!cardIDs.Contains(cardID)) return null;
        if (deckDictionary[cardID].GetCardData().cardType != EnumCardType.STATUS_EFFECT) return null;
        return (deckDictionary[cardID] as StatusEffectCard).GetStatusEffectCardData();
    }


    public void InitializeDeck()
    {
        drawPile = ShuffleDeck(cardIDs);
        inHand = new List<string>();
        discardPile = new List<string>();
    }
    private void RecycleDiscards()
    {
        drawPile = ShuffleDeck(discardPile);
        discardPile = new List<string>();
    }

    public void DrawUp()
    {
        int cardsNeeded = Deck.IN_HAND_MAX - inHand.Count;
        if (drawPile.Count < cardsNeeded)
        {
            RecycleDiscards();
        }
        for (int i = 0; i < cardsNeeded; i++)
        {
            inHand.Add(drawPile[0]);
            drawPile.RemoveAt(0);
        }

    }
    public List<string> ShuffleDeck(List<string> pile)
    {
        return RandomTools.ShuffleList(pile); // returns a copy of pile in randomized order
    }
}


public class CombatantController : MonoBehaviour
{
    [SerializeField] private CombatantSO combatantDataContainer;
    [SerializeField] private DeckDataSO deckDataContainer;
    [SerializeField] private SpriteRenderer focusIndicator;

    [Header("Events")]
    [SerializeField] private AttackCardDataEvent attackCardSelectedEvent;
    [SerializeField] private UtilityCardDataEvent utilityCardSelectedEvent;
    [SerializeField] private StatusEffectCardDataEvent statusEffectCardSelectedEvent;
    [SerializeField] private GenericCardDataEvent cardDiscardedEvent;

    private StructCombatantData myData;
    private Deck myDeck = null;
    private EnumFocus focusState = EnumFocus.UNFOCUSED;
    private string combatantID = string.Empty; // uniquely identifies this combatant during the present encounter

    public string CombatantID { get => combatantID; }
    public StructCombatantData CombatantData { get => myData; }
    public EnumFocus FocusState { get => focusState; }
    public Deck CombatantDeck { get => myDeck; }

    

    private void Awake()
    {
        combatantID = System.Guid.NewGuid().ToString(); 
        myData = combatantDataContainer.Data;
        myDeck = new Deck(deckDataContainer);
    }

    private void Start()
    {
        
    }

    public void DealHand()
    {
        Debug.Log($"Dealing {myData.combatantName}'s first hand...");
        myDeck.InitializeDeck();
        myDeck.DrawUp();
    }

    /*
    public List<CardSO> GetInHandCards()
    {
        //return
    }
    */

    private string DebugDeckState()
    {
        return $"Draw pile: {myDeck.DrawPile.Count}\nIn-hand: {myDeck.InHand.Count}\nDiscard pile: {myDeck.DiscardPile.Count}";
    }

    public void SetFocus(EnumFocus _focusState)
    {
        focusState = _focusState;
        focusIndicator.enabled = focusState == EnumFocus.FOCUSED;
    }

    private void OnValidate()
    {
        if (GetComponent<CombatantSprite>() == null) { Debug.LogError("CombatantSprite component missing"); return; }
        if (combatantDataContainer == null) { Debug.LogError("Add a combatant data SO"); return; }
        if (deckDataContainer == null) { Debug.LogError("Add a deck data SO"); return; }
        if (focusIndicator == null) { Debug.LogError("Add a focus indicator"); return; }

        if (combatantDataContainer.Data.control == EnumCombatantControl.CPU)
        {
            if (GetComponent<EncounterAI>() == null) { Debug.LogError("EncounterAI component missing"); return; }
        }
    }
}
