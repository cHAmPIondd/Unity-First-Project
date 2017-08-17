using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class 游戏按键控制 : MonoBehaviour {

    public static 游戏按键控制 _instance;
    public static bool 手柄1P=false;
    public static bool 手柄2P=false;

    public static KeyCode 方向上_1P;
    public static KeyCode 方向左_1P;
    public static KeyCode 方向右_1P;
    public static KeyCode 方向下_1P;
    public static KeyCode 按键A_1P;
    public static KeyCode 按键B_1P;
    public static KeyCode 按键C_1P;
    public static KeyCode 按键D_1P;

    public static KeyCode 方向上_2P;
    public static KeyCode 方向左_2P;
    public static KeyCode 方向右_2P;
    public static KeyCode 方向下_2P;
    public static KeyCode 按键A_2P;
    public static KeyCode 按键B_2P;
    public static KeyCode 按键C_2P;
    public static KeyCode 按键D_2P;

    public Text[] text;
    public Text[] text1;
    public void 读取按键设置()
    {
        方向上_1P =  (KeyCode)PlayerPrefs.GetInt("方向上_1P");
        text[0].text = ((KeyCode)PlayerPrefs.GetInt("方向上_1P")).ToString();
        text1[0].text = ((KeyCode)PlayerPrefs.GetInt("方向上_1P")).ToString();
        方向左_1P = (KeyCode)PlayerPrefs.GetInt("方向左_1P");
        text[1].text = ((KeyCode)PlayerPrefs.GetInt("方向左_1P")).ToString();
        text1[1].text = ((KeyCode)PlayerPrefs.GetInt("方向左_1P")).ToString();
        方向右_1P = (KeyCode)PlayerPrefs.GetInt("方向右_1P");
        text[2].text = ((KeyCode)PlayerPrefs.GetInt("方向右_1P")).ToString();
        text1[2].text = ((KeyCode)PlayerPrefs.GetInt("方向右_1P")).ToString();
        方向下_1P = (KeyCode)PlayerPrefs.GetInt("方向下_1P");
        text[3].text = ((KeyCode)PlayerPrefs.GetInt("方向下_1P")).ToString();
        text1[3].text = ((KeyCode)PlayerPrefs.GetInt("方向下_1P")).ToString();
        按键A_1P = (KeyCode)PlayerPrefs.GetInt("按键A_1P");
        text[4].text = ((KeyCode)PlayerPrefs.GetInt("按键A_1P")).ToString();
        text1[4].text = ((KeyCode)PlayerPrefs.GetInt("按键A_1P")).ToString();
        按键B_1P = (KeyCode)PlayerPrefs.GetInt("按键B_1P");
        text[5].text = ((KeyCode)PlayerPrefs.GetInt("按键B_1P")).ToString();
        text1[5].text = ((KeyCode)PlayerPrefs.GetInt("按键B_1P")).ToString();
        按键C_1P = (KeyCode)PlayerPrefs.GetInt("按键C_1P");
        text[6].text = ((KeyCode)PlayerPrefs.GetInt("按键C_1P")).ToString();
        text1[6].text = ((KeyCode)PlayerPrefs.GetInt("按键C_1P")).ToString();

        方向上_2P = (KeyCode)PlayerPrefs.GetInt("方向上_2P");
        text[7].text = ((KeyCode)PlayerPrefs.GetInt("方向上_2P")).ToString();
        text1[7].text = ((KeyCode)PlayerPrefs.GetInt("方向上_2P")).ToString();
        方向左_2P = (KeyCode)PlayerPrefs.GetInt("方向左_2P");
        text[8].text = ((KeyCode)PlayerPrefs.GetInt("方向左_2P")).ToString();
        text1[8].text = ((KeyCode)PlayerPrefs.GetInt("方向左_2P")).ToString();
        方向右_2P = (KeyCode)PlayerPrefs.GetInt("方向右_2P");
        text[9].text = ((KeyCode)PlayerPrefs.GetInt("方向右_2P")).ToString();
        text1[9].text = ((KeyCode)PlayerPrefs.GetInt("方向右_2P")).ToString();
        方向下_2P = (KeyCode)PlayerPrefs.GetInt("方向下_2P");
        text[10].text = ((KeyCode)PlayerPrefs.GetInt("方向下_2P")).ToString();
        text1[10].text = ((KeyCode)PlayerPrefs.GetInt("方向下_2P")).ToString();
        按键A_2P = (KeyCode)PlayerPrefs.GetInt("按键A_2P");
        text[11].text = ((KeyCode)PlayerPrefs.GetInt("按键A_2P")).ToString();
        text1[11].text = ((KeyCode)PlayerPrefs.GetInt("按键A_2P")).ToString();
        按键B_2P = (KeyCode)PlayerPrefs.GetInt("按键B_2P");
        text[12].text = ((KeyCode)PlayerPrefs.GetInt("按键B_2P")).ToString();
        text1[12].text = ((KeyCode)PlayerPrefs.GetInt("按键B_2P")).ToString();
        按键C_2P = (KeyCode)PlayerPrefs.GetInt("按键C_2P");
        text[13].text = ((KeyCode)PlayerPrefs.GetInt("按键C_2P")).ToString();
        text1[13].text = ((KeyCode)PlayerPrefs.GetInt("按键C_2P")).ToString();
    }


    public void 初始化设置()
    {
        PlayerPrefs.SetInt("方向上_1P", (int)KeyCode.W);
        PlayerPrefs.SetInt("方向左_1P", (int)KeyCode.A);
        PlayerPrefs.SetInt("方向右_1P", (int)KeyCode.D);
        PlayerPrefs.SetInt("方向下_1P", (int)KeyCode.S);
        PlayerPrefs.SetInt("按键A_1P", (int)KeyCode.J);
        PlayerPrefs.SetInt("按键B_1P", (int)KeyCode.K);
        PlayerPrefs.SetInt("按键C_1P", (int)KeyCode.L);

        PlayerPrefs.SetInt("方向上_2P", (int)KeyCode.UpArrow);
        PlayerPrefs.SetInt("方向左_2P", (int)KeyCode.LeftArrow);
        PlayerPrefs.SetInt("方向右_2P", (int)KeyCode.RightArrow);
        PlayerPrefs.SetInt("方向下_2P", (int)KeyCode.DownArrow);
        PlayerPrefs.SetInt("按键A_2P", (int)KeyCode.Keypad1);
        PlayerPrefs.SetInt("按键B_2P", (int)KeyCode.Keypad2);
        PlayerPrefs.SetInt("按键C_2P", (int)KeyCode.Keypad3);
        读取按键设置();
    }
	void Start () {
        if (PlayerPrefs.GetInt("是否第一次进入游戏") != 19960908)
        {
            PlayerPrefs.SetInt("是否第一次进入游戏", 19960908);
            初始化设置();
            生涯概况记录._instance.初始化记录();
        }
        _instance = this;
        读取按键设置();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
