using UnityEngine;
using System.Collections;

public class 虫子 : MonoBehaviour
{
    public int 伤害 = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            for (int i = 0; i < 伤害; i++)
                other.transform.parent.gameObject.SendMessage("BeHit");
            GetComponent<PolygonCollider2D>().enabled = false;
            if (!other.transform.parent.GetComponent<Player>().is1P && 游戏进度控制._instance.当前1P状态
                || other.transform.parent.GetComponent<Player>().is1P && !游戏进度控制._instance.当前1P状态)
            {
                Destroy(gameObject);
            }
            Destroy(gameObject, 5);
        }
    }

    public float 移动速度=2;
    public float 距离=1;

    private Rigidbody2D Rigidbody2D;

    private Transform playerTransform_1P;
    private Transform playerTransform_2P;

    private bool is触发;
    // Use this for initialization
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        playerTransform_1P = GameObject.Find("火柴人1P").GetComponent<Transform>();
        playerTransform_2P = GameObject.Find("火柴人2P").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!is触发)
        {
            if (playerTransform_1P.position.y + 距离 >= gameObject.transform.position.y && 游戏进度控制._instance.当前1P状态)
            {
                Rigidbody2D.velocity = new Vector3(-移动速度, 0, 0);
                GetComponent<AudioSource>().Play();
                is触发 = true;
            }
            if (playerTransform_2P.position.y + 距离 >= gameObject.transform.position.y && !游戏进度控制._instance.当前1P状态)
            {
                Rigidbody2D.velocity = new Vector3(-移动速度, 0, 0);
                GetComponent<AudioSource>().Play();
                is触发 = true;
            }
        }
    }
}
