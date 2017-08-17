using UnityEngine;
using System.Collections;

public class 攻击判定 : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="敌人")
        {
            other.SendMessage("BeHit");
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
