using UnityEngine;
using System.Collections;

public class 激活时循环音效 : MonoBehaviour {

    public AudioSource music;
    void OnEnable()
    {
        music.Play();
    }
    void OnDisable()
    {
        music.Stop();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
