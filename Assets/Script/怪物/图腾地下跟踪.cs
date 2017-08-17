using UnityEngine;
using System.Collections;

public class 图腾地下跟踪 : MonoBehaviour
{

    private bool is遭遇主角;
    public float 遭遇距离 = 15f;
    public float 移动速度 = 5.8f;
    private Transform cameraTransform;
    public Rigidbody2D Rigidbody2D;
    // Use this for initialization
    void Start()
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!is遭遇主角)
        {
            if (cameraTransform.position.x + 遭遇距离 > transform.position.x)
            {
                is遭遇主角 = true;
            }
        }
        else
        {
            move();
        }
    }
    private float moveTimer = 0;
    void move()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer > 1)
        {
            moveTimer = 0;
            if (cameraTransform.position.x > transform.position.x)
            {
                if (Rigidbody2D.velocity.x > 0)//在右 往右
                {
                    if (Random.Range(0, 1f) > 0.8)
                    {
                        Rigidbody2D.velocity = new Vector2(-移动速度, 0);
                    }
                }
                else//在右 往左
                {
                    if (Random.Range(0, 1f) > 0.5)
                    {
                        Rigidbody2D.velocity = new Vector2(移动速度, 0);
                    }
                }
            }
            else
            {
                if (Rigidbody2D.velocity.x > 0)//在左 往右
                {
                    if (Random.Range(0, 1f) > 0.5)
                    {
                        Rigidbody2D.velocity = new Vector2(-移动速度, 0);
                    }
                }
                else//在左 往左
                {
                    if (Random.Range(0, 1f) > 0.8)
                    {
                        Rigidbody2D.velocity = new Vector2(移动速度, 0);
                    }
                }
            }
        }
    }
}