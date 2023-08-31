using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChoiceGate : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] bool positive;


    void Start()
    {
        if (positive)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            GetComponentInChildren<TextMeshPro>().text = "+" + value;
        }
        if (!positive)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            GetComponentInChildren<TextMeshPro>().text = "-" + value;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (positive)
                PlayerSmoother.SharedInstance.ballsCount += value;
            if (!positive)
                PlayerSmoother.SharedInstance.ballsCount -= value;

            Destroy(this.gameObject);
        }
    }
}
