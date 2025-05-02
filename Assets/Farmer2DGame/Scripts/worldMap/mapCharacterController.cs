using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class mapCharacterController : MonoBehaviour
{
    Rigidbody2D character;
    public float Speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        character.linearVelocity =  new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical")) * Speed;    
    }
}
