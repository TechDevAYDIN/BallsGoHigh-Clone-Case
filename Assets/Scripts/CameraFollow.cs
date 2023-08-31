using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerPos;
    private Vector3 offset;

    void Start()
    {
        Application.targetFrameRate = 60;
        offset = transform.position - playerPos.transform.position;
    }
    void LateUpdate()
    {
        Vector3 targetPos = transform.position;
        targetPos.x = playerPos.transform.position.x + offset.x;
        targetPos.y = playerPos.transform.position.y + offset.y;
        targetPos.z = playerPos.transform.position.z + offset.z;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 8);
        transform.position = smoothedPos;
    }
}
