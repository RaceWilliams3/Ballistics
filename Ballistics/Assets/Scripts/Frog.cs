using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public float myTimeScale = 1.0f;
    public GameObject target;
    public float launchForce;
    Rigidbody rb;
    Vector3 startPos;

    Vector3 tempCircleTarget;
    Vector3 wanderTarget;
    public float wanderRadius;
    public float wanderOffset;

    public float jumpFreq;
    private float lastJump = 0f;

    private FrogController fc;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Time.timeScale = myTimeScale; // allow for slowing time to see what's happening
        rb = GetComponent<Rigidbody>();
        fc = GetComponent<FrogController>();
        launchForce += UnityEngine.Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastJump + jumpFreq && checkGround())
        {
            FiringSolution fs = new FiringSolution();
            Nullable<Vector3> aimVector = fs.Calculate(transform.position, getTargetPosition(), launchForce, Physics.gravity);
            if (aimVector.HasValue)
            {
                fc.Jump();
                rb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange); 
            }
            lastJump = Time.time;
        }   

        if (Input.GetKeyDown(KeyCode.R))
        {
            // reset
            rb.isKinematic = true;
            transform.position = startPos;
            rb.isKinematic = false;
        }

        transform.forward = Vector3.Lerp(transform.forward, rb.velocity, Time.deltaTime * 1);
    }

    bool checkGround()
    {
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), 1))
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    Vector3 getTargetPosition()
    {
        Vector3 circleCenter = GetComponent<Transform>().position + GetComponent<Transform>().forward * wanderOffset;
        tempCircleTarget = new Vector2(circleCenter.x, circleCenter.z) + UnityEngine.Random.insideUnitCircle * wanderRadius;
        wanderTarget = new Vector3(tempCircleTarget.x, 0, tempCircleTarget.y);
        return wanderTarget;
    }


}