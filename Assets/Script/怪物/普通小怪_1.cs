using UnityEngine;
using System.Collections;

public class 普通小怪_1 : MonoBehaviour {

    public bool is徘徊;
    public float 徘徊距离;
    private float 起点;

    public enum Direction
    {
        Right,
        Left,
    }

    public int hp = 1;

    public void BeHit()
    {
        hp--;
        if(hp<=0)
        {
            Death();
        }
    }
    
    public GameObject 小喽罗死亡粒子 = null;

    private int 加能量值 = 1;
    private RectTransform 能量遮罩_1P;
    private RectTransform 能量遮罩_2P;
    void Death()
    {
        GameObject 小喽罗死亡粒子_ex = (GameObject)Instantiate(小喽罗死亡粒子);
        Vector3 enemyPos = transform.position;
        小喽罗死亡粒子_ex.transform.position = new Vector3(enemyPos.x, enemyPos.y, enemyPos.z + 1.0f);
        游戏进度控制._instance.分数 += 50;

        if (游戏进度控制._instance.当前1P状态)
            游戏进度控制._instance.能量_2P += 加能量值;
        else
            游戏进度控制._instance.能量_1P += 加能量值;

        if (游戏进度控制._instance.能量_1P > 游戏进度控制._instance.虚体能量Max)
            游戏进度控制._instance.能量_1P = 游戏进度控制._instance.虚体能量Max;
        if (游戏进度控制._instance.能量_2P > 游戏进度控制._instance.虚体能量Max)
            游戏进度控制._instance.能量_2P = 游戏进度控制._instance.虚体能量Max;

        能量遮罩_1P.sizeDelta = new Vector2(游戏进度控制._instance.能量_1P * 635 / 30f+0.01f, 能量遮罩_1P.sizeDelta.y);
        能量遮罩_2P.sizeDelta = new Vector2(游戏进度控制._instance.能量_2P * 635 / 30f+0.01f, 能量遮罩_2P.sizeDelta.y);

        Destroy(this.gameObject);
    }

    //小喽罗AI
    private Direction 小喽罗Direction=Direction.Left;
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
            if (起点 - 徘徊距离 > this.gameObject.transform.position.x || this.gameObject.transform.position.x>起点)
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


	void Start () {
        小喽罗Rigidbody = GetComponent<Rigidbody2D>();
        groundLayer=LayerMask.GetMask("floor");
        起点 = this.gameObject.transform.position.x;

        能量遮罩_1P = GameObject.Find("Canvas/游戏UI/1P状态/能量/遮罩").GetComponent<RectTransform>();
        能量遮罩_2P = GameObject.Find("Canvas/游戏UI/2P状态/能量/遮罩").GetComponent<RectTransform>();
	}
	


	void Update () {
        move();
	}
}
