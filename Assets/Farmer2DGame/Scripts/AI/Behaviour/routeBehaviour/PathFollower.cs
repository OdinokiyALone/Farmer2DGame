using UnityEngine;

public class PathFollower : Seek
{
    public Route path;
    public float pathOffset = 0;
    float currentParam;
    

    public override void Awake()
    {
        base.Awake();
        target = new GameObject();
        currentParam = 0;
    }

    public override Steering GetSteering()
    {
        currentParam = path.GetParam(transform.position, currentParam);
        float targetParam = currentParam + pathOffset;

        target.transform.position = path.GetPosition(targetParam);
        return base.GetSteering();
    }
}
