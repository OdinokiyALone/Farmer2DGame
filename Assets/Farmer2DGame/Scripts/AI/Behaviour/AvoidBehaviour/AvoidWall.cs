using UnityEngine;

public class AvoidWall : Seek
{
    public float avoidDistance;
    public float lookAhead;
     
     
    private Vector2 gDir;

    public override void Awake()
    {
        base.Awake();
        target = new GameObject();
    }
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        Vector3 position = transform.position;
        Vector3 rayVector = agent.body.linearVelocity.normalized * lookAhead; 
        Vector3 direction = rayVector;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, lookAhead);

        if(hit)
        {
            position = hit.point + hit.normal * avoidDistance;
            target.transform.position = position;
            steering = base.GetSteering();
        }
        return steering;
    }

    private void OnDrawGizmos()
    {
        gDir = agent == null ? transform.position + Vector3.right : agent.body.linearVelocity.normalized;
        gDir *= lookAhead;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,  gDir);
    }
}