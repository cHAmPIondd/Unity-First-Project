using UnityEngine;
using System.Collections;

public class 草原小怪受伤判定 : MonoBehaviour {

    public int hp = 1;


    public void BeHit()
    {
        hp--;
        if (hp <= 0)
        {
            Death();
        }
    }

    public GameObject 小喽罗死亡粒子 = null;

    private int 加能量值 = 1;
    private RectTransform 能量遮罩_1P;
    private RectTransform 能量遮罩_2P;
    void Death()
    {
        GameObject 小喽罗死亡粒子_ex = (GameObject)Instantiate(小喽罗死亡粒子);
        Vector3 enemyPos = transform.position;
        小喽罗死亡粒子_ex.transform.position = new Vector3(enemyPos.x, enemyPos.y, enemyPos.z + 1.0f);
        游戏进度控制._instance.分数 += 50;

        if (游戏进度控制._instance.当前1P状态)
            游戏进度控制._instance.能量_2P += 加能量值;
        else
            游戏进度控制._instance.能量_1P += 加能量值;

        if (游戏进度控制._instance.能量_1P > 游戏进度控制._instance.虚体能量Max)
            游戏进度控制._instance.能量_1P = 游戏进度控制._instance.虚体能量Max;
        if (游戏进度控制._instance.能量_2P > 游戏进度控制._instance.虚体能量Max)
            游戏进度控制._instance.能量_2P = 游戏进度控制._instance.虚体能量Max;

        能量遮罩_1P.sizeDelta = new Vector2(游戏进度控制._instance.能量_1P * 635 / 30f+0.01f, 能量遮罩_1P.sizeDelta.y);
        能量遮罩_2P.sizeDelta = new Vector2(游戏进度控制._instance.能量_2P * 635 / 30f+0.01f, 能量遮罩_2P.sizeDelta.y);
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    void Start()
    {
        能量遮罩_1P = GameObject.Find("Canvas/游戏UI/1P状态/能量/遮罩").GetComponent<RectTransform>();
        能量遮罩_2P = GameObject.Find("Canvas/游戏UI/2P状态/能量/遮罩").GetComponent<RectTransform>();
    }
}
