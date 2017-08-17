using UnityEngine;
using System.Collections;

public class 普通子弹发射 : MonoBehaviour
{

    public GameObject 子弹;
    public float 发射时间间隔 = 2;
    private bool 可发射子弹 = true;
    private float 发射子弹timer;
    private Vector3 子弹角度;
    public float 小喽罗攻击范围 = 30;
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
            if (游戏进度控制._instance.当前1P状态)
            {
                if (Mathf.Pow(player_1P.transform.position.x - 自身Transform.position.x, 2) +
                    Mathf.Pow(player_1P.transform.position.y - 自身Transform.position.y, 2) < 小喽罗攻击范围)
                {
                    if (player_1P.transform.position.x - 自身Transform.position.x > 0)
                    {
                        子弹角度 = new Vector3(0, 0, 180 + Mathf.Atan(((player_1P.transform.position.y - 自身Transform.position.y) /
                            (player_1P.transform.position.x - 自身Transform.position.x))) / Mathf.Deg2Rad);
                    }
                    else
                    {
                        子弹角度 = new Vector3(0, 0, Mathf.Atan(((player_1P.transform.position.y - 自身Transform.position.y) /
                            (player_1P.transform.position.x - 自身Transform.position.x))) / Mathf.Deg2Rad);
                    }
                    Instantiate(子弹, gameObject.transform.position,
                        Quaternion.Euler(子弹角度));
                    可发射子弹 = false;
                }   // 实体在攻击范围内
                else if (Mathf.Pow(player_2P.transform.position.x - 自身Transform.position.x, 2) +
                    Mathf.Pow(player_2P.transform.position.y - 自身Transform.position.y, 2) < 小喽罗攻击范围)
                {
                    if (player_2P.transform.position.x - 自身Transform.position.x > 0)
                    {
                        子弹角度 = new Vector3(0, 0, 180 + Mathf.Atan(((player_2P.transform.position.y - 自身Transform.position.y) /
                            (player_2P.transform.position.x - 自身Transform.position.x))) / Mathf.Deg2Rad);
                    }
                    else
                    {
                        子弹角度 = new Vector3(0, 0, Mathf.Atan(((player_2P.transform.position.y - 自身Transform.position.y) /
                            (player_2P.transform.position.x - 自身Transform.position.x))) / Mathf.Deg2Rad);
                    }
                    Instantiate(子弹, gameObject.transform.position,
                        Quaternion.Euler(子弹角度));
                    可发射子弹 = false;
                }// 实体不在攻击范围内，虚体在
            }//当前1P为实体
            else
            {
                if (Mathf.Pow(player_2P.transform.position.x - 自身Transform.position.x, 2) +
                    Mathf.Pow(player_2P.transform.position.y - 自身Transform.position.y, 2) < 小喽罗攻击范围)
                {
                    if (player_2P.transform.position.x - 自身Transform.position.x > 0)
                    {
                        子弹角度 = new Vector3(0, 0, 180 + Mathf.Atan(((player_2P.transform.position.y - 自身Transform.position.y) /
                            (player_2P.transform.position.x - 自身Transform.position.x))) / Mathf.Deg2Rad);
                    }
                    else
                    {
                        子弹角度 = new Vector3(0, 0, Mathf.Atan(((player_2P.transform.position.y - 自身Transform.position.y) /
                            (player_2P.transform.position.x - 自身Transform.position.x))) / Mathf.Deg2Rad);
                    }
                    Instantiate(子弹, gameObject.transform.position,
                        Quaternion.Euler(子弹角度));
                    可发射子弹 = false;
                }   // 实体在攻击范围内
                else if (Mathf.Pow(player_1P.transform.position.x - 自身Transform.position.x, 2) +
                    Mathf.Pow(player_1P.transform.position.y - 自身Transform.position.y, 2) < 小喽罗攻击范围)
                {
                    if (player_1P.transform.position.x - 自身Transform.position.x > 0)
                    {
                        子弹角度 = new Vector3(0, 0, 180 + Mathf.Atan(((player_1P.transform.position.y - 自身Transform.position.y) /
                            (player_1P.transform.position.x - 自身Transform.position.x))) / Mathf.Deg2Rad);
                    }
                    else
                    {
                        子弹角度 = new Vector3(0, 0, Mathf.Atan(((player_1P.transform.position.y - 自身Transform.position.y) /
                            (player_1P.transform.position.x - 自身Transform.position.x))) / Mathf.Deg2Rad);
                    }
                    Instantiate(子弹, gameObject.transform.position,
                        Quaternion.Euler(子弹角度));
                    可发射子弹 = false;
                }//实体不在攻击范围,虚体在
            }//当前1P为虚体
        }

    }
}
