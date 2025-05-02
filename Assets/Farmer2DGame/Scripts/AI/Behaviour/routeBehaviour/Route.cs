using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public List<GameObject> nodes;
    List<PathSegment> segments;

    private void Start()
    {
        segments = GetSegments();
    }

    public float GetParam(Vector2 position, float lastParam)
    {
        float param = 0f;
        PathSegment currentSegment = null;
        float tempParam = 0;
        foreach (PathSegment ps in segments)
        {
            tempParam += Vector2.Distance(ps.a, ps.b);
            if (lastParam <= tempParam)
            {
                currentSegment = ps;
                break;
            }
        }
        if (currentSegment == null) return 0;

        Vector2 currPos = position - currentSegment.a;
        Vector2 segmentDirection = currentSegment.a - currentSegment.b;
        segmentDirection.Normalize();

        Vector2 pointInSegment = Vector3.Project(currPos, segmentDirection);

        param = tempParam - Vector3.Distance(currentSegment.a, currentSegment.b);

        param += pointInSegment.magnitude;
        return param;
    }

    public Vector2 GetPosition(float param)
    {
        Vector2 position = Vector2.zero;
        PathSegment currentSegment = null;
        float tempParam = 0;
        foreach (PathSegment ps in segments)
        {
            tempParam += Vector2.Distance(ps.a, ps.b);
            if (param <= tempParam)
            {
                currentSegment = ps;
                break;
            }
        }
        if (currentSegment == null) return Vector2.zero;

        Vector2 segmentDirection = currentSegment.b - currentSegment.a;
        segmentDirection.Normalize();
        tempParam -= Vector2.Distance(currentSegment.a, currentSegment.b);
        tempParam = param - tempParam;
        position = currentSegment.a + segmentDirection * tempParam;
        return position;
    }

    public List<PathSegment> GetSegments()
    {
        List<PathSegment> segments = new List<PathSegment>();
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            Vector2 src = nodes[i].transform.position;
            Vector2 dst = nodes[i + 1].transform.position;
            PathSegment segment = new PathSegment(src,dst);
            segments.Add(segment);
        }
        return segments;
    }

    private void OnDrawGizmos()
    {
        Vector3 direction;
        Color tmp = Gizmos.color;
        Gizmos.color = Color.red;
        int i;
        for (i = 0; i < nodes.Count - 1; i++)
        {
            Vector3 src = nodes[i].transform.position;
            Vector3 dst = nodes[i + 1].transform.position;
            direction = dst - src;
            Gizmos.DrawRay(src, direction);
        }
        Gizmos.color = tmp;
    }
}

