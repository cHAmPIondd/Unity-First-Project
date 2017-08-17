using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class 手柄控制 : MonoBehaviour {

    private Toggle 这个按钮;
    public bool is1P;
    public GameObject 遮罩;
    void OnEnable()
    {
        if (is1P)
            这个按钮.isOn = 
                游戏按键控制.手柄1P;
        else
            这个按钮.isOn = 游戏按键控制.手柄2P;
    }
	// Use this for initialization
    public void OnChange()
    {
        if (is1P)
            游戏按键控制.手柄1P = 这个按钮.isOn;
        else
            游戏按键控制.手柄2P = 这个按钮.isOn;
        if (这个按钮.isOn)
            遮罩.SetActive(true);
        else
            遮罩.SetActive(false);
    }

	void Awake () {
        这个按钮 = GetComponent<Toggle>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
