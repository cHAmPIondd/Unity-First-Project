using UnityEngine;
using System.Collections;

public class 激活时音效 : MonoBehaviour {

    public AudioSource music;
	// Use this for initialization
    void OnEnable()
    {
        music.Play();
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
