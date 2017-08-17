using UnityEngine;
using System.Collections;

public class 草原子弹 : MonoBehaviour {

    public float moveSpeed = 10;
    private GameObject player_1P;
    private GameObject player_2P;
    private Transform target;
    public float 子弹跟踪范围 = 30;
    public float 攻击高低范围 = 1;
    public float 最大速度=10;
    public float 转向力 = 10;
    private Rigidbody2D 子弹Rigidbody;
    public float 子弹存在时间;
    private float timer = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            other.transform.parent.gameObject.SendMessage("BeHit");
            Destroy(gameObject);
        }
    }
	void Start () {
        子弹Rigidbody = GetComponent<Rigidbody2D>();
        player_1P = GameObject.Find("火柴人1P");
        player_2P = GameObject.Find("火柴人2P");
        if (游戏进度控制._instance.当前1P状态)
        {
            if ((Mathf.Abs(player_1P.transform.position.x - gameObject.transform.position.x) < 子弹跟踪范围
                    && Mathf.Abs(player_1P.transform.position.y - gameObject.transform.position.y) < 攻击高低范围))
            {
                target = player_1P.transform;
            }
            else
            {
                target = player_2P.transform;
            }
        }
        else
        {
            if ((Mathf.Abs(player_2P.transform.position.x - gameObject.transform.position.x) < 子弹跟踪范围
                   && Mathf.Abs(player_2P.transform.position.y - gameObject.transform.position.y) < 攻击高低范围))
            {
                target = player_2P.transform;
            }
            else
            {
                target = player_1P.transform;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (target.position.x > gameObject.transform.position.x)
            子弹Rigidbody.AddForce(new Vector2(转向力, 0));
        else
            子弹Rigidbody.AddForce(new Vector2(-转向力, 0));
        if (子弹Rigidbody.velocity.x > 最大速度)
            子弹Rigidbody.velocity = new Vector2(最大速度,0);
        if (子弹Rigidbody.velocity.x < -最大速度)
            子弹Rigidbody.velocity = new Vector2(-最大速度, 0);   
        timer += Time.deltaTime;
        if(timer>子弹存在时间)
            Destroy(gameObject);
	}
}
