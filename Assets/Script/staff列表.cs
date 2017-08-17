using UnityEngine;
using System.Collections;

public class staff列表 : MonoBehaviour {

    private float time;
	// Use this for initialization
	void Start () {
        rectTransform = gameObject.GetComponent<RectTransform>();
        time = Time.time;
	}
    private RectTransform rectTransform;
    public float 播放时间;
    public float 等待时间=10f;
	// Update is called once per frame
    void Update()
    {
        if (time + 播放时间 > Time.time)
        {
            rectTransform.localPosition += new Vector3(0, 5480 / 播放时间 * Time.deltaTime, 0);

        }
        else//滚动结束
        {
            if (time + 播放时间 + 等待时间 < Time.time)
            {
                场景切换管理._instance.关卡进入时刻[游戏进度控制._instance.当前关卡] = Time.time;
                Invoke("启动主界面", 场景切换管理._instance.关卡切换时间);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            场景切换管理._instance.关卡进入时刻[游戏进度控制._instance.当前关卡] = Time.time;
            Invoke("启动主界面", 场景切换管理._instance.关卡切换时间);
        }
    }
    void 启动主界面()
    {
        Application.LoadLevel("2");
    }
}
