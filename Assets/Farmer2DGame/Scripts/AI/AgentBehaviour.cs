
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    public GameObject target;
    protected Agent agent;
    

    public virtual void Awake()
    {
        agent = GetComponent<Agent>();
    }

    public virtual void Update()
    {
        agent.SetSteering(GetSteering());
    }
    public virtual Steering GetSteering()
    {
        return new Steering();
    }

    //public Vector2 GetOriAsVec(float orientation)
    //{
    //    Vector2 vector = Vector2.zero;

    //}
    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(target == null ? transform.position : target.transform.position, 1);
    }

}