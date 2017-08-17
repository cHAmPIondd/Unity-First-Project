using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class 场景切换管理 : MonoBehaviour
{
    public AudioSource[] 关卡音乐;
    public Image 黑幕;
    public float[] 关卡离开时刻;
    public float[] 关卡进入时刻;
    public float 关卡切换时间 = 5f;
    public static 场景切换管理 _instance;
    // Use this for initialization
    void Start()
    {
        _instance = this;
        关卡离开时刻 = new float[10];
        关卡进入时刻 = new float[10];
        for (int i = 0; i < 10; i++)
        {
            关卡离开时刻[i] = -10;
            关卡进入时刻[i] = -10;
        }
        黑幕.color = new Color(1, 1, 1, 0);
        黑幕.gameObject.SetActive(false);
    }

    public bool is正在切换场景()
    {
        if (关卡进入时刻[游戏进度控制._instance.当前关卡] + 关卡切换时间 > Time.time
            || 关卡离开时刻[游戏进度控制._instance.当前关卡] + 关卡切换时间 > Time.time)
            return true;
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if (关卡进入时刻[游戏进度控制._instance.当前关卡] + 关卡切换时间 > Time.time)
        {
            黑幕.gameObject.SetActive(true);
            关卡音乐[游戏进度控制._instance.当前关卡].volume -= (0.3f / 关卡切换时间 * Time.deltaTime);
            黑幕.color += new Color(0, 0, 0, 1 / 关卡切换时间 * Time.deltaTime);
        }
        if (关卡离开时刻[游戏进度控制._instance.当前关卡] + 关卡切换时间 > Time.time)
        {
            if (!关卡音乐[游戏进度控制._instance.当前关卡].isPlaying)
                关卡音乐[游戏进度控制._instance.当前关卡].Play();
            关卡音乐[游戏进度控制._instance.当前关卡].volume += (0.3f / 关卡切换时间 * Time.deltaTime );
            黑幕.color -= new Color(0, 0, 0, 1 / 关卡切换时间 * Time.deltaTime );
        }
        if (关卡进入时刻[9] + 关卡切换时间 > Time.time)
        {
            黑幕.gameObject.SetActive(true);
            关卡音乐[9].volume -= (0.3f / 关卡切换时间 * Time.deltaTime);
            黑幕.color += new Color(0, 0, 0, 1 / 关卡切换时间 * Time.deltaTime);
        }
    }
}
