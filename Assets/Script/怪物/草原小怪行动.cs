using UnityEngine;
using System.Collections;

public class 草原小怪行动: MonoBehaviour {

    public bool is徘徊;
    public float 徘徊距离;
    private float 起点;

    public enum Direction
    {
        Right,
        Left,
    }


    //小喽罗AI
    private Direction 小喽罗Direction = Direction.Left;
    private int groundLayer;//地面层
    private Rigidbody2D 小喽罗Rigidbody;
    public float moveSpeed = 100;
    void move()
    {
        if (!is徘徊)
        {
            if (!Physics2D.Raycast(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Vector2.down, 0.3f, groundLayer)
                   || Physics2D.Raycast(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Vector2.left, 1f, groundLayer)
                   || Physics2D.Raycast(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Vector2.right, 1f, groundLayer))
            {
                if (小喽罗Direction == Direction.Left)
                {
                    小喽罗Direction = Direction.Right;
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y);
                }
                else
                {
                    小喽罗Direction = Direction.Left;
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y);
                }
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
        else
        {
            if (起点 - 徘徊距离 > this.gameObject.transform.position.x || this.gameObject.transform.position.x > 起点)
            {
                if (小喽罗Direction == Direction.Left)
                {
                    小喽罗Direction = Direction.Right;
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y);
                }
                else
                {
                    小喽罗Direction = Direction.Left;
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y);
                }
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
        小喽罗Rigidbody.velocity = new Vector2((小喽罗Direction == Direction.Left ? -moveSpeed : moveSpeed) * Time.deltaTime, 小喽罗Rigidbody.velocity.y);
    }


    void Start()
    {
        小喽罗Rigidbody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("floor");
        起点 = this.gameObject.transform.position.x;
    }



    void Update()
    {
        move();
    }
}
