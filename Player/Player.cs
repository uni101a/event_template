using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float _moveSpeed = 5;
    [SerializeField] float _jumpSpeed = 7;

    private GameManager _gameManager;
    private Rigidbody2D _rigidbody;

    private int jumpCount = 0;

    void Awake()
    {
        _gameManager = GameManager.GetInstance();
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();

        MoveCamera();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                CollideGround(collision);
                break;

            case "Enemy":
                OnHitEnemy(collision);
                break;

            case "Goal":
                Goal();
                break;

            case "Trap":
                Dead();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// チュートリアル
    /// </summary>
    private void Move()
    {
        float direction = Input.GetAxis("Horizontal");
        float xSpeed = direction * _moveSpeed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
    }


    /// <summary>
    /// クエストNo2 - 二段ジャンプをしよう！ -
    /// </summary>
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpCount++;
            _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private void CollideGround(Collision2D collision)
    {
        if(IsStepOn(gameObject, collision))
        {
            jumpCount = 0;
        }
    }


    /// <summary>
    /// クエストNo4 - 敵に当たったら死のう！ -
    /// クエストNo5 - 敵を倒そう！ -
    /// </summary>
    /// <param name="collision"></param>
    private void OnHitEnemy(Collision2D collision)
    {
        Debug.Log("敵と衝突したよ！");
        if (IsStepOn(gameObject, collision))
        {
            collision.gameObject.GetComponent<IDamageable>().Damaged();
        }
        else
        {
            Damaged();
        }
    }

    /// <summary>
    /// プレイヤーオブジェクトが対象のオブジェクトを上から踏んだかを判定する
    /// </summary>
    /// <param name="actObject">Playerオブジェクト</param>
    /// <param name="collision">衝突した対象となるオブジェクト</param>
    /// <returns></returns>
    private bool IsStepOn(GameObject actObject, Collision2D collision)
    {
        ContactPoint2D[] points = collision.contacts;
        float acterLowerPos = actObject.transform.position.y - actObject.transform.localScale.y / 2;

        foreach (ContactPoint2D p in points)
        {
            return p.point.y < acterLowerPos;
        }

        return false;
    }

    /// <summary>
    /// クエストNo4, 番外3
    /// </summary>
    public void Damaged()
    {
        Debug.Log("ダメージを受けたよ！");
    }

    public void Dead()
    {
        Debug.Log("YOU DEAD");
    }


    /// <summary>
    /// クエストNo7 - トラップを作ろう！ -
    /// </summary>
    private void OnHitTrap()
    {
        Debug.Log("トラップに当たったよ！");
    }


    /// <summary>
    /// クエストNo8 - コースを延長しよう！ カメラ移動 - 
    /// </summary>
    private void MoveCamera()
    {
        Vector3 cameraPos = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = cameraPos;
    }


    /// <summary>
    /// クエストNo9 - ゴールを作ろう！ - 
    /// </summary>
    private void Goal()
    {
        Debug.Log("ゴールしたよ！");
        _gameManager.Goal();
    }

}
