using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherEnemy : Enemy
{
    private Vector2 direction = new Vector2(-1, 0);
    private float timer = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// クエストNo3 - 動きの違う敵を生成しよう -
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1.5f)
        {
            direction.x *= -1;
            timer = 0;
            Jump();
        }
        Move(direction.x);
    }
}
