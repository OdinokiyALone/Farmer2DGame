using UnityEngine;

public class Arrive : AgentBehaviour
{
    public float targetRaduis = .1f;
    public float slowRadius = .1f;
    public float timeToTarge = .1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        Vector2 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;
        float targetSpeed;
        if(distance <targetRaduis)
        {
            return steering;
        }
        if(distance > slowRadius)
        {
            targetSpeed = agent.maxSpeed;
        }
        else targetSpeed = agent.maxSpeed * distance / slowRadius;

        Vector2 desiredVelocity = direction;
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;
        steering.linear = desiredVelocity - agent.body.linearVelocity;
        steering.linear /= timeToTarge;
        if(steering.linear.magnitude > agent.maxAccel)
        {
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
        }


        return steering;
    }
}