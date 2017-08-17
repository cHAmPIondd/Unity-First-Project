using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using UnityEngine.UI;
public class 游戏进度控制 : MonoBehaviour {

    public static 游戏进度控制 _instance;
    public int 分数=0;
    public int 剩余命数;
    public int 当前关卡;

    public Text 分数显示;
    
    public int 实体血量Max=5;
    public int 虚体能量Max;
    public int 血量_1P=5;
    public int 能量_1P;
    public int 血量_2P = 5;
    public int 能量_2P;
    public bool is虚体失控 = false;
    public bool 当前1P状态 = true;//true表示实体，false表示虚体

    
    
    public Rigidbody2D playerRigidbody_1P;
    public Rigidbody2D playerRigidbody_2P;
    public SpriteRenderer playerSpriteRenderer_1P;
    public SpriteRenderer playerSpriteRenderer_2P;
    public Player playerPlayer_1P;
    public Player playerPlayer_2P;
    public GameObject 虚体失控_1P;
    public GameObject 虚体失控_2P;
    public Sprite 虚体失控Sprite;
    public Sprite 虚体正常Sprite;

    public void 虚体失控()
    {
        is虚体失控 = true;
        if (当前1P状态)
        {
            虚体正常Sprite = playerSpriteRenderer_2P.sprite;
            playerSpriteRenderer_2P.sprite = 虚体失控Sprite;
            playerRigidbody_2P.gravityScale =   1f;
            虚体失控_2P.SetActive(true);
        }
        else
        {
            虚体正常Sprite = playerSpriteRenderer_1P.sprite;
            playerSpriteRenderer_1P.sprite = 虚体失控Sprite;
            playerRigidbody_1P.gravityScale = 1f;
            虚体失控_1P.SetActive(true);
        }
        is镜头颤抖 = true;
    }

    public Transform playerTramsform_1P;
    public Transform playerTramsform_2P;
    private Transform cameraTramsform;


    public bool is镜头颤抖=false;
    public float 镜头颤抖时间 = 0.1f;
    public float 镜头颤抖幅度 = 0.2f;
    private float 镜头颤抖timer = 0;
    GamePadState state1P;
    GamePadState state2P;
    public void 镜头颤抖()//横版
    {
        镜头颤抖timer += Time.deltaTime;
        if (游戏按键控制.手柄1P)
            GamePad.SetVibration(PlayerIndex.One, 10, 10);
        if (游戏按键控制.手柄2P)
            GamePad.SetVibration(PlayerIndex.Two, 10, 10);
        if (镜头颤抖timer > 镜头颤抖时间)
        {
            is镜头颤抖 = false;
            镜头颤抖timer = 0;
        }
        cameraTramsform.position = new Vector3(cameraTramsform.position.x + Random.Range(-镜头颤抖幅度, 镜头颤抖幅度), cameraTramsform.position.y + Random.Range(-镜头颤抖幅度, 镜头颤抖幅度), cameraTramsform.position.z);
    }


    //状态栏控制
    public RectTransform 实体头像_1P;
    public RectTransform 虚体头像_1P;
    public RectTransform 实体头像_2P;
    public RectTransform 虚体头像_2P;
    public bool is虚实交换 = false;
    public float 交换动画Speed=1;
    void 虚实交换动画()
    {
        实体头像_1P.anchoredPosition3D += new Vector3(0, 1, 0) * Time.deltaTime * 交换动画Speed;
        虚体头像_1P.anchoredPosition3D += new Vector3(0, 1, 0) * Time.deltaTime * 交换动画Speed;
        实体头像_2P.anchoredPosition3D += new Vector3(0, 1, 0) * Time.deltaTime * 交换动画Speed;
        虚体头像_2P.anchoredPosition3D += new Vector3(0, 1, 0) * Time.deltaTime * 交换动画Speed;
        if(当前1P状态)
        {
            if (实体头像_1P.anchoredPosition3D.y >= -1)
            {
                is虚实交换 = false;
            }
        }
        else
        {
            if (虚体头像_1P.anchoredPosition3D.y >= -1)
            {
                is虚实交换 = false;
            }
        }
    }





    //暂停游戏
    public GameObject 暂停界面;
    public bool is暂停 = false;
    void 暂停游戏()
    {
        is暂停 = true;
        暂停界面.SetActive(true);
        Time.timeScale = 0;
        场景切换管理._instance.黑幕.gameObject.SetActive(false);
    }

    void Awake()
    {
        Screen.fullScreen = true;
    }
	void Start () {
        _instance = this;
        cameraTramsform = GetComponent<Transform>();
        state1P = GamePad.GetState(PlayerIndex.One);
        state2P = GamePad.GetState(PlayerIndex.Two);
        濒临状态Image = 濒临状态.GetComponent<Image>();
	}

    public GameObject 濒临状态;
    private bool is濒临;
    private Image 濒临状态Image;
    private float 濒临闪烁时间=0.5f;
    void 濒临状态函数()
    {
        濒临状态.SetActive(false);
        is濒临 = false;
        if (!is暂停)
        {
            if (当前1P状态)
            {
                if (血量_1P == 1)
                {
                    濒临状态.SetActive(true);
                    is濒临 = true;
                }
                else if (血量_2P == 1 && 能量_2P == 0)
                {
                    濒临状态.SetActive(true);
                    is濒临 = true;
                }
            }
            else
            {
                if (血量_2P == 1)
                {
                    濒临状态.SetActive(true);
                    is濒临 = true;
                }
                else if (血量_1P == 1 && 能量_1P == 0)
                {
                    濒临状态.SetActive(true);
                    is濒临 = true;
                }
            }
            if (is濒临)
            {
                濒临状态Image.color += new Color(0, 0, 0, 1f / 濒临闪烁时间 * Time.deltaTime);
                if (濒临状态Image.color.a <= 0&&濒临闪烁时间<0)
                    濒临闪烁时间 = -濒临闪烁时间;
                else if(濒临状态Image.color.a >= 1&&濒临闪烁时间>0)
                    濒临闪烁时间 = -濒临闪烁时间;
            }
        }
    }
	void Update () {
        //分数更改
        分数显示.text = 分数.ToString();
        
        if (is镜头颤抖&&Time.timeScale!=0)
        {
            镜头颤抖();
        }
        else
        {
            //停止手柄震动
            GamePad.SetVibration(PlayerIndex.One, 0, 0);
            GamePad.SetVibration(PlayerIndex.Two, 0, 0);
        }
        //失控恢复
        if (is虚体失控)
        {
            if (Vector2.SqrMagnitude(playerTramsform_1P.position - playerTramsform_2P.position) < 16)
            {
                is虚体失控 = false;
                if (当前1P状态)
                {
                    playerSpriteRenderer_2P.sprite = 虚体正常Sprite;
                    playerRigidbody_2P.gravityScale =0;
                    虚体失控_2P.SetActive(false);
                }
                else
                {
                    playerSpriteRenderer_1P.sprite = 虚体正常Sprite;
                    playerRigidbody_1P.gravityScale = 0;
                    虚体失控_1P.SetActive(false);
                }
            }
        }
        //交换动画
        if(is虚实交换)
        {
            虚实交换动画();
        }

        //暂停游戏
        if (当前关卡 > 0 && 当前关卡 < 7 && is暂停 == false && Input.GetKeyDown(KeyCode.Escape) && !playerPlayer_1P.isDeath && !playerPlayer_2P.isDeath)
        {
            暂停游戏();
            鼠标操控._instance.显示鼠标();
        }
        //濒临状态
        濒临状态函数();
	}
    public GameObject 结局的动画;
    void 结局动画()
    {
        结局的动画.SetActive(true);
    }
    
}
