using UnityEngine;
using System.Collections.Generic;
using GameTools;
using GameEvents;
using StateMachine;

public class EncounterController : MonoBehaviour
{
    [SerializeField] private StateMachineSO encounterStateMachine;


    [Header("Events")]
    [SerializeField] private StringPayloadEvent cardSelectedEvent;


    private List<CombatantController> combatants = new List<CombatantController>();
    private CombatantController focusedCombatant = null;
    private List<string> playerPartyIDs = new List<string>();
    private List<string> opponentPartyIDs = new List<string>();
    private int turnIdx = -1;

    private string selectedCardID = string.Empty;

    

    

    private void Awake()
    {
        encounterStateMachine.Initialize();
    }
    private void Start()
    {
        RegisterCombatants();
        encounterStateMachine.TriggerTransition(Constants.TO_DEAL);
    }

    private void OnEnable()
    {
        foreach(StateSO state in encounterStateMachine.GetStates())
        {
            state.OnStateEntered += OnStateEntered;
        }
        cardSelectedEvent.OnEventTriggered += HandleOnCardSelected;
    }
    private void OnDisable()
    {
        foreach (StateSO state in encounterStateMachine.GetStates())
        {
            state.OnStateEntered -= OnStateEntered;
        }
        cardSelectedEvent.OnEventTriggered -= HandleOnCardSelected;
    }

    private void OnDealStateEntered()
    {
        foreach (CombatantController comb in combatants)
        {
            comb.DealHand();
        }
        encounterStateMachine.TriggerTransition(Constants.TO_DECIDE_TURN);
    }

    private void OnDecideTurnStateEntered()
    {
        turnIdx = (turnIdx + 1) % combatants.Count;
        for (int i = 0; i < combatants.Count; i++)
        {
            if (i == turnIdx) { combatants[i].SetFocus(EnumFocus.FOCUSED); focusedCombatant = combatants[i]; }
            else { combatants[i].SetFocus(EnumFocus.UNFOCUSED); }
        }
        Debug.Log($"It is {focusedCombatant.CombatantData.combatantName}'s turn.");
        encounterStateMachine.TriggerTransition(Constants.TO_SELECT_CARD);
        focusedCombatant.AvailableAP = focusedCombatant.CombatantData.perTurnAP;
    }

    private void OnSelectCardStateEntered()
    {
        if (focusedCombatant.CombatantData.control == EnumCombatantControl.PLAYER)
        {
            if (focusedCombatant.GetComponent<EncounterAI>() != null)
            {
                Debug.LogError("Player character should not have an AI brain");
                return;
            }
            HandlePlayerSelectCard(focusedCombatant);
        }
        else
        {
            if (focusedCombatant.GetComponent<EncounterAI>() == null)
            {
                Debug.LogError("CPU character has no brain");
                return;
            }
        }
    }
    private void OnSelectActionStateEntered()
    {
        if (focusedCombatant.CombatantDeck.GetCardDataByID(selectedCardID) == null) { Debug.LogError("DONT HAVE THAT CARD"); return; }
        StructCardData cardData = focusedCombatant.CombatantDeck.GetCardDataByID(selectedCardID).Value;

        EncounterCanvas encounterUI = GameObject.FindGameObjectWithTag(Constants.ENCOUNTER_CANVAS_TAG).GetComponent<EncounterCanvas>();

        if (focusedCombatant.CombatantData.control == EnumCombatantControl.PLAYER)
        {
            encounterUI.FixOnSelectActionInHand(selectedCardID);

            int availableAP = focusedCombatant.AvailableAP;



            //int availableAP = focusedCombatant.CombatantData.

            // Play --> to SELECT_TARGET, 
            // Discard --> to DRAW_UP
            // Back --> to SELECT_CARD
            // End Turn --> to DECIDE_TURN

        }
        else
        {
            // handle AI action select behavior
        }


    }

    private void HandlePlayerSelectCard(CombatantController playerCombatant)
    {
        EncounterCanvas encounterUI = GameObject.FindGameObjectWithTag(Constants.ENCOUNTER_CANVAS_TAG).GetComponent<EncounterCanvas>();
        encounterUI.DisplayInHand(playerCombatant.CombatantDeck);
    }
    private void HandleCPUSelectCard(CombatantController cpuCombatant)
    {
        EncounterAI cpuIO = cpuCombatant.GetComponent<EncounterAI>();
        //TODO:  class EncounterAI is just a blank monobehavior script right now...
    }
    private void HandleOnCardSelected(string cardID)
    {
        if (focusedCombatant.CombatantDeck.GetCardDataByID(cardID) == null) { Debug.LogError("DONT HAVE THAT CARD"); return; }
        StructCardData cardData = focusedCombatant.CombatantDeck.GetCardDataByID(cardID).Value;
        Debug.Log($"{focusedCombatant.CombatantData.combatantName} played {cardData.cardName}");
        selectedCardID = cardID;
        encounterStateMachine.TriggerTransition(Constants.TO_SELECT_ACTION);
    }


    private void OnStateEntered(StateData stateData)
    {
        Debug.Log($"Encounter state: {stateData.StateName}");
        switch (stateData.StateName)
        {
            case Constants.BEGIN_STATE:
                return;
            case Constants.DEAL_STATE:
                OnDealStateEntered();
                return;
            case Constants.DECIDE_TURN_STATE:
                OnDecideTurnStateEntered();
                return;
            case Constants.SELECT_CARD_STATE:
                OnSelectCardStateEntered();
                return;
            case Constants.SELECT_ACTION_STATE:
                OnSelectActionStateEntered();
                return;
            case Constants.SELECT_TARGET_STATE:
                return;
            case Constants.RESOLVE_EFFECT_STATE:
                return;
            case Constants.DRAW_UP_STATE:
                return;
            case Constants.CHECK_WIN_STATE:
                return;
            case Constants.END_STATE:
                return;

            default: Debug.LogError($"Invalid state {stateData.StateName}"); return;

        }
    }

    

    private void OnStateExited(StateData stateData)
    {

    }
    
    private void RegisterCombatants()
    {
        GameObject[] combatantObjs = GameObject.FindGameObjectsWithTag(Constants.COMBATANT_TAG);
        foreach (GameObject obj in combatantObjs)
        {
            CombatantController comb = obj.GetComponent<CombatantController>();
            combatants.Add(comb);
            if (comb.CombatantData.control == EnumCombatantControl.PLAYER) { playerPartyIDs.Add(comb.CombatantID); }
            else { opponentPartyIDs.Add(comb.CombatantID); }
        }

        // Re-order the combatants list by initiative:
        // -- higher values come first
        // -- if tie, then player-controlled combatant first
        // -- if still tie, then any order is fine
        // Get initiative int with "comb.CombatantData.initiative"
        // Get control Enum with "comb.CombatantData.control"
        // -- comb.CombatantData.control == EnumCombatantControl.PLAYER means player
        // -- comb.CombatantData.control == EnumCombatantControl.CPU means not player
        combatants.Sort
            (
                (a,b) =>
                {
                    // Rule 1: higher initiative first
                    int initCompare = b.CombatantData.initiative.CompareTo(a.CombatantData.initiative);
                    if (initCompare != 0) return initCompare;

                    // Rule 2: player before CPU
                    // Convert control to an int rank: PLAYER = 1, CPU = 0
                    int aRank = (a.CombatantData.control == EnumCombatantControl.PLAYER) ? 1 : 0;
                    int bRank = (b.CombatantData.control == EnumCombatantControl.PLAYER) ? 1 : 0;
                    int controlCompare = bRank.CompareTo(aRank);
                    if (controlCompare != 0) return controlCompare;

                    // 3. Tie-breaker: unique ID (consistent ordering)
                    return string.Compare(a.CombatantID, b.CombatantID, System.StringComparison.Ordinal);
                }
            );

    }

    private StructCombatantData? GetCombatantDataByID(string iD)
    {
        foreach(CombatantController comb in combatants)
        {
            if (comb.CombatantID == iD) { return comb.CombatantData; }
        }
        return null;
    }
}
