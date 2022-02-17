using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MomController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    public float fov;

    public bool canSeeKid;

    public Transform[] points;
    private int destPoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

        agent.autoBraking = false;

        NextPoint();
    }

    void NextPoint()
    {
        Debug.Log(destPoint);
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;
       
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        
        
    }


    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(agent.remainingDistance);
        Vector3 distanceToPlayer = player.position - transform.position;
        float viewAngle = Vector3.Angle(distanceToPlayer, transform.forward);
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(.7f, 0, 0)), Color.blue);
        RaycastHit hit;
        if (viewAngle <= fov)
        {
             Debug.DrawRay(transform.transform.position, distanceToPlayer);
             if (Physics.Raycast(transform.position, distanceToPlayer, out hit))
             {
                 //Debug.Log(hit.transform.gameObject.name);
                 if (hit.transform.gameObject.name == "Player")
                 {
                     agent.destination = player.position;
                     Debug.Log("SPOTTED");
                 }
             }
        }
        else*/ 
        if (canSeeKid)
        {
            Vector3 distanceToPlayer = player.position - transform.position;
            RaycastHit hit;
            Debug.DrawRay(transform.transform.position, distanceToPlayer);
            if (Physics.Raycast(transform.position, distanceToPlayer, out hit))
            {
                //Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.name == "Player")
                {
                    agent.destination = player.position;
                    Debug.Log("SPOTTED");
                }
            }
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f && !canSeeKid)
        {
            destPoint = (destPoint + 1) % points.Length;
            NextPoint();
        }
        else
        {
            Debug.DrawLine(transform.transform.position, points[destPoint].position);
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.name == "Player")
        {
            canSeeKid = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.name == "Player")
        {
            StartCoroutine("LoseKid");
        }
    }

    IEnumerator LoseKid()
    {
        yield return new WaitForSeconds(5);
        canSeeKid = false;

    }
}
