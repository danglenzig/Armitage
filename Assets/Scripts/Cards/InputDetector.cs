using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class InputDetector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool pointerIsIn = false;
    private string myID = System.Guid.NewGuid().ToString();
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerIsIn = true;
        Debug.Log($"Enter {myID}");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pointerIsIn = false;
        Debug.Log($"Exit {myID}");
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"click {myID}");
    }
}
