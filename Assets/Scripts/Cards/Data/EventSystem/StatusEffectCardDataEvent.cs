using UnityEngine;
namespace GameEvents
{
    [CreateAssetMenu(fileName = "StatusEffectCardDataEvent", menuName = "Events/StatusEffectCardDataEvent")]
    public class StatusEffectCardDataEvent : ScriptableObject
    {
        public event System.Action<StructCardData, StructStatusEffectCardData, string> OnEventTriggered;
        public void TriggerEvent(StructCardData cardData, StructStatusEffectCardData statusEffectCardData, string combatantID)
        {
            OnEventTriggered?.Invoke(cardData, statusEffectCardData, combatantID);
        }
    }
}


