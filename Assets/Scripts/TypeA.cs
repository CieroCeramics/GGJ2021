﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TypeA : SoulBehavior
{
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    public float fleeRadius;

    //====================================================================================================================//
    
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }


    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        float dist = Vector3.Distance(playerController.transform.position, transform.position);

        if (dist > fleeRadius)
        {
            Wander();
        }
        else 
            Flee();
    }

    //====================================================================================================================//
    
    private void Wander()
    {
        //print("wander");
        
        if (timer < wanderTimer)
            return;
        
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
        timer = 0;
    }

    private void Flee()
    {
        //print("Flee:");
        Vector3 a = transform.position;
        Vector3 b = transform.position;

        Vector3 c = a - b;
        c = Vector3.Normalize(c);
        agent.SetDestination(c);
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
