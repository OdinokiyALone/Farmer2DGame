
using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class Agent : MonoBehaviour
{
    public float maxSpeed;
    public float maxAccel;
    //spublic float orientation;
    //public Vector2 velocity;
    protected Steering steering;

    [HideInInspector]public Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D> ();
        body.linearVelocity = Vector2.zero;
        steering = new Steering();
    }
    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }

    public virtual void Update()
    {
        //Vector3 displacement = body.velocity * Time.deltaTime;
        //body.velocity = displacement;

        body.linearVelocity += steering.linear * Time.deltaTime;
        if (body.linearVelocity.magnitude > maxSpeed)
        {
            body.linearVelocity = body.linearVelocity.normalized * maxSpeed;
        }
        if (steering.linear.sqrMagnitude == 0)
        {
            body.linearVelocity = Vector3.zero;
        }
        steering = new Steering();
    }
    public virtual void LateUpdate()
    {
    }

}