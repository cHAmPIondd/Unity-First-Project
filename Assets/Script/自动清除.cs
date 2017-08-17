using UnityEngine;
using System.Collections;

public class 自动清除 : MonoBehaviour {

    public float 清除时间;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 清除时间);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
