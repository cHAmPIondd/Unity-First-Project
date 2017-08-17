using UnityEngine;
using System.Collections;

public class temp : MonoBehaviour {

    private GameObject temp1;
    public void temp2()
    {
        if (游戏进度控制._instance.当前关卡 == 6)
        {
            GameObject.Find("BOSS").GetComponent<Boss>().花瓣出现();
        }
        switch(游戏进度控制._instance.当前关卡)
        {
            case 1: temp1 = GameObject.Find("蓝色花瓣");
                break;
            case 2: temp1 = GameObject.Find("青色花瓣");
                break;
            case 3: temp1 = GameObject.Find("绿色花瓣");
                break;
            case 4: temp1 = GameObject.Find("黄色花瓣");
                break;
            case 5: temp1 = GameObject.Find("橙色花瓣");
                break;
            case 6: temp1 = GameObject.Find("红色花瓣");
                break;
        }

        if (游戏进度控制._instance.当前1P状态)
            temp1.transform.position = GameObject.Find("火柴人1P").transform.position;
        else
            temp1.transform.position = GameObject.Find("火柴人2P").transform.position;
    }
    void temp3()//无敌
    {
        游戏进度控制._instance.实体血量Max = 500;
        游戏进度控制._instance.血量_1P = 500;
        游戏进度控制._instance.血量_2P = 500;
    }
    void temp4()
    {
        游戏进度控制._instance.实体血量Max = 5;
        游戏进度控制._instance.血量_1P = 5;
        游戏进度控制._instance.血量_2P = 5;
    }
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F12))
            temp2();
        if (Input.GetKeyDown(KeyCode.F11))
            temp3();
        if (Input.GetKeyDown(KeyCode.F10))
            temp4();
	}
}
