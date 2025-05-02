using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{
    public float maxPrediction;
    private GameObject targetAux;
    private Agent targetAgent;

    public override void Awake()
    {
        base.Awake();
        targetAgent = target.GetComponent<Agent>();
        targetAux = target;
        target = new GameObject();
    }

    private void OnDestroy()
    {
        Destroy(targetAux);
    }
    public override Steering GetSteering()
    {
         Vector2 direction = targetAux.transform.position - transform.position;
        float distance = direction.magnitude;
        float speed = agent.body.linearVelocity.magnitude;
        float prediction;
        if(speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else prediction = distance / speed;
        target.transform.position = targetAux.transform.position;
        target.transform.position += (Vector3)targetAgent.body.linearVelocity * prediction;

        return base.GetSteering();
    }
}
