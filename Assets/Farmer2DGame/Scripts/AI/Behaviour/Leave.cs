using UnityEngine;

public class Leave : AgentBehaviour
{
    public float escapeRadius = .1f;
    public float dangerRadius = .1f;
    public float timeToTarget = .1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        Vector3 direction = transform.position - target.transform.position;
        float distance = direction.magnitude;
        if(distance > dangerRadius)
        {
            return steering;
        }
        float reduce;
        if (distance < escapeRadius)
        {
            reduce = 0;
        }
        else reduce = distance / dangerRadius * agent.maxSpeed;
        float targetSpeed = agent.maxSpeed - reduce;

        Vector2 desiredVelocity = direction;
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;
        steering.linear = desiredVelocity - agent.body.linearVelocity;
        steering.linear /= timeToTarget;
        if (steering.linear.magnitude > agent.maxAccel)
        {
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
        }

        return steering;

    }
}