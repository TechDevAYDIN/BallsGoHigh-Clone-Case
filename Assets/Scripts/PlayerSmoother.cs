using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSmoother : MonoBehaviour
{
    public static PlayerSmoother SharedInstance;

    [SerializeField] GameObject playerController;
    [SerializeField] TextMeshProUGUI ballsText;
    [SerializeField] Image goalBar;
    public float goalDistance = 2273;

    public int ballsCount = 1;
    public List<GameObject> pooledObjects;
    public List<GameObject> activeBalls;
    void Awake()
    {
        SharedInstance = this;

        //Add active balls to list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                activeBalls.Add(pooledObjects[i]);
                pooledObjects.Remove(pooledObjects[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(activeBalls.Count > 0)
        {
            FollowController();
            CheckBallsCount();
            ballsText.text = ballsCount.ToString();
            goalBar.fillAmount = Mathf.Lerp(goalBar.fillAmount, this.transform.position.z / goalDistance, Time.deltaTime * 6); ;
        }
    }
    void CheckBallsCount()
    {
        if (ballsCount <= 0)
        {
            ballsCount = 0;
            playerController.GetComponent<MainPlayerController>().Death();
        }
            
        if (ballsCount <= activeBalls.Count+pooledObjects.Count)
        {
            if (ballsCount > activeBalls.Count)
            {
                AddBall();
                ballsText.text = ballsCount.ToString();
            }
            if (ballsCount < activeBalls.Count)
            {
                RemoveBall();
                ballsText.text = ballsCount.ToString();
            }
        }
        else if (ballsCount > activeBalls.Count + pooledObjects.Count)
        {
            if (110 > activeBalls.Count)
            {
                for (int i = 0; i < pooledObjects.Count; i++)
                {
                    pooledObjects[0].SetActive(true);
                    activeBalls.Add(pooledObjects[0]);
                    pooledObjects.Remove(pooledObjects[0]);
                }
                ballsText.text = ballsCount.ToString();
            }
        }
    }
    void FollowController()
    {
        Vector3 targetPos = transform.position;
        targetPos.x = playerController.transform.position.x;
        targetPos.y = playerController.transform.position.y;
        targetPos.z = playerController.transform.position.z;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);
        transform.position = smoothedPos;
        transform.rotation = Quaternion.Lerp(transform.rotation, playerController.transform.rotation, Time.deltaTime * 6);
    }
    void AddBall()
    {
        if(pooledObjects.Count > 1)
        {
            for (int i = activeBalls.Count-1; i < ballsCount; i++)
            {
                pooledObjects[0].SetActive(true);
                activeBalls.Add(pooledObjects[0]);
                pooledObjects.Remove(pooledObjects[0]);
            }
        }
    }
    void RemoveBall()
    {
        for (int i = activeBalls.Count - 1; i >= ballsCount; i--)
        {
            activeBalls[i].SetActive(false);
            pooledObjects.Add(activeBalls[i]);
            activeBalls.Remove(activeBalls[i]);
        }
    }
    public void Fall()
    {
        for (int i = 1; i < activeBalls.Count; i++)
        {
            activeBalls[i].GetComponent<Ball>().Fall();
        }
    }
    public void Fly()
    {
        for (int i = 1; i < activeBalls.Count; i++)
        {
            activeBalls[i].GetComponent<Ball>().Fly();
        }
    }
}
