using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 startLocalPos;

    private bool inAir;
    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (inAir)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                 new Vector3(startLocalPos.x / 3 * 2, startLocalPos.y / 3 * 2, startLocalPos.z * 2), Time.deltaTime * 5);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,startLocalPos, Time.deltaTime * 4);
        }
    }
    public void Fly()
    {
        inAir = true;
    }
    public void Fall()
    {
        inAir = false;
    }
}
