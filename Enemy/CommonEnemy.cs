using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy : Enemy
{
    private Vector2 direction = new Vector2(-1, 0);
    private float timer = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// クエストNo1 - 敵キャラを動かそう！ -
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.5f)
        {
            direction.x *= -1;
            timer = 0;
        }
        Move(direction.x);
    }
}
