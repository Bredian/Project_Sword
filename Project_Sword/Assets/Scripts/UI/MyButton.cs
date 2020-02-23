
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Stamina.pressedPause = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Stamina.pressedPause = 1f;
    }
}