using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI背景渐渐切换 : MonoBehaviour {

    public Image 上层, 下层;
    public Sprite[] 背景;
    public float 切换时间;
    public float 停留时间;
    private float timer;
    // Use this for initialization
	void Start () {
        上层.sprite = 背景[0];
        下层.sprite = 背景[1];
	}
    private bool is出现=false;
    private int Index=2;
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer>停留时间)
        {
            if (is出现)
                上层.color += new Color(0, 0, 0, 1 / 切换时间 * Time.deltaTime);
            else
                上层.color -= new Color(0, 0, 0, 1 / 切换时间 * Time.deltaTime);
            if(timer>停留时间+切换时间)
            {
                if(is出现)
                {
                    下层.sprite = 背景[Index];
                }
                else
                {
                    上层.sprite = 背景[Index];
                }
                Index++;
                if (Index >= 6)
                    Index = 0;
                is出现 = !is出现;
                timer = 0;
            }
        }
	}
}
