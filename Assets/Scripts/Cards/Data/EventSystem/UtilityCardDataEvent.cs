using UnityEngine;
namespace GameEvents
{
    [CreateAssetMenu(fileName = "UtilityCardDataEvent", menuName = "Events/UtilityCardDataEvent")]
    public class UtilityCardDataEvent : ScriptableObject
    {
        public event System.Action<StructCardData, StructUtilityCardData, string> OnEventTriggered;
        public void TriggerEvent(StructCardData cardData, StructUtilityCardData utilityCardData, string combatantID)
        {
            OnEventTriggered?.Invoke(cardData, utilityCardData, combatantID);
        }
    }
}