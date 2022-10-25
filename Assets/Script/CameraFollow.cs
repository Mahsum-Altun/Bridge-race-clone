using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public GameObject winTransform;
    float winSpeed = 70f;
    public Vector3 offset;
    public static bool win = false;

    public float smoothSpeed = 0.1f;
    private InputManager inputManager;

    private void LateUpdate()
    {
        if (win == false)
        {
            SmoothFollow();
        }
        else
        {
            Win();
        }
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
        transform.LookAt(target);
    }

    public void Win()
    {
        transform.position = Vector3.MoveTowards(transform.position, winTransform.transform.position, Time.deltaTime * winSpeed);
        inputManager.speed = 0f;
    }
}
