using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class 开场动画 : MonoBehaviour {

    public Image[] 画面;
    public float[] 间隔时间;
    public float 渐变时间;
    private float timer=0;
    private int Index=0;
	// Use this for initialization
	void Start () {
	}
    private bool is启动;
	// Update is called once per frame
    void Update()
    {
        if (!is启动&&(Index > 画面.Length - 1 || Input.GetKeyDown(KeyCode.Escape)))//动画结束
        {
            is启动 = true;
            场景切换管理._instance.关卡进入时刻[游戏进度控制._instance.当前关卡] = Time.time;
            Invoke("启动第一关", 场景切换管理._instance.关卡切换时间);
        }
        if(!is启动)
        {
            timer += Time.deltaTime;
            画面[Index].color += new Color(0, 0, 0, 1 / 渐变时间 * Time.deltaTime);
            if (timer > 渐变时间 + 间隔时间[Index])
            {
                timer = 0;
                Index++;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (画面[Index].color.a >= 1)
                {
                    timer = 0;
                    Index++;
                }
                else
                {
                    画面[Index].color = new Color(1, 1, 1, 1);
                    timer = 2;
                }

            }
        }
    }
    public GameObject[] 打开物件;
    public GameObject[] 关闭物件;
    void 启动第一关()
    {
        生涯概况记录._instance.记录关卡最短时间();
        游戏进度控制._instance.当前关卡++;
        场景切换管理._instance.关卡离开时刻[游戏进度控制._instance.当前关卡] = Time.time;
        foreach (GameObject 物件 in 打开物件)
        {
            物件.SetActive(true);
        }
        foreach (GameObject 物件 in 关闭物件)
        {
            物件.SetActive(false);
        }     
    }
}
