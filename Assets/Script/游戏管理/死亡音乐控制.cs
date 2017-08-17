using UnityEngine;
using System.Collections;

public class 死亡音乐控制 : MonoBehaviour {
    private AudioSource music;
    private bool is渐变;
	// Use this for initialization
	void Start () {
        Invoke("渐变",4.5f);
	}
	
    void 渐变()
    {
        music=GetComponent<AudioSource>();
        is渐变 = true;
    }
	// Update is called once per frame
	void Update () {
	    if(is渐变)
        {
            music.volume-=(1f/2*Time.deltaTime);
        }
	}
}
