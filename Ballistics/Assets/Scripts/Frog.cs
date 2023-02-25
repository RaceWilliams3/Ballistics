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
    private bool canJump = true;
    private float lastJump = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Time.timeScale = myTimeScale; // allow for slowing time to see what's happening
        rb = GetComponent<Rigidbody>();
        launchForce += UnityEngine.Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastJump + jumpFreq && canJump)
        {
            FiringSolution fs = new FiringSolution();
            Nullable<Vector3> aimVector = fs.Calculate(transform.position, getTargetPosition(), launchForce, Physics.gravity);
            if (aimVector.HasValue)
            {
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
    }

    Vector3 getTargetPosition()
    {
        Vector3 circleCenter = GetComponent<Transform>().position + GetComponent<Transform>().forward * wanderOffset;
        tempCircleTarget = new Vector2(circleCenter.x, circleCenter.z) + UnityEngine.Random.insideUnitCircle * wanderRadius;
        wanderTarget = new Vector3(tempCircleTarget.x, 0, tempCircleTarget.y);
        return wanderTarget;
    }
}