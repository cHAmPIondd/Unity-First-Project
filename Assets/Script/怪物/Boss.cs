using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
    private int hp = 3;
	
    public GameObject[] 状态;
    private bool is无敌=true;

    public bool isBoss触发;
    private AudioSource 受伤音效;
    void BeHit()
    {
        if (!is无敌)
        {
            恢复无敌();
            受伤音效.Play();
            状态[3 - hp].SetActive(false);
            hp--;
            状态[3 - hp].SetActive(true);
            if (hp <= 0)
            {

                Destroy(震动波);
                Invoke("花瓣出现", 3f);
                CancelInvoke("天降陨石");
                CancelInvoke("天降踏板");
            }
        }
    }
    public GameObject 花瓣;
    public void 花瓣出现()
    {
        花瓣.SetActive(true);
    }
    // Use this for initialization

	void Start () {
        player_1P = GameObject.Find("火柴人1P").GetComponent<Player>();
        player_2P = GameObject.Find("火柴人2P").GetComponent<Player>();
        受伤音效 = GetComponent<AudioSource>();
	}
    public float 震荡波间隔时间=30f;
    public float 空隙时间=10f;
    private float 上次震荡波时间=0;
    public GameObject 震动波;
    public float 扩大时间;
    public float 扩大速度;

    private  Player player_1P;
    private  Player player_2P;
	// Update is called once per frame
	void Update () {
        if (isBoss触发)
        {
            if (游戏进度控制._instance.is虚体失控)
                恢复无敌();
            if (上次震荡波时间 + 震荡波间隔时间 < Time.time)
            {
                震动波.transform.localScale += Vector3.one * 扩大速度 * Time.deltaTime;
                if (游戏进度控制._instance.当前1P状态)
                {
                    player_1P.is震荡 = true;
                }
                else
                {
                    player_2P.is震荡 = true;
                }
                if (上次震荡波时间 + 震荡波间隔时间 + 扩大时间 < Time.time)//震荡
                {
                    上次震荡波时间 = Time.time;
                    is无敌 = false;
                    Invoke("恢复无敌", 空隙时间);
                }
            }
            if (!is陨石)
            {
                InvokeRepeating("天降陨石", 2, 5);
                is陨石 = true;
            }
            if(!is怪物踏板)
            {
                InvokeRepeating("天降踏板", 0, 掉落间隔时间);
                is怪物踏板 = true;
            }
        }
        else
            上次震荡波时间 = Time.time;
        音乐渐出();
	}
    public AudioSource boss音乐;
    void 音乐渐出()
    {
        if (hp <= 0)
        {
            boss音乐.volume -= (0.3f / 3 * Time.deltaTime);
        }
    }
    void 恢复无敌()
    {
        is无敌 = true;
        震动波.transform.localScale =new Vector3(0.4f,0.4f,0.4f);
    }

    private bool is陨石;
    public GameObject 陨石;
    public float 陨石最小x;
    public float 陨石最大x;
    void 天降陨石()
    {
        Instantiate(陨石, new Vector3(Random.Range(陨石最小x, 陨石最大x), 2, 0),Quaternion.Euler(new Vector3(0,0,Random.Range(25f ,35f))));
    }

    private bool is怪物踏板;
    public float 掉落间隔时间;
    public float 掉落最大X;
    public float 掉落最小X;
    public GameObject 加分踏板;
    public GameObject 加血踏板;
    public GameObject 踏板;

    private float 随机数;
    void 天降踏板()
    {
        随机数=Random.Range(0,1f);
        if(随机数>0.95f)
            Instantiate(加血踏板, new Vector3(Random.Range(掉落最小X, 掉落最大X), 0.5f, 0), Quaternion.identity);
        else if (随机数>0.7)
            Instantiate(加分踏板, new Vector3(Random.Range(掉落最小X, 掉落最大X), 0.5f, 0), Quaternion.identity);
        else
            Instantiate(踏板, new Vector3(Random.Range(掉落最小X, 掉落最大X), 0.5f, 0), Quaternion.identity);
    }
    
}
