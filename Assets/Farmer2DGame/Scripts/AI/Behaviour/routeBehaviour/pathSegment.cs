using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PathSegment
{
    public Vector2 a;
    public Vector2 b;

    public PathSegment() : this(Vector2.zero, Vector2.zero) { }

    public PathSegment(Vector2 a, Vector2 b)
    {
        this.a = a;
        this.b = b;
    }
}