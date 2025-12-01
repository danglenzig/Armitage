using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class InputDetector : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event System.Action MouseEntered;
    public event System.Action MouseExited;
    public event System.Action MouseClicked;

    private bool pointerIsIn = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEntered?.Invoke();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExited?.Invoke();
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        MouseClicked?.Invoke();
    }
}
