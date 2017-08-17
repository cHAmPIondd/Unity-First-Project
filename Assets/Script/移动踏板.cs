using UnityEngine;
using System.Collections;

public class 移动踏板 : MonoBehaviour {


    public enum Direction
    {
        Right,
        Left,
    }
    public float 徘徊距离;
    private float 起点;
    private Rigidbody2D 踏板Rigidbody;
    private Direction 踏板Direction = Direction.Left;
    public float moveSpeed=100;

    public bool isVertical;
    public bool isRight;
    void verticalMove()
    {
        if (起点 - 徘徊距离 > this.gameObject.transform.position.y || this.gameObject.transform.position.y > 起点)
        {
            if (踏板Direction == Direction.Left)
            {
                踏板Direction = Direction.Right;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+0.1f, -1);
            }
            else
            {
                踏板Direction = Direction.Left;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-0.1f, -1);
            }
        }

        踏板Rigidbody.velocity = new Vector2( 踏板Rigidbody.velocity.x,(踏板Direction == Direction.Left ? -moveSpeed : moveSpeed));
    }
    void horizontalMove()
    {
        if (!isRight)
        {
            if (起点 - 徘徊距离 > this.gameObject.transform.position.x || this.gameObject.transform.position.x > 起点)
            {
                if (踏板Direction == Direction.Left)
                {
                    踏板Direction = Direction.Right;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y, -1);
                }
                else
                {
                    踏板Direction = Direction.Left;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y, -1);
                }
            }
        }
        else
        {
            if (起点 + 徘徊距离 < this.gameObject.transform.position.x || this.gameObject.transform.position.x < 起点)
            {
                if (踏板Direction == Direction.Left)
                {
                    踏板Direction = Direction.Right;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.1f, gameObject.transform.position.y, -1);
                }
                else
                {
                    踏板Direction = Direction.Left;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.1f, gameObject.transform.position.y, -1);
                }
            }
        }
         踏板Rigidbody.velocity = new Vector2((踏板Direction == Direction.Left ? -moveSpeed : moveSpeed), 踏板Rigidbody.velocity.y);
    }
	// Use this for initialization
	void Start () {
        if(isVertical)
            起点 = this.gameObject.transform.position.y;
        else
            起点 = this.gameObject.transform.position.x;
        踏板Rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isVertical)
            verticalMove();
        else
            horizontalMove();
	}
}
