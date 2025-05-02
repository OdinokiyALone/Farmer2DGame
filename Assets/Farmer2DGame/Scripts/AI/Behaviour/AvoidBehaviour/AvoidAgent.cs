using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class AvoidAgent : AgentBehaviour
{
    public float collisionRadius = .4f;
    GameObject[] targets;

    private void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Agent");
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        float shortestTime = Mathf.Infinity;
        GameObject firstTarget = null;
        float firstMinSeparation = 0f;
        float firstDistance  = 0f;
        Vector3 firstRelativePos = Vector3.zero;
        Vector3 firstRealativeVel = Vector3.zero;

        foreach (GameObject t in targets)
        {
            Vector3 relativePos;
            Agent targetAgent = t.GetComponent<Agent>();
            relativePos = t.transform.position - transform.position;
            Vector3 relativeVel = targetAgent.body.linearVelocity - agent.body.linearVelocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = Vector3.Dot(relativePos, relativeVel);
            timeToCollision /= relativeSpeed * relativeSpeed * -1;
            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;
            if (minSeparation > 2 * collisionRadius)
            {
                continue;
            }
            if(timeToCollision > 0 && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = t;
                firstMinSeparation = minSeparation;
                firstRelativePos = relativePos;
                firstRealativeVel = relativeVel;
            }
        }
        if (firstTarget == null) return steering;

        if (firstMinSeparation < 0 || firstDistance < 2 * collisionRadius)
        {
            firstRelativePos = firstTarget.transform.position;
        }
        else firstRelativePos += firstRealativeVel * shortestTime;
        firstRelativePos.Normalize();
        steering.linear = -firstRelativePos * agent.maxSpeed;
        return steering;
    }
}