using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class 生涯概况记录 : MonoBehaviour {

    public static 生涯概况记录 _instance;
    public Text[] 关卡最短时间;
    public Text[] 关卡死亡次数;
    public Text[] 其它记录;
	
    public int[] 关卡开启时间;
    // Use this for initialization
	void Start () {
        _instance = this;
        InvokeRepeating("记录游戏时间", 1, 1);
        关卡开启时间 = new int[10];
	}
    public void 初始化记录()
    {
        PlayerPrefs.SetInt("一最短", 215999);
        PlayerPrefs.SetInt("二最短", 215999);
        PlayerPrefs.SetInt("三最短", 215999);
        PlayerPrefs.SetInt("四最短", 215999);
        PlayerPrefs.SetInt("五最短", 215999);
        PlayerPrefs.SetInt("六最短", 215999);
        PlayerPrefs.SetInt("一死亡", 0);
        PlayerPrefs.SetInt("二死亡", 0);
        PlayerPrefs.SetInt("三死亡", 0);
        PlayerPrefs.SetInt("四死亡", 0);
        PlayerPrefs.SetInt("五死亡", 0);
        PlayerPrefs.SetInt("六死亡", 0);

        PlayerPrefs.SetInt("游戏时间", 0);
        PlayerPrefs.SetInt("最高得分", 0);
        PlayerPrefs.SetInt("通关最短时间", 215999);
        PlayerPrefs.SetInt("通关次数", 0);
    }
    public void 记录关卡最短时间()
    {
        关卡开启时间[游戏进度控制._instance.当前关卡] = (int)Time.time;
        switch (游戏进度控制._instance.当前关卡)
        {
            case 1:
                PlayerPrefs.SetInt("一最短", PlayerPrefs.GetInt("一最短") < (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1]) ?
                    PlayerPrefs.GetInt("一最短") : (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1])); break;
            case 2:
                PlayerPrefs.SetInt("二最短", PlayerPrefs.GetInt("二最短") < (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1]) ?
                    PlayerPrefs.GetInt("二最短") : (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1])); break;
            case 3:
                PlayerPrefs.SetInt("三最短", PlayerPrefs.GetInt("三最短") < (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1]) ?
                     PlayerPrefs.GetInt("三最短") : (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1])); break;
            case 4:
                PlayerPrefs.SetInt("四最短", PlayerPrefs.GetInt("四最短") < (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1]) ?
                    PlayerPrefs.GetInt("四最短") : (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1])); break;
            case 5:
                PlayerPrefs.SetInt("五最短", PlayerPrefs.GetInt("五最短") < (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1]) ?
                    PlayerPrefs.GetInt("五最短") : (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1])); break;
            case 6:
                PlayerPrefs.SetInt("六最短", PlayerPrefs.GetInt("六最短") < (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1]) ?
                    PlayerPrefs.GetInt("六最短") : (int)(Time.time - 关卡开启时间[游戏进度控制._instance.当前关卡 - 1])); break;
            default:
                break;
        }

    }
    public void 记录中途退出最高分()
    {
        PlayerPrefs.SetInt("最高得分", PlayerPrefs.GetInt("最高得分") > 游戏进度控制._instance.分数 ? PlayerPrefs.GetInt("最高得分") : 游戏进度控制._instance.分数);
    }
    public void 记录关卡死亡次数()
    {
        switch (游戏进度控制._instance.当前关卡)
        {
            case 1: PlayerPrefs.SetInt("一死亡", PlayerPrefs.GetInt("一死亡") + 1); break;
            case 2: PlayerPrefs.SetInt("二死亡", PlayerPrefs.GetInt("二死亡") + 1); break;
            case 3: PlayerPrefs.SetInt("三死亡", PlayerPrefs.GetInt("三死亡") + 1); break;
            case 4: PlayerPrefs.SetInt("四死亡", PlayerPrefs.GetInt("四死亡") + 1); break;
            case 5: PlayerPrefs.SetInt("五死亡", PlayerPrefs.GetInt("五死亡") + 1); break;
            case 6: PlayerPrefs.SetInt("六死亡", PlayerPrefs.GetInt("六死亡") + 1); break;
        }
        if (游戏进度控制._instance.当前关卡 != 2)
            PlayerPrefs.SetInt("最高得分",PlayerPrefs.GetInt("最高得分")>游戏进度控制._instance.分数?PlayerPrefs.GetInt("最高得分"):游戏进度控制._instance.分数);
    }
    public void 记录通关()
    {
        PlayerPrefs.SetInt("最高得分", PlayerPrefs.GetInt("最高得分") > 游戏进度控制._instance.分数 ? PlayerPrefs.GetInt("最高得分") : 游戏进度控制._instance.分数);
        PlayerPrefs.SetInt("通关最短时间", PlayerPrefs.GetInt("通关最短时间") < (int)(Time.time - 关卡开启时间[0]) ? PlayerPrefs.GetInt("通关最短时间") : (int)(Time.time - 关卡开启时间[0]));
        PlayerPrefs.SetInt("通关次数", PlayerPrefs.GetInt("通关次数") + 1);
    }
    public void 记录游戏时间()
    {
        int 之前游戏时间=PlayerPrefs.GetInt("游戏时间");
        PlayerPrefs.SetInt("游戏时间", (int)(之前游戏时间 + 1));
        其它记录[0].text = 转化为规范化输出("游戏时间");
    }

    public void 更新记录()
    {
        关卡最短时间[0].text = 转化为规范化输出("一最短");
        关卡最短时间[1].text = 转化为规范化输出("二最短");
        关卡最短时间[2].text = 转化为规范化输出("三最短");
        关卡最短时间[3].text = 转化为规范化输出("四最短");
        关卡最短时间[4].text = 转化为规范化输出("五最短");
        关卡最短时间[5].text = 转化为规范化输出("六最短");


        关卡死亡次数[0].text = PlayerPrefs.GetInt("一死亡").ToString();
        关卡死亡次数[1].text = PlayerPrefs.GetInt("二死亡").ToString();
        关卡死亡次数[2].text = PlayerPrefs.GetInt("三死亡").ToString();
        关卡死亡次数[3].text = PlayerPrefs.GetInt("四死亡").ToString();
        关卡死亡次数[4].text = PlayerPrefs.GetInt("五死亡").ToString();
        关卡死亡次数[5].text = PlayerPrefs.GetInt("六死亡").ToString();

        其它记录[0].text = 转化为规范化输出("游戏时间");
        其它记录[1].text = PlayerPrefs.GetInt("最高得分").ToString();
        其它记录[2].text = 转化为规范化输出("通关最短时间");
        其它记录[3].text = PlayerPrefs.GetInt("通关次数").ToString();
    }
    string 转化为规范化输出(string 名字)
    {
        int 时间=PlayerPrefs.GetInt(名字);
        int 小时 = 时间 / 3600;
        int 分钟 = 时间 / 60 % 60;
        int 秒 = 时间 % 60;
        string S小时,S分钟,S秒;
        if (小时 >= 10)
            S小时 = 小时.ToString();
        else
            S小时 = "0"+小时.ToString();
        if (分钟 >= 10)
            S分钟 = 分钟.ToString();
        else
            S分钟 = "0" + 分钟.ToString();
        if (秒 >= 10)
            S秒 = 秒.ToString();
        else
            S秒 = "0" + 秒.ToString();
        return S小时 + ":" + S分钟 + ":" + S秒;

    }
	// Update is called once per frame
	void Update () {
	    
	}
}
