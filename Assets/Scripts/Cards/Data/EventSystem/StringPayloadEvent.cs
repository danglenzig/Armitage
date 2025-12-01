using UnityEngine;
namespace GameEvents
{
    [CreateAssetMenu(fileName = "StringPayloadEvent", menuName = "Events/StringPayloadEvent")]
    public class StringPayloadEvent : ScriptableObject
    {
        public event System.Action<string> OnEventTriggered;
        public void TriggerEvent(string payload)
        {
            OnEventTriggered?.Invoke(payload);
        }
    }
}


