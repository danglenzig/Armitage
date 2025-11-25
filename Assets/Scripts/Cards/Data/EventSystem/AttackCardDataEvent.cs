using UnityEngine;
namespace GameEvents
{
    [CreateAssetMenu(fileName = "AttackCardDataEvent", menuName = "Events/AttackCardDataEvent")]
    public class AttackCardDataEvent : ScriptableObject
    {
        public event System.Action<StructCardData, StructAttackCardData, string> OnEventTriggered;
        public void TriggerEvent(StructCardData cardData, StructAttackCardData attackCardData, string combatantID)
        {
            OnEventTriggered?.Invoke(cardData, attackCardData, combatantID);
        }
    }
}


