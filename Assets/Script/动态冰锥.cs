using UnityEngine;
using System.Collections;

public class 动态冰锥 : MonoBehaviour
{

    public float max距离;
    public float min距离;
    public float max重力;
    public float min重力;
    private Transform Transform_1P;
    private Transform Transform_2P;
    private float 实际距离;
    private float 实际重力;
    private Rigidbody2D Rigidbody;

    public bool is回头冰锥;
    private bool is第一次经过=true;
    // Use this for 
    void Start()
    {
        Transform_1P = GameObject.Find("火柴人1P").GetComponent<Transform>();
        Transform_2P = GameObject.Find("火柴人2P").GetComponent<Transform>();
        实际距离 = Random.Range(min距离, max距离);
        实际重力 = Random.Range(min重力, max重力);
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            other.transform.parent.gameObject.SendMessage("BeHit");
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!is回头冰锥)
        {
            if (Transform_1P.position.x + 实际距离 >= gameObject.transform.position.x && 游戏进度控制._instance.当前1P状态)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                Rigidbody.gravityScale = 实际重力;
            }
            if (Transform_2P.position.x + 实际距离 >= gameObject.transform.position.x && !游戏进度控制._instance.当前1P状态)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                Rigidbody.gravityScale = 实际重力;
            }
        }
        else
        {
            if (Transform_1P.position.x -实际距离 >= gameObject.transform.position.x && 游戏进度控制._instance.当前1P状态)
            {
                is第一次经过 = false;
            }
            if (!is第一次经过 && Transform_1P.position.x - 实际距离+0.1f < gameObject.transform.position.x && 游戏进度控制._instance.当前1P状态)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                Rigidbody.gravityScale = 实际重力;
            }
            if (Transform_2P.position.x - 实际距离 >= gameObject.transform.position.x && !游戏进度控制._instance.当前1P状态)
            {
                is第一次经过 = false;
            }
            if (!is第一次经过 && Transform_2P.position.x - 实际距离 + 0.1f < gameObject.transform.position.x && !游戏进度控制._instance.当前1P状态)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                Rigidbody.gravityScale = 实际重力;
            }
        }
        if (gameObject.transform.position.y < -40)
        {
            Destroy(gameObject);
        }
    }
}
