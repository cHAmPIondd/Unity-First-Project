using UnityEngine;
using System.Collections;

public class 加血 : MonoBehaviour {
    private AudioSource 加血提示音;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player" && (other.transform.parent.gameObject.GetComponent<Player>().is1P && 游戏进度控制._instance.当前1P状态 || !other.transform.parent.gameObject.GetComponent<Player>().is1P && !游戏进度控制._instance.当前1P状态))
        {
            加血提示音.Play();
            other.transform.parent.gameObject.SendMessage("BeHeal");
            Destroy(this.gameObject);
        }
    }
	// Use this for initialization
	void Start () {
        加血提示音 = GameObject.Find("游戏公用物/音效/加血").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
