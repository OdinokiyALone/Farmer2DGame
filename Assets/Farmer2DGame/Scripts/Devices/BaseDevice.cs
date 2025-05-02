using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseDevice : MonoBehaviour
{
    private bool _inCollision = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _inCollision = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _inCollision = false;
    }

    private void Update()
    {
        if (_inCollision)
        {
            Managers.Player.AllowAttack = false;
            if (Input.GetButtonDown("Attack"))
            {
                OnActivate();
            }
        }
    }
    
    public virtual void OnActivate()
    {

    }



}
