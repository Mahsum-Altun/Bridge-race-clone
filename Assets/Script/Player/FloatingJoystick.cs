using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[System.Serializable]
public enum JoyStickDirection { Horizontal, Vertical, Both }


public class FloatingJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public JoyStickDirection joyStickDirection = JoyStickDirection.Both;

    public RectTransform background;
    public RectTransform handle;

    [Range(0, 2f)] public float handleLimit = 1f;
    private Vector2 input = Vector2.zero;
    //OutPut
    public float Vertical { get { return input.y; } }
    public float Horizontal { get { return input.x; } }
    Vector2 joyPosition = Vector2.zero;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        joyPosition = eventData.position;
        background.position=eventData.position;
        handle.anchoredPosition=Vector2.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joyDriection = eventData.position - joyPosition;
        input = (joyDriection.magnitude > background.sizeDelta.x / 2f) ? joyDriection.normalized :
        joyDriection / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (input * background.sizeDelta.x / 2f) * handleLimit;
        if (joyStickDirection == JoyStickDirection.Horizontal)
        {
            input = new Vector2(input.x, 0f);
        }
        if (joyStickDirection == JoyStickDirection.Vertical)
        {
            input = new Vector2(input.y, 0f);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }


}
