using UnityEngine;
using System.Collections;

public class boss触发器 : MonoBehaviour {
    private bool player_1P;
    private bool player_2P;
    public Boss Boss;
    public 正常镜头显示 镜头A;
    public Boss镜头显示 镜头B;
    public AudioSource boss音乐;
    private AudioSource 音乐;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="player")
        {
            if(other.transform.parent.GetComponent<Player>().is1P)
            {
                if (player_2P)//触发boss
                {
                    Boss.isBoss触发 = true;
                    镜头A.enabled = false;
                    镜头B.enabled = true;
                    音乐.volume=0;
                    boss音乐.enabled = true;
                    Destroy(this.gameObject);
                }
                else
                    player_1P = true;
            }
            else
            {
                if (player_1P)//触发boss
                {
                    Boss.isBoss触发 = true;
                    镜头A.enabled = false;
                    镜头B.enabled = true;
                    音乐.volume = 0;
                    boss音乐.enabled = true;
                    Destroy(this.gameObject);
                }
                else
                    player_2P = true;
            }
        }
    }
	// Use this for initialization
	void Start () {
        音乐 = GameObject.Find("游戏公用物/音乐/第六关").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
