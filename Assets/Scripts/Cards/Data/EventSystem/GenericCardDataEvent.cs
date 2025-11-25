using UnityEngine;
namespace GameEvents
{
    [CreateAssetMenu(fileName = "GenericCardDataEvent", menuName = "Events/GenericCardDataEvent")]
    public class GenericCardDataEvent : ScriptableObject
    {
        public event System.Action<StructCardData, string> OnEventTriggered;
        public void TriggerEvent(StructCardData cardData, string combatantID)
        {
            OnEventTriggered?.Invoke(cardData, combatantID);
        }
    }
}


