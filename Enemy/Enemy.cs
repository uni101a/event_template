using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _moveSpeed = 1.5f;
    [SerializeField] protected float _jumpSpeed = 3f;

    protected Rigidbody2D _rigidbody;


    protected virtual void Move(float xDir)
    {
        float xSpeed = xDir * _moveSpeed;

        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
    }

    protected virtual void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
    }

    public virtual void Damaged()
    {
        Dead();
    }

    public virtual void Dead()
    {
        Destroy(gameObject);
    }
}
