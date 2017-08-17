using UnityEngine;
using System.Collections;

public class 加分 : MonoBehaviour {

    private AudioSource 加分提示音;
    public int 加能量值=2;
    private RectTransform 能量遮罩_1P;
    private RectTransform 能量遮罩_2P;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player" && (other.transform.parent.gameObject.GetComponent<Player>().is1P && 游戏进度控制._instance.当前1P状态 || !other.transform.parent.gameObject.GetComponent<Player>().is1P && !游戏进度控制._instance.当前1P状态))
        {
            加分提示音.Play();
            游戏进度控制._instance.分数 += 100;

            if(游戏进度控制._instance.当前1P状态)
                游戏进度控制._instance.能量_2P += 加能量值;
            else
                游戏进度控制._instance.能量_1P += 加能量值;

            if (游戏进度控制._instance.能量_1P > 游戏进度控制._instance.虚体能量Max)
                游戏进度控制._instance.能量_1P = 游戏进度控制._instance.虚体能量Max;
            if (游戏进度控制._instance.能量_2P > 游戏进度控制._instance.虚体能量Max)
                游戏进度控制._instance.能量_2P = 游戏进度控制._instance.虚体能量Max;

            能量遮罩_1P.sizeDelta = new Vector2(游戏进度控制._instance.能量_1P * 635 / 30f+0.01f, 能量遮罩_1P.sizeDelta.y);
            能量遮罩_2P.sizeDelta = new Vector2(游戏进度控制._instance.能量_2P * 635 / 30f+0.01f, 能量遮罩_2P.sizeDelta.y);

            Destroy(this.gameObject);
        }
    }
	// Use this for initialization
    void Start()
    {
        加分提示音 = GameObject.Find("游戏公用物/音效/加分").GetComponent<AudioSource>();
        能量遮罩_1P = GameObject.Find("Canvas/游戏UI/1P状态/能量/遮罩").GetComponent<RectTransform>();
        能量遮罩_2P = GameObject.Find("Canvas/游戏UI/2P状态/能量/遮罩").GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
