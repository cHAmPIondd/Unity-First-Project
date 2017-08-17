using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public enum State
    {
        站立,
        着地攻击,
        蹲下攻击,
        跳跃攻击,
        上跳,
        下跳,
        奔跑,
        蹲下,
        虚体,
        死亡,
        技能体_1,
        技能体_2,
        技能体_3,
        踏板_入,
        踏板,
        踏板_出,
        震荡_1,
        震荡_2,
        震荡_3
    }
    public bool is1P;
    public Player anotherPlayer;
    public Rigidbody2D anotherPlayerRigidbody;
    public GameObject[] 血量;
    public GameObject[] 空血;
    public RectTransform 能量遮罩;
    public SpriteRenderer[] SpriteRenderer;
    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;

    private SpriteRenderer 自身SpriteRenderer;

    //状态子集
    public GameObject[] 状态;

    //状态技能子集
    public GameObject[] 技能_1子集;
    public GameObject[] 技能_2子集;
    public GameObject[] 技能_3子集;

    private int groundLayer;//地面层
    private int moveGroundLayer;//移动地面层

    void Start()
    {
        playerTransform=this.gameObject.transform;
        playerRigidbody=GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("floor","第六关floor","边界墙");
        moveGroundLayer = LayerMask.GetMask("moveFloor");
        if(!is1P)    
            isVirtual = true;
        虚体被击中Animator=状态[8].GetComponent<Animator>();
        自身SpriteRenderer = 状态[8].GetComponent<SpriteRenderer>();
    }

    private bool isGround, isWalking, isAttack, isDown, isGravity, isVirtual, isAddSpeed_Right, isAddSpeed_Left, is技能, is踏板, is踏板_入, is踏板_出,is撞墙;
    public bool is震荡, isDeath;
    private State playerState;
    private bool 方向上, 方向左, 方向右, 方向下, 按键A, 按键B, 按键C;
    private float timer=0,addSpeed_RightTimer=0,addSpeed_LeftTimer=0,转换Timer=0,震荡timer=0;
    private int lastState=8;
    private int nowState;

    public AudioSource 落地声;
    public AudioSource 跳跃声;
    public AudioSource 受伤声;
    public AudioSource 出拳声;
    public AudioSource 加血声;
    public AudioSource 错误提示声;
    public GameObject 残影物体;

    private bool is无敌;
    private float 无敌timer;
    private float 闪烁时间间隔timer;

    //游戏参数
    public float moveSpeed = 300;
    public float jumpSpeed=7;
    public float attackTime = 0.2f;
    public float downSpeed = 100;
    public float 无敌时间 = 1;
    public float addSpeed=3.3f;
    public float 旋转速度=1f;
    private Vector2 偏移速度=Vector2.zero;

    private float 切换时间;

    //BOSS参数
    public float Boss左墙X=391.5f;
    public float 震荡飞行速度 = 3f;
    public float 震荡爬起时间 = 1f;


    void 状态判断()
    {
        if (isDeath)
        {
            playerState = State.死亡;
            状态[9].SetActive(true);
            nowState = 9;
        }
        else if(is震荡)
        {
            if (is撞墙||transform.position.x < Boss左墙X
                || Physics2D.Raycast(new Vector2(playerTransform.position.x, playerTransform.position.y+0.5f), Vector2.left, 0.4f, moveGroundLayer)
                || Physics2D.Raycast(new Vector2(playerTransform.position.x, playerTransform.position.y + 0.5f), Vector2.left, 0.4f, groundLayer))
            {
                is撞墙 = true;
                if(isGround)//第三状态
                {
                    playerState = State.震荡_3;
                    状态[18].SetActive(true);
                    nowState = 18;
                }
                else//第二状态
                {
                    playerState = State.震荡_2;
                    状态[17].SetActive(true);
                    nowState = 17;
                }
            }
            else//第一状态
            {
                playerState = State.震荡_1;
                状态[16].SetActive(true);
                nowState = 16;
            }
        }
        else if (is技能)
        {
            if (游戏进度控制._instance.当前关卡 < 3)
            {
                playerState = State.技能体_1;
                状态[10].SetActive(true);
                nowState = 10;
            }
            else if (游戏进度控制._instance.当前关卡 < 5)
            {
                playerState = State.技能体_2;
                状态[11].SetActive(true);
                nowState = 11;
            }
            else
            {
                playerState = State.技能体_3;
                状态[12].SetActive(true);
                nowState = 12;
            }
        }
        else if (is踏板_入)
        {
            playerState = State.踏板_入;
            状态[13].SetActive(true);
            nowState = 13;
            转换Timer += Time.deltaTime;
            if (转换Timer >= 0.208)
            {
                转换Timer = 0;
                状态[13].SetActive(false);
                is踏板 = true;
                is踏板_入 = false;
                playerRigidbody.isKinematic = true;
                状态[14].SetActive(true);
            }
        }
        else if(is踏板)
        {
            状态[14].SetActive(true);
            playerState = State.踏板;
            nowState = 14;
        }
        else if (is踏板_出)
        {
            playerState = State.踏板_出;
            状态[15].SetActive(true);
            nowState = 15;
            转换Timer += Time.deltaTime;
            if (转换Timer >= 0.208)
            {
                转换Timer = 0;
                状态[15].SetActive(false);
                is踏板_出 = false;
                状态[8].SetActive(true);
            }
        }
        else if (isGround && !isWalking && !isAttack && !isDown && !isVirtual)
        {
            playerState = State.站立;
            状态[0].SetActive(true);
            nowState = 0;
        }
        else if (isGround && isAttack && !isDown && !isVirtual)
        {
            playerState = State.着地攻击;
            状态[1].SetActive(true);
            nowState = 1;
        }
        else if (isGround && isAttack && isDown && !isVirtual)
        {
            playerState = State.蹲下攻击;
            状态[2].SetActive(true);
            nowState = 2;
        }
        else if (!isGround && isAttack && !isVirtual)
        {
            playerState = State.跳跃攻击;
            状态[3].SetActive(true);
            nowState = 3;
        }
        else if (!isGround && !isAttack && isGravity && !isVirtual)
        {
            playerState = State.上跳;
            状态[4].SetActive(true);
            nowState = 4;
        }
        else if (!isGround && !isAttack && !isGravity && !isVirtual)
        {
            playerState = State.下跳;
            状态[5].SetActive(true);
            nowState = 5;
        }
        else if (isGround && isWalking && !isAttack && !isDown && !isVirtual)
        {
            playerState = State.奔跑;
            状态[6].SetActive(true);
            nowState = 6;
        }
        else if (isGround && !isAttack && isDown && !isVirtual)
        {
            playerState = State.蹲下;
            状态[7].SetActive(true);
            nowState = 7;
        }
        else if (isVirtual)
        {
            playerState = State.虚体;
            状态[8].SetActive(true);
            nowState = 8;
        }
        else
            print("状态判断错误");
        if (lastState != nowState)
        {
            状态[lastState].SetActive(false);
            //状态切换时做的改变
            if (nowState < 8 && lastState == 8)//变为实体
            {
                切换时间 = Time.time + 1;
                playerRigidbody.gravityScale = 1;
                playerRigidbody.velocity = Vector2.zero;
                按键C = false;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) / transform.localScale.x, 1, 1);
            }
            if (nowState == 8 && lastState<8)//变为虚体
            {
                切换时间 = Time.time + 1;
                if (游戏进度控制._instance.当前1P状态)
                    游戏进度控制._instance.当前1P状态 = false;
                else
                    游戏进度控制._instance.当前1P状态 = true;

                游戏进度控制._instance.is虚实交换 = true;
                if (!游戏进度控制._instance.当前1P状态)
                {
                    游戏进度控制._instance.虚体头像_1P.anchoredPosition3D = new Vector3(1.5f, -65, 0);
                    游戏进度控制._instance.实体头像_2P.anchoredPosition3D = new Vector3(1.5f, -65, 0);
                    游戏进度控制._instance.虚体头像_2P.anchoredPosition3D = new Vector3(1.5f, 0, 0);
                    游戏进度控制._instance.实体头像_1P.anchoredPosition3D = new Vector3(1.5f, 0, 0);
                }
                else
                {
                    游戏进度控制._instance.实体头像_1P.anchoredPosition3D = new Vector3(1.5f, -65, 0);
                    游戏进度控制._instance.虚体头像_2P.anchoredPosition3D = new Vector3(1.5f, -65, 0);
                    游戏进度控制._instance.实体头像_2P.anchoredPosition3D = new Vector3(1.5f, 0, 0);
                    游戏进度控制._instance.虚体头像_1P.anchoredPosition3D = new Vector3(1.5f, 0, 0);
                }

                is无敌 = false;
                无敌timer = 0;
                foreach (SpriteRenderer playerSpriteRenderer in SpriteRenderer)
                {
                    playerSpriteRenderer.color = new Color(1, 1, 1, 1);
                }
                playerRigidbody.gravityScale = 0;
                if (Vector2.SqrMagnitude(playerTransform.position - anotherPlayer.gameObject.transform.position) > 16)
                {
                    游戏进度控制._instance.虚体失控();
                }
            }

            //落地
            if ((lastState == 3 || lastState == 4 || lastState == 5) && nowState != 3 && nowState != 4 && nowState != 5 && nowState != 8)
                if (!落地声.isPlaying)
                    落地声.Play();
            //跳跃
            if (nowState == 4)
                if (!跳跃声.isPlaying)
                    跳跃声.Play();
            //出拳
            if ((nowState == 1 || nowState == 2) && lastState != 3)
                if (!出拳声.isPlaying)
                    出拳声.Play();
            //放技能
            if (nowState >= 10)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        lastState = nowState;
    }

    

    void 物理参数更改()
    {
        //重置偏移
        偏移速度 = Vector2.zero;
        //修改isGround
        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(playerTransform.position.x, playerTransform.position.y - 0.01f), Vector2.down, 0.3f, moveGroundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(playerTransform.position.x + 0.251f, playerTransform.position.y - 0.01f), Vector2.down, 0.3f, moveGroundLayer);
        RaycastHit2D hit3 = Physics2D.Raycast(new Vector2(playerTransform.position.x - 0.251f, playerTransform.position.y - 0.01f), Vector2.down, 0.3f, moveGroundLayer);
        
        if (   Physics2D.Raycast(new Vector2(playerTransform.position.x, playerTransform.position.y - 0.01f), Vector2.down, 0.3f, groundLayer)
            || Physics2D.Raycast(new Vector2(playerTransform.position.x + 0.251f, playerTransform.position.y - 0.01f), Vector2.down, 0.3f, groundLayer)
            || Physics2D.Raycast(new Vector2(playerTransform.position.x - 0.251f, playerTransform.position.y - 0.01f), Vector2.down, 0.3f, groundLayer))
            isGround = true;
        else if ((hit1 || hit2 || hit3))
        {
            isGround = true;
            if (playerRigidbody.velocity.y <= 0)
            {
                if (hit1)
                    偏移速度 += hit1.rigidbody.velocity;
                else if (hit2)
                    偏移速度 += hit2.rigidbody.velocity;
                else if (hit3)
                    偏移速度 += hit3.rigidbody.velocity;
            }
        }
        else
            isGround = false;

        //修改isGravity
        if (playerRigidbody.velocity.y < 0)
            isGravity = false;
        else
            isGravity = true;


        //修改isAddSpeed
        if (!方向左 || 方向右 || 方向下||(isGround&&按键A))
        {
            isAddSpeed_Left = false;
            addSpeed_LeftTimer += Time.deltaTime;
        }
        else if (addSpeed_LeftTimer < 0.1f && addSpeed_LeftTimer > 0 && !方向右 && playerState == State.奔跑)
        {
            isAddSpeed_Left = true;
            残影物体.SetActive(true);
            addSpeed_LeftTimer = 0;
        }
        else
        {
            addSpeed_LeftTimer = 0;
        }
        if (!方向右 || 方向左 || 方向下 || (isGround && 按键A))
        {
            isAddSpeed_Right = false;
            addSpeed_RightTimer += Time.deltaTime;
        }
        else if (addSpeed_RightTimer < 0.1f && addSpeed_RightTimer > 0 && !方向左 && playerState == State.奔跑)
        {
            isAddSpeed_Right = true;
            残影物体.SetActive(true);
            addSpeed_RightTimer = 0;
        }
        else
        {
            addSpeed_RightTimer = 0;
        }

        if (isAddSpeed_Left)
        {
            偏移速度 += new Vector2(-addSpeed, 0);
        }
        else if (isAddSpeed_Right)
        {
            偏移速度 += new Vector2(addSpeed, 0);
        }
        else
        {
            残影物体.SetActive(false);
        }
        //人物移动速度重置
        if (nowState>=8&&nowState<16 && !游戏进度控制._instance.is虚体失控)
        {
            playerRigidbody.velocity = new Vector2(0, 0);
        }
        else
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y) + 偏移速度;
        }
    }
    void 更改输入参数()
    {

        if (is1P)
        {
            if (!游戏按键控制.手柄1P)
            {
                if (Input.GetKeyDown(游戏按键控制.方向上_1P))
                    方向上 = true;
                if (Input.GetKeyUp(游戏按键控制.方向上_1P))
                    方向上 = false;
                if (Input.GetKeyDown(游戏按键控制.方向左_1P))
                    方向左 = true;
                if (Input.GetKeyUp(游戏按键控制.方向左_1P))
                    方向左 = false;
                if (Input.GetKeyDown(游戏按键控制.方向右_1P))
                    方向右 = true;
                if (Input.GetKeyUp(游戏按键控制.方向右_1P))
                    方向右 = false;
                if (Input.GetKeyDown(游戏按键控制.方向下_1P))
                    方向下 = true;
                if (Input.GetKeyUp(游戏按键控制.方向下_1P))
                    方向下 = false;
                if (Input.GetKeyDown(游戏按键控制.按键A_1P))
                    按键A = true;
                if (Input.GetKeyUp(游戏按键控制.按键A_1P))
                    按键A = false;
                if (Input.GetKeyDown(游戏按键控制.按键B_1P))
                    按键B = true;
                if (Input.GetKeyUp(游戏按键控制.按键B_1P))
                    按键B = false;
                if (Input.GetKeyDown(游戏按键控制.按键C_1P))
                    按键C = true;
                if (Input.GetKeyUp(游戏按键控制.按键C_1P))
                    按键C = false;
            }
            else
            {
                if (Input.GetAxis("Vertical1") < 0)
                    方向上 = true;
                if (Input.GetAxis("Vertical1") >= 0)
                    方向上 = false;
                if (Input.GetAxis("Horizontal1") < 0)
                    方向左 = true;
                if (Input.GetAxis("Horizontal1") >= 0)
                    方向左 = false;
                if ( Input.GetAxis("Horizontal1") > 0)
                    方向右 = true;
                if (Input.GetAxis("Horizontal1") <= 0)
                    方向右 = false;
                if (Input.GetAxis("Vertical1") > 0)
                    方向下 = true;
                if (Input.GetAxis("Vertical1") <= 0)
                    方向下 = false;
                if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                    按键A = true;
                if (Input.GetKeyUp(KeyCode.Joystick1Button0))
                    按键A = false;
                if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                    按键B = true;
                if (Input.GetKeyUp(KeyCode.Joystick1Button1))
                    按键B = false;
                if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                    按键C = true;
                if (Input.GetKeyUp(KeyCode.Joystick1Button2))
                    按键C = false;
            }
        }
        else
        {
            if (!游戏按键控制.手柄2P)
            {
                if (Input.GetKeyDown(游戏按键控制.方向上_2P))
                    方向上 = true;
                if (Input.GetKeyUp(游戏按键控制.方向上_2P) )
                    方向上 = false;
                if (Input.GetKeyDown(游戏按键控制.方向左_2P) )
                    方向左 = true;
                if (Input.GetKeyUp(游戏按键控制.方向左_2P))
                    方向左 = false;
                if (Input.GetKeyDown(游戏按键控制.方向右_2P) )
                    方向右 = true;
                if (Input.GetKeyUp(游戏按键控制.方向右_2P))
                    方向右 = false;
                if (Input.GetKeyDown(游戏按键控制.方向下_2P))
                    方向下 = true;
                if (Input.GetKeyUp(游戏按键控制.方向下_2P) )
                    方向下 = false;
                if (Input.GetKeyDown(游戏按键控制.按键A_2P))
                    按键A = true;
                if (Input.GetKeyUp(游戏按键控制.按键A_2P))
                    按键A = false;
                if (Input.GetKeyDown(游戏按键控制.按键B_2P))
                    按键B = true;
                if (Input.GetKeyUp(游戏按键控制.按键B_2P))
                    按键B = false;
                if (Input.GetKeyDown(游戏按键控制.按键C_2P))
                    按键C = true;
                if (Input.GetKeyUp(游戏按键控制.按键C_2P))
                    按键C = false;
            }
            else if (游戏按键控制.手柄1P)
            {
                if (Input.GetAxis("Vertical2") < 0)
                    方向上 = true;
                if (Input.GetAxis("Vertical2") >= 0)
                    方向上 = false;
                if (Input.GetAxis("Horizontal2") < 0)
                    方向左 = true;
                if ( Input.GetAxis("Horizontal2") >= 0)
                    方向左 = false;
                if ( Input.GetAxis("Horizontal2") > 0)
                    方向右 = true;
                if ( Input.GetAxis("Horizontal2") <= 0)
                    方向右 = false;
                if (Input.GetAxis("Vertical2") > 0)
                    方向下 = true;
                if ( Input.GetAxis("Vertical2") <= 0)
                    方向下 = false;
                if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                    按键A = true;
                if (Input.GetKeyUp(KeyCode.Joystick2Button0))
                    按键A = false;
                if (Input.GetKeyDown(KeyCode.Joystick2Button1))
                    按键B = true;
                if (Input.GetKeyUp(KeyCode.Joystick2Button1))
                    按键B = false;
                if (Input.GetKeyDown(KeyCode.Joystick2Button2))
                    按键C = true;
                if (Input.GetKeyUp(KeyCode.Joystick2Button2))
                    按键C = false;
            }
            else
            {
                if (Input.GetAxis("Vertical1") < 0)
                    方向上 = true;
                if (Input.GetAxis("Vertical1") >= 0)
                    方向上 = false;
                if (Input.GetAxis("Horizontal1") < 0)
                    方向左 = true;
                if (Input.GetAxis("Horizontal1") >= 0)
                    方向左 = false;
                if ( Input.GetAxis("Horizontal1") > 0)
                    方向右 = true;
                if (Input.GetAxis("Horizontal1") <= 0)
                    方向右 = false;
                if (Input.GetAxis("Vertical1") > 0)
                    方向下 = true;
                if (Input.GetAxis("Vertical1") <= 0)
                    方向下 = false;
                if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                    按键A = true;
                if (Input.GetKeyUp(KeyCode.Joystick1Button0))
                    按键A = false;
                if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                    按键B = true;
                if (Input.GetKeyUp(KeyCode.Joystick1Button1))
                    按键B = false;
                if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                    按键C = true;
                if (Input.GetKeyUp(KeyCode.Joystick1Button2))
                    按键C = false;
            }
        }
    }
    void 站立()
    {
        if(playerState==State.站立)
        {
            if(方向左&&!方向右)
            {
                playerTransform.localScale = new Vector3(-1, 1, 1);
                isWalking = true;
            }
            if(方向右&&!方向左)
            {
                playerTransform.localScale = new Vector3(1, 1, 1);
                isWalking = true;
            }
            if(方向下)
            {
                isDown = true;
            }
            if(按键A)
            {
                isAttack = true;
            }
            if(按键B)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
                playerRigidbody.velocity += new Vector2(0, jumpSpeed);
                按键B = false;
            }
            if (按键C)
            {
                if (!游戏进度控制._instance.is虚体失控 && 切换时间 < Time.time && anotherPlayer.GetComponent<Player>().nowState == 8)
                {
                    isVirtual = true;
                    anotherPlayer.isVirtual = false;
                    按键C = false;
                }
                else
                    错误提示声.Play();
            }
        }
    }
    void 着地攻击()
    {
        if (playerState == State.着地攻击)
        {
            timer += Time.deltaTime;
            if (方向左)
            {
                playerTransform.localScale = new Vector3(-1, 1, 1);
            }
            if (方向右)
            {
                playerTransform.localScale = new Vector3(1, 1, 1);
            }
            if(方向下)
            {
                isDown = true;
            }
            if (按键C)
            {
                if (!游戏进度控制._instance.is虚体失控 && 切换时间 < Time.time && anotherPlayer.GetComponent<Player>().nowState == 8)
                {
                    isAttack = false;
                    isVirtual = true;
                    anotherPlayer.isVirtual = false;
                    按键C = false;
                }
                else
                    错误提示声.Play();
            }
            if (timer >= attackTime)
            {
                isAttack = false;
                timer = 0;
            }
        }
    }
    void 蹲下攻击()
    {
        if (playerState == State.蹲下攻击)
        {
            timer += Time.deltaTime;
            if (方向左)
            {
                playerTransform.localScale = new Vector3(-1, 1, 1);
            }
            if (方向右)
            {
                playerTransform.localScale = new Vector3(1, 1, 1);
            }
            if(!方向下)
            {
                isDown = false;
            }
            if (按键C)
            {
                if (!游戏进度控制._instance.is虚体失控 && 切换时间 < Time.time && anotherPlayer.GetComponent<Player>().nowState == 8)
                {
                    isAttack = false;
                    isVirtual = true;
                    anotherPlayer.isVirtual = false;
                    按键C = false;
                }
                else
                    错误提示声.Play();
            }
            if (timer >= attackTime)//攻击时间到转换状态
            {
                isAttack = false;
                timer = 0;
            }
        }
    }
    void 跳跃攻击()
    {
        if (playerState == State.跳跃攻击)
        {
            timer += attackTime;
            if (方向左)
            {
                isWalking = true;
                playerTransform.localScale = new Vector3(-1, 1, 1);
                playerRigidbody.velocity += new Vector2(-moveSpeed, 0);
            }

            if (方向右)
            {
                isWalking = true;
                playerTransform.localScale = new Vector3(1, 1, 1);
                playerRigidbody.velocity += new Vector2(moveSpeed, 0);
            }
            if (方向下)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y - downSpeed);
            }
            if (按键C)
            {
                if (!游戏进度控制._instance.is虚体失控 && 切换时间 < Time.time && anotherPlayer.GetComponent<Player>().nowState == 8)
                {
                    isAttack = false;
                    isVirtual = true;
                    anotherPlayer.isVirtual = false;
                    按键C = false;
                }
                else
                    错误提示声.Play();
            }
        }
    }
    void 上跳()
    {
        if (playerState == State.上跳)
        {
            if (方向左)
            {
                isWalking = true;
                playerTransform.localScale = new Vector3(-1, 1, 1);
                playerRigidbody.velocity += new Vector2(-moveSpeed, 0);
            }
            if (方向右)
            {
                isWalking = true;
                playerTransform.localScale = new Vector3(1, 1, 1);
                playerRigidbody.velocity += new Vector2(moveSpeed, 0);
            }
            if (方向下)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y - downSpeed);
            }
            if (按键A)
            {
                isAttack = true;
            }
            if (按键C)
            {
                if (!游戏进度控制._instance.is虚体失控 && 切换时间 < Time.time && anotherPlayer.GetComponent<Player>().nowState==8)
                {
                    isVirtual = true;
                    anotherPlayer.isVirtual = false;
                    按键C = false;
                }
                else
                    错误提示声.Play();
            }
        }
    }
    void 下跳()
    {
        if (playerState == State.下跳)
        {
            if (方向左)
            {
                isWalking = true;
                playerTransform.localScale = new Vector3(-1, 1, 1);
                playerRigidbody.velocity += new Vector2(-moveSpeed, 0);
            }
            if (方向右)
            {
                isWalking = true;
                playerTransform.localScale = new Vector3(1, 1, 1);
                playerRigidbody.velocity += new Vector2(moveSpeed, 0);
            }
            if (方向下)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y - downSpeed);
            }
            if (按键A)
            {
                isAttack = true;
            }
            if (按键C)
            {
                if (!游戏进度控制._instance.is虚体失控 && 切换时间 < Time.time && anotherPlayer.GetComponent<Player>().nowState == 8)
                {
                    isVirtual = true;
                    anotherPlayer.isVirtual = false;
                    按键C = false;
                }
                else
                    错误提示声.Play();
            }
        }
    }
    void 奔跑()
    {
        if (playerState == State.奔跑)
        {
            if (方向左)
            {
                playerTransform.localScale = new Vector3(-1, 1, 1);
                playerRigidbody.velocity += new Vector2(-moveSpeed, 0);
            }
            if (方向右)
            {
                playerTransform.localScale = new Vector3(1, 1, 1);
                playerRigidbody.velocity += new Vector2(moveSpeed, 0);
            }
            if(方向左&&方向右||!方向左&&!方向右)
            {
                isWalking = false;
            }
            if (方向下)
            {
                isDown = true;
            }
            if (按键A)
            {
                isAttack = true;
            }
            if (按键B)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
                playerRigidbody.velocity+=new Vector2(0, jumpSpeed);
                按键B = false;
            }
            if (按键C)
            {
                if (!游戏进度控制._instance.is虚体失控 && 切换时间 < Time.time && anotherPlayer.GetComponent<Player>().nowState == 8)
                {
                    isVirtual = true;
                    anotherPlayer.isVirtual = false;
                    按键C = false;
                }
                else
                    错误提示声.Play();
            }
        }
    }
    void 蹲下()
    {
        if (playerState == State.蹲下)
        {
            if (方向左)
            {
                playerTransform.localScale = new Vector3(-1, 1, 1);
            }
            if (方向右)
            {
                playerTransform.localScale = new Vector3(1, 1, 1);
            }
            if (!方向下)//没蹲下打断攻击状态
            {
                isDown = false;
            }
            if (按键A)
            {
                isAttack = true;
            }
            if (按键C)
            {
                if (!游戏进度控制._instance.is虚体失控 && 切换时间 < Time.time && anotherPlayer.GetComponent<Player>().nowState == 8)
                {
                    isVirtual = true;
                    anotherPlayer.isVirtual = false;
                    按键C = false;
                }
                else
                    错误提示声.Play();
            }
        }
    }
    void 虚体()
    {
        if (playerState == State.虚体&&!游戏进度控制._instance.is虚体失控)
        {
            if (方向上)
            {
                playerRigidbody.velocity += new Vector2(0,(moveSpeed+addSpeed));
            }
            if (方向左)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                playerRigidbody.velocity += new Vector2((-moveSpeed-addSpeed), 0);
            }

            if (方向右)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                playerRigidbody.velocity += new Vector2((moveSpeed+addSpeed), 0);
            }
            if (方向下)
            {
                playerRigidbody.velocity += new Vector2(0,(-moveSpeed-addSpeed));
            }
            if (按键A)
            {
                is踏板_入 = true;
                按键A = false;
            }
            if (按键B && !游戏进度控制._instance.is虚体失控 && (is1P && 游戏进度控制._instance.能量_1P>0 || !is1P && 游戏进度控制._instance.能量_2P>0))
            {
                is技能 = true;
                按键B = false;
            }
            if (按键C)
            {
                if (!游戏进度控制._instance.is虚体失控 && 切换时间 < Time.time)
                {
                    isVirtual = false;
                    anotherPlayer.isVirtual = true;
                    按键C = false;
                }
                else
                    错误提示声.Play();
            }
        }
    }
    void 死亡()
    {
        if (playerState == State.死亡)
        {
            死亡界面.color += new Color(0, 0, 0, 1f / 死亡界面时间 * Time.deltaTime);
        }
    }

    private float 能量消耗单位时间=4f;
    private float 上次能量消耗时间=-10;
    void 技能体()
    {
        
        if (playerState == State.技能体_1 || playerState == State.技能体_2 || playerState == State.技能体_3)
        {
            transform.position = anotherPlayer.transform.position;//与实体同在

            //能量消耗
            if(能量消耗单位时间+上次能量消耗时间<Time.time)
            {
                if (游戏进度控制._instance.当前1P状态)
                {
                    游戏进度控制._instance.能量_2P--;
                    能量遮罩.sizeDelta = new Vector2(游戏进度控制._instance.能量_2P * 635 / 30f+0.01f, 能量遮罩.sizeDelta.y);
                    if(游戏进度控制._instance.能量_2P<=0)
                    {
                        is技能 = false;
                    }
                }
                else
                {
                    游戏进度控制._instance.能量_1P--;
                    能量遮罩.sizeDelta = new Vector2(游戏进度控制._instance.能量_1P * 635 / 30f+0.01f, 能量遮罩.sizeDelta.y);
                    if (游戏进度控制._instance.能量_1P <= 0)
                    {
                        is技能 = false;
                    }
                }
                上次能量消耗时间 = Time.time;
            }
            //操作
            if (playerState == State.技能体_1)
            {
                if (方向左)
                {
                    状态[10].transform.Rotate(0, 0, 旋转速度 * Time.deltaTime);
                    技能_1子集[0].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_1子集[1].transform.rotation = Quaternion.Euler(Vector3.zero);
                }
                if (方向右)
                {
                    状态[10].transform.Rotate(0, 0, -旋转速度 * Time.deltaTime);
                    技能_1子集[0].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_1子集[1].transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
            else if (playerState == State.技能体_2)
            {
                if (方向左)
                {
                    状态[11].transform.Rotate(0, 0, 旋转速度 * Time.deltaTime);
                    技能_2子集[0].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_2子集[1].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_2子集[2].transform.rotation = Quaternion.Euler(Vector3.zero);
                }
                if (方向右)
                {
                    状态[11].transform.Rotate(0, 0, -旋转速度 * Time.deltaTime);
                    技能_2子集[0].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_2子集[1].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_2子集[2].transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
            else if (playerState == State.技能体_3)
            {
                if (方向左)
                {
                    状态[12].transform.Rotate(0, 0, 旋转速度 * Time.deltaTime);
                    技能_3子集[0].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_3子集[1].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_3子集[2].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_3子集[3].transform.rotation = Quaternion.Euler(Vector3.zero);
                }
                if (方向右)
                {
                    状态[12].transform.Rotate(0, 0, -旋转速度 * Time.deltaTime);
                    技能_3子集[0].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_3子集[1].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_3子集[2].transform.rotation = Quaternion.Euler(Vector3.zero);
                    技能_3子集[3].transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
            if (按键B)
            {
                is技能 = false;
                按键B = false;
            }
        }
    }

    void 踏板()
    {
        if (playerState == State.踏板)
        {
            if(按键A)
            {
                is踏板_出 = true;
                is踏板 = false;
                playerRigidbody.isKinematic = false;
                按键A = false;
            }
        }
    }

    void 震荡_1()
    {
        if (playerState == State.震荡_1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            playerRigidbody.velocity = Vector2.left*震荡飞行速度;
        }
    }

    void 震荡_3()
    {
        if (playerState == State.震荡_3)
        {
            震荡timer += Time.deltaTime;
            if(震荡timer>震荡爬起时间)
            {
                震荡timer = 0;
                is震荡 = false;
                is撞墙 = false;
            }
        }
    }

    public Sprite 虚体正常Sprite;
    public Sprite 虚体失控Sprite;
    private Animator 虚体被击中Animator;
    public void BeHit()
    {
        if (!isDeath && !anotherPlayer.GetComponent<Player>().isDeath)
        {
            if (playerState == State.技能体_1 || playerState == State.技能体_2 || playerState == State.技能体_3)
            {

            }
            else if (playerState == State.虚体||playerState==State.踏板)
            {
                if (playerState == State.虚体)
                {
                    虚体被击中Animator.enabled=true;
                    Invoke("关闭动画", 0.25f);
                }
                if (is1P)
                {
                    if (游戏进度控制._instance.能量_1P > 0)
                    {
                        游戏进度控制._instance.能量_1P--;
                        能量遮罩.sizeDelta = new Vector2(游戏进度控制._instance.能量_1P * 635 / 30f + 0.01f, 能量遮罩.sizeDelta.y);
                    }
                    else
                    {
                        受伤声.Play();
                        游戏进度控制._instance.is镜头颤抖 = true;
                        if (is1P)
                        {
                            游戏进度控制._instance.血量_1P--;
                            血量[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_1P - 1].SetActive(false);
                            空血[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_1P - 1].SetActive(true);
                            if (游戏进度控制._instance.血量_1P <= 0)
                                GameOver();
                        }
                        else
                        {
                            游戏进度控制._instance.血量_2P--;
                            血量[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_2P - 1].SetActive(false);
                            空血[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_2P - 1].SetActive(true);
                            if (游戏进度控制._instance.血量_2P <= 0)
                                GameOver();
                        }
                    }
                }
                else 
                {
                    if (游戏进度控制._instance.能量_2P > 0)
                    {
                        游戏进度控制._instance.能量_2P--;
                        能量遮罩.sizeDelta = new Vector2(游戏进度控制._instance.能量_2P * 635 / 30f + 0.01f, 能量遮罩.sizeDelta.y);
                    }
                    else
                    {
                        受伤声.Play();
                        游戏进度控制._instance.is镜头颤抖 = true;
                        if (is1P)
                        {
                            游戏进度控制._instance.血量_1P--;
                            血量[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_1P - 1].SetActive(false);
                            空血[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_1P - 1].SetActive(true);
                            if (游戏进度控制._instance.血量_1P <= 0)
                                GameOver();
                        }
                        else
                        {
                            游戏进度控制._instance.血量_2P--;
                            血量[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_2P - 1].SetActive(false);
                            空血[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_2P - 1].SetActive(true);
                            if (游戏进度控制._instance.血量_2P <= 0)
                                GameOver();
                        }
                    }
                }
            }
            else if (nowState < 8) //实体
            {
                if (!is无敌)
                {
                    is无敌 = true;
                    受伤声.Play();
                    游戏进度控制._instance.is镜头颤抖 = true;
                    if (is1P)
                    {
                        游戏进度控制._instance.血量_1P--;
                        血量[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_1P - 1].SetActive(false);
                        空血[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_1P - 1].SetActive(true);
                        if (游戏进度控制._instance.血量_1P <= 0)
                            GameOver();
                    }
                    else
                    {
                        游戏进度控制._instance.血量_2P--;
                        血量[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_2P - 1].SetActive(false);
                        空血[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_2P - 1].SetActive(true);
                        if (游戏进度控制._instance.血量_2P <= 0)
                            GameOver();
                    }
                }
            }
        }
    }
    void 关闭动画()
    {
        虚体被击中Animator.enabled = false;
        if (游戏进度控制._instance.is虚体失控)
            自身SpriteRenderer.sprite = 虚体失控Sprite;
        else
            自身SpriteRenderer.sprite = 虚体正常Sprite;
    }
    public void BeHeal()
    {
        if (is1P)
        {
            if (游戏进度控制._instance.血量_1P < 游戏进度控制._instance.实体血量Max)
            {
                游戏进度控制._instance.血量_1P++;
                血量[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_1P ].SetActive(true);
                空血[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_1P ].SetActive(false);
            }
        }
        else
        {
            if (游戏进度控制._instance.血量_2P < 游戏进度控制._instance.实体血量Max)
            {
                游戏进度控制._instance.血量_2P++;
                血量[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_2P ].SetActive(true);
                空血[游戏进度控制._instance.实体血量Max - 游戏进度控制._instance.血量_2P ].SetActive(false);
            }
        }
    }
    
    public Transform[] 残影Transform;
    public SpriteRenderer[] 残影SpriteRenderer;
    public Sprite[] 残影Sprite;
    public float 残影间隔=0.1f;
    private float 残影timer=0;
    private int Index = 0;
    void 残影()
    {
        残影timer += Time.deltaTime;
        if (残影timer > 残影间隔)
        {
            残影timer = 0;
            if ((int)playerState < 8)
            {
                残影SpriteRenderer[Index].sprite = 残影Sprite[(int)playerState];
                残影SpriteRenderer[Index].color = new Color(1, 1, 1, 0.5f);
                残影Transform[Index].position = this.gameObject.transform.position;
                残影Transform[Index].localScale = this.gameObject.transform.localScale;
            }
            else
            {
                残影SpriteRenderer[Index].sprite = null;
                残影SpriteRenderer[Index].color = new Color(1, 1, 1, 0.5f);
            }
            Index++;
            Index %= 残影Transform.Length;
        }
        for (int i = 0; i < 残影Transform.Length; i++)
            残影SpriteRenderer[i].color -= new Color(0, 0, 0, (0.5f / (残影Transform.Length * 残影间隔)) * Time.deltaTime);

    }
    void 无敌状态()
    {
        if (is无敌)
        {
            无敌timer += Time.deltaTime;
            闪烁时间间隔timer += Time.deltaTime;
            if (闪烁时间间隔timer > 0.05f)
            {
                闪烁时间间隔timer = 0;
                foreach (SpriteRenderer playerSpriteRenderer in SpriteRenderer)
                {
                    if (playerSpriteRenderer.color.a != 0)
                    {
                        playerSpriteRenderer.color = new Color(1, 1, 1, 0);
                    }
                    else
                    {
                        playerSpriteRenderer.color = new Color(1, 1, 1, 0.7f);
                    }
                }
            }
            if (无敌timer >= 无敌时间)
            {
                foreach (SpriteRenderer playerSpriteRenderer in SpriteRenderer)
                {
                    playerSpriteRenderer.color = new Color(1, 1, 1, 1);
                }
                is无敌 = false;
                无敌timer = 0;
            }
        }
    }

    public GameObject 大光圈;
    public GameObject 小光圈;
    void 光圈()
    {
        if (Vector2.SqrMagnitude(playerTransform.position - anotherPlayer.gameObject.transform.position) < 16)
        {
            if ((is1P && !游戏进度控制._instance.当前1P状态 || !is1P && 游戏进度控制._instance.当前1P状态)&&nowState==8&&!isDeath)
                小光圈.SetActive(true);
            else
                小光圈.SetActive(false);
        }
        else
        {
            if (is1P && !游戏进度控制._instance.当前1P状态 || !is1P && 游戏进度控制._instance.当前1P状态)
                小光圈.SetActive(false);
        }

        if ((游戏进度控制._instance.is虚体失控 && (is1P && 游戏进度控制._instance.当前1P状态 || !is1P && !游戏进度控制._instance.当前1P状态)) && !is技能)
            大光圈.SetActive(true);
        else
            大光圈.SetActive(false);
    }

    public  Image 死亡界面;
    public float 死亡界面时间=4f;
    public GameObject 音乐;
    public void GameOver()//游戏结束
    {
        生涯概况记录._instance.记录关卡死亡次数();
        isDeath = true;
        死亡界面.gameObject.SetActive(true);
        Invoke("Load", 6);

        音乐.SetActive(false);
    }
    void Load()
    {
        Application.LoadLevel("2");
    }
    void Update()
    {
        物理参数更改();
        更改输入参数();
        站立();
        着地攻击();
        蹲下攻击();
        跳跃攻击();
        上跳();
        下跳();
        奔跑();
        蹲下();
        虚体();
        死亡();
        技能体();
        踏板();
        震荡_1();
        震荡_3();
        状态判断();
        
        无敌状态();
        残影();
        光圈();

    }
}
