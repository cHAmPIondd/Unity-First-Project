using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class 花瓣 : MonoBehaviour {

    private GameObject[] Player;
    public static GameObject 当前关卡;
    public GameObject 关卡预设体;
    private Camera Camera;
    private GameObject 左挡板;
    private GameObject 右挡板;

    private Image 能量条_1P;
    private Image 空能量条_1P;
    private Image 能量条_2P;
    private Image 空能量条_2P;

    public Sprite 能量条;
    public Sprite 空能量;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            场景切换管理._instance.关卡进入时刻[游戏进度控制._instance.当前关卡] = Time.time;
            Invoke("启动下一关", 场景切换管理._instance.关卡切换时间);
            Destroy(transform.FindChild("花瓣特效").gameObject);
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<SpriteRenderer>());
            
        }
    }
    public void 启动下一关()
    {
        生涯概况记录._instance.记录关卡最短时间();
        游戏进度控制._instance.当前关卡++;
        能量条_1P.sprite = 能量条_2P.sprite = 能量条;
        空能量条_1P.sprite = 空能量条_2P.sprite = 空能量;
        游戏进度控制._instance.虚体能量Max = 12 + 3 * 游戏进度控制._instance.当前关卡;
        if (游戏进度控制._instance.当前关卡 == 2)
        {
            Camera.orthographicSize = 7.5f;
            左挡板.transform.localPosition = new Vector2(10 - 10 * Camera.orthographicSize / 7.5f, 左挡板.transform.localPosition.y);
            右挡板.transform.localPosition = new Vector2(10 + 10 * Camera.orthographicSize / 7.5f, 右挡板.transform.localPosition.y);
        }
        if (游戏进度控制._instance.当前关卡 == 5)
        {
            Camera.orthographicSize = 7.5f;
            左挡板.transform.localPosition = new Vector2(10 - 10 * Camera.orthographicSize / 7.5f, 左挡板.transform.localPosition.y);
            右挡板.transform.localPosition = new Vector2(10 + 10 * Camera.orthographicSize / 7.5f, 右挡板.transform.localPosition.y);
            Camera.transform.position = new Vector3(10,-7.5f,-30);
        }
        场景切换管理._instance.关卡离开时刻[游戏进度控制._instance.当前关卡] = Time.time;
        Player[0].transform.position = new Vector3(6.06f, -10.47f, 1f);
        Player[1].transform.position = new Vector3(4.8f, -9.07f, 1f);
        Destroy(当前关卡);
        当前关卡 = (GameObject)Instantiate(关卡预设体, Vector3.zero, Quaternion.identity);
    }
    void Awake()
    {
        当前关卡 = GameObject.Find("第一关");
    }
	// Use this for initialization
	void Start () {
        Player=new GameObject[2];
        Player[0] = GameObject.Find("火柴人1P");
        Player[1] = GameObject.Find("火柴人2P");
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        左挡板 = GameObject.Find("左挡板");
        右挡板 = GameObject.Find("右挡板");
        能量条_1P = GameObject.Find("能量条1P").GetComponent <Image>();
        能量条_2P = GameObject.Find("能量条2P").GetComponent<Image>();
        空能量条_1P = GameObject.Find("空能量1P").GetComponent<Image>();
        空能量条_2P = GameObject.Find("空能量2P").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
