using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    public bool isStarted;
    [SerializeField]private bool onGround, ballsFlying, isAlive = true;
    [SerializeField] float startSpeed;
    [SerializeField] float perfectSpeed;
    [SerializeField] float inAirRotationLimit, onGroundRotationLimit, rotationSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem particles1;
    [SerializeField] ParticleSystem particles2;
    [SerializeField] GameObject levelPassedCanvas;
    [SerializeField] GameObject levelFailedCanvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted)
            RayCheck();
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        TouchControl();
        if (isStarted)
        {
            rb.velocity = transform.up * startSpeed;
        }
        if (onGround)
        {
            Quaternion quaternion = Quaternion.Euler(onGroundRotationLimit, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * 6);
        }
        
        if (!onGround)
        {
            Quaternion quaternion = Quaternion.Euler(inAirRotationLimit, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion,Time.deltaTime * rotationSpeed);
        }

    }
    void TouchControl()
    {
        if (isAlive && !onGround)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rotationSpeed = 2.4f;
            }
            if (Input.GetMouseButton(0))
            {
                rotationSpeed = 2.4f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                rotationSpeed = .15f;
            }
        }
        
    }
    void RayCheck()
    {
        Ray frontRay = new Ray(transform.position, transform.forward);
        Ray backRay = new Ray(transform.position - transform.up * 2.5f, transform.forward);
        if (Physics.Raycast(frontRay, out RaycastHit hitFront, 10, 1 << 10) &&
            Physics.Raycast(backRay, out RaycastHit hitback, 10, 1 << 10))
        {
            
            if(hitFront.distance < 5 && ballsFlying)
            {
                PlayerSmoother.SharedInstance.Fall();
                particles1.Stop();
                particles2.Stop();
                ballsFlying = false;
            }
            if (hitFront.distance >= 7 && !ballsFlying)
            {
                PlayerSmoother.SharedInstance.Fly();
                Invoke("particlesStep1", .5f);
                Invoke("particlesStep2", 1f);
                ballsFlying = true;
            }
            if (hitFront.distance > hitback.distance - .15f && onGround)
            {
                onGroundRotationLimit = inAirRotationLimit;
            }
            else if (hitFront.distance < hitback.distance- .15f && onGround)
            {
                onGroundRotationLimit = 25;
            }
            else
            {
                onGroundRotationLimit = 25;
            }
            /*
            else
            {
                onGroundRotationLimit = transform.rotation.x;
            }*/
        }
    }
    public void particlesStep1()
    {
        if(!onGround)
            particles1.Play();
    }
    public void particlesStep2()
    {
        if (!onGround)
            particles2.Play();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10)
        {
            //PlayerSmoother.SharedInstance.Fall();
            onGround = true;
        }
        if(collision.gameObject.layer == 16)
        {
            isStarted = false;
            isAlive = false;
            levelPassedCanvas.SetActive(true);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 10)
        {
            onGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.layer == 10)
        {
            //PlayerSmoother.SharedInstance.Fly();
            onGround = false;
            rotationSpeed = .15f;
        }
    }
    public void Death()
    {
        isStarted = false;
        isAlive = false;
        particles1.Stop();
        particles2.Stop();
        levelFailedCanvas.SetActive(true);
        this.gameObject.SetActive(false);
        Destroy(this);
    }
}
