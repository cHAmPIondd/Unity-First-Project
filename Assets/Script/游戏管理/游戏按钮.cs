using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class 游戏按钮 : MonoBehaviour
{

    public GameObject[] 打开物件;
    public GameObject[] 关闭物件;
    public SpriteRenderer 火柴人SpriteRenderer;
    private Image 文字SpriteRenderer;
    public Sprite 文字Sprite_1;

    public 开始界面动画 火柴点燃;

 
    void Start()
    {
        文字SpriteRenderer = GetComponent<Image>();
    }
    //开始界面

    public void 开始游戏()
    {
        场景切换管理._instance.关卡进入时刻[9] = Time.time;//主界面音乐渐出
        Invoke("启动开场动画", 场景切换管理._instance.关卡切换时间 );
    }
    void 启动开场动画()
    {
        foreach (GameObject 物件 in 打开物件)
        {
            物件.SetActive(true);
        }
        foreach (GameObject 物件 in 关闭物件)
        {
            物件.SetActive(false);
        }
        //开场动画音乐
        场景切换管理._instance.关卡离开时刻[游戏进度控制._instance.当前关卡] = Time.time;
        鼠标操控._instance.隐藏鼠标();
    }

    public void 无尽模式()
    {
        生涯概况记录._instance.更新记录();
        foreach (GameObject 物件 in 打开物件)
        {
            物件.SetActive(true);
        }
        foreach (GameObject 物件 in 关闭物件)
        {
            物件.SetActive(false);
        }
        火柴人SpriteRenderer.color = new Color(1, 1, 1, 0);
        文字SpriteRenderer.sprite = 文字Sprite_1;
    }
    public void 初始化数据()
    {
        foreach (GameObject 物件 in 打开物件)
        {
            物件.SetActive(true);
        }   
    }
    public void 确定初始化数据()
    {
        生涯概况记录._instance.初始化记录();
        生涯概况记录._instance.更新记录();
        foreach (GameObject 物件 in 关闭物件)
        {
            物件.SetActive(false);
        }
    }
    public void 取消()
    {
        foreach (GameObject 物件 in 关闭物件)
        {
            物件.SetActive(false);
        }
    }
    public void 游戏设置()
    {
        foreach (GameObject 物件 in 打开物件)
        {
            物件.SetActive(true);
        }
        foreach (GameObject 物件 in 关闭物件)
        {
            物件.SetActive(false);
        }

        火柴人SpriteRenderer.color = new Color(1, 1, 1, 0);
        文字SpriteRenderer.sprite = 文字Sprite_1;
    }

    public void 退出游戏()
    {
        Application.Quit();
    }
    //其它按钮


    public void 继续游戏()
    {
        foreach (GameObject 物件 in 打开物件)
        {
            物件.SetActive(true);
        }
        foreach (GameObject 物件 in 关闭物件)
        {
            物件.SetActive(false);
        }
        Time.timeScale = 1;
        游戏进度控制._instance.is暂停 = false;
        鼠标操控._instance.隐藏鼠标();
        场景切换管理._instance.黑幕.gameObject.SetActive(true);
    }
    
    public void 游戏中设置()
    {
        foreach (GameObject 物件 in 打开物件)
        {
            物件.SetActive(true);
        }
        foreach (GameObject 物件 in 关闭物件)
        {
            物件.SetActive(false);
        }
    }
    public void 回到主界面()
    {
        Time.timeScale = 1;
        生涯概况记录._instance.记录中途退出最高分();
        Application.LoadLevel("2");
    }
    //设置按键

    public void 完成按键设置()
    {
        foreach (GameObject 物件 in 打开物件)
        {
            物件.SetActive(true);
        }
        foreach (GameObject 物件 in 关闭物件)
        {
            物件.SetActive(false);
        }
    }

    public Text Text;
    private string nowText;
    private KeyCode KeyCode;

    //1P按键修改
    private bool is输入上_1P = false;
    private bool is输入左_1P = false;
    private bool is输入右_1P = false;
    private bool is输入下_1P = false;
    private bool is输入A_1P = false;
    private bool is输入B_1P = false;
    private bool is输入C_1P = false;

    public void 上_1P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入上_1P = true;
    }
    public void 左_1P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入左_1P = true;
    }
    public void 右_1P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入右_1P = true;
    }
    public void 下_1P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入下_1P = true;

    }
    public void A_1P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入A_1P = true;
    }
    public void B_1P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入B_1P = true;
    }
    public void C_1P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入C_1P = true;
    }

    //2P按键修改

    private bool is输入上_2P = false;
    private bool is输入左_2P = false;
    private bool is输入右_2P = false;
    private bool is输入下_2P = false;
    private bool is输入A_2P = false;
    private bool is输入B_2P = false;
    private bool is输入C_2P = false;
    public void 上_2P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入上_2P = true;
    }
    public void 左_2P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入左_2P = true;
    }
    public void 右_2P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入右_2P = true;
    }
    public void 下_2P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入下_2P = true;
    }
    public void A_2P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入A_2P = true;
    }
    public void B_2P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入B_2P = true;
    }
    public void C_2P()
    {
        nowText = Text.text;
        Text.text = "";
        is输入C_2P = true;
    }
    public void 初始化设置()
    {
        游戏按键控制._instance.初始化设置();
    }
    void Update()
    {
        //1P按键修改
        if (is输入上_1P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape&&KeyCode !=KeyCode.Mouse0)
                {
                    游戏按键控制.方向上_1P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("方向上_1P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入上_1P = false;
            }
        }
        if (is输入左_1P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.方向左_1P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("方向左_1P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入左_1P = false;
            }
        }
        if (is输入右_1P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.方向右_1P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("方向右_1P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入右_1P = false;
            }
        }
        if (is输入下_1P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.方向下_1P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("方向下_1P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入下_1P = false;
            }
        }
        if (is输入A_1P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.按键A_1P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("按键A_1P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入A_1P = false;
            }
        }
        if (is输入B_1P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.按键B_1P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("按键B_1P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入B_1P = false;
            }
        }
        if (is输入C_1P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.按键C_1P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("按键C_1P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入C_1P = false;
            }
        }

        //2P按键修改

        if (is输入上_2P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.方向上_2P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("方向上_2P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入上_2P = false;
            }
        }
        if (is输入左_2P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.方向左_2P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("方向左_2P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入左_2P = false;
            }
        }
        if (is输入右_2P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.方向右_2P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("方向右_2P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入右_2P = false;
            }
        }
        if (is输入下_2P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.方向下_2P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("方向下_2P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入下_2P = false;
            }
        }
        if (is输入A_2P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.按键A_2P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("按键A_2P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入A_2P = false;
            }
        }
        if (is输入B_2P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.按键B_2P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("按键B_2P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入B_2P = false;
            }
        }
        if (is输入C_2P)
        {
            if ((KeyCode = getKeyDownCode()) != KeyCode.None)
            {
                if (KeyCode != KeyCode.Escape && KeyCode != KeyCode.Mouse0)
                {
                    游戏按键控制.按键C_2P = KeyCode;
                    Text.text = KeyCode.ToString();
                    PlayerPrefs.SetInt("按键C_2P", (int)KeyCode);
                }
                else
                    Text.text = nowText;
                is输入C_2P = false;
            }
        }
    }

    public static KeyCode getKeyDownCode()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in KeyCode.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return keyCode;
                }
            }
        }
        return KeyCode.None;
    }
}
