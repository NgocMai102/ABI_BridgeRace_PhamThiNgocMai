using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _UI.Scripts.UI;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
