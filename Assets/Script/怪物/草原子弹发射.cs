using UnityEngine;
using System.Collections;

public class 草原子弹发射 : MonoBehaviour {

    public GameObject 子弹;
    public float 发射时间间隔 = 2;
    private bool 可发射子弹 = true;
    private float 发射子弹timer;
    public float 小喽罗攻击范围 = 30;
    public float 攻击高低范围=1;
    // Use this for initialization
    void Start()
    {
        player_1P = GameObject.Find("火柴人1P");
        player_2P = GameObject.Find("火柴人2P");
    }
    public Transform 自身Transform;
    private GameObject player_1P;
    private GameObject player_2P;
    void Update()
    {
        if (!可发射子弹)
        {
            发射子弹timer += Time.deltaTime;
            if (发射子弹timer >= 发射时间间隔)
            {
                发射子弹timer = 0;
                可发射子弹 = true;
            }
        }
        if (可发射子弹)
        {
            if ((Mathf.Abs(player_1P.transform.position.x - 自身Transform.position.x) < 小喽罗攻击范围
                    && Mathf.Abs(player_1P.transform.position.y - 自身Transform.position.y) < 攻击高低范围)
            || (Mathf.Abs(player_2P.transform.position.x - 自身Transform.position.x) < 小喽罗攻击范围
                    && Mathf.Abs(player_2P.transform.position.y  - 自身Transform.position.y) < 攻击高低范围))
            {
                Instantiate(子弹, gameObject.transform.position, Quaternion.identity);
                可发射子弹 = false;
            }
        }

    }
}
