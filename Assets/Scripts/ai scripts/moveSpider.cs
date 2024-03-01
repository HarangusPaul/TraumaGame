using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class moveSpider : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    public Transform player;
    
    public Transform pointOfInterest;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {

        try
        {
            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    if (player != null)
                    {
                        float dist = Vector3.Distance(player.position, transform.position); //so you can see with gizmos
                        Debug.Log(dist);
                    }

                    agent.SetDestination(point);
                }
            }
        }
        catch (Exception e)
        {
            // Console.WriteLine(e);
            // throw;
        }
       
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        if (player != null)
        {
            float dist = Vector3.Distance(player.position, randomPoint);
            int count = 0;
            while (dist < range/2)
            {
                randomPoint = center + Random.insideUnitSphere * range;
                dist = Vector3.Distance(player.position, randomPoint);
                count++;
                if(count >= 20)
                    break;
            }
        }

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation

            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void disableSpider()
    {
        gameObject.active = false;
    }
}