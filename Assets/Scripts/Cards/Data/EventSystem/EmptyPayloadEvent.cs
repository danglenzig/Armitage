using UnityEngine;
namespace GameEvents
{
    [CreateAssetMenu(fileName = "EmptyPayloadEvent", menuName = "Events/EmptyPayloadEvent")]
    public class EmptyPayloadEvent : ScriptableObject
    {
        public event System.Action OnEventTriggered;
        public void TriggerEvent()
        {
            OnEventTriggered?.Invoke();
        }
    }
}


