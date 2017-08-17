using UnityEngine;
using System.Collections;

public class 红色花瓣 : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            场景切换管理._instance.关卡进入时刻[游戏进度控制._instance.当前关卡] = Time.time;
            Invoke("结尾动画", 场景切换管理._instance.关卡切换时间);
            Destroy(transform.FindChild("花瓣特效").gameObject);
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<SpriteRenderer>());

        }
    }
    private GameObject player_1P;
    private GameObject player_2P;
    void Start()
    {
        player_1P = GameObject.Find("火柴人1P");
        player_2P = GameObject.Find("火柴人2P");
    }
    void 结尾动画()
    {
        生涯概况记录._instance.记录通关();
        生涯概况记录._instance.记录关卡最短时间();
        游戏进度控制._instance.当前关卡++;
        场景切换管理._instance.关卡离开时刻[游戏进度控制._instance.当前关卡] = Time.time;
        player_1P.SetActive(false);
        player_2P.SetActive(false);
        游戏进度控制._instance.SendMessage("结局动画");
        Destroy(花瓣.当前关卡);
    }
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
	
	}
}
