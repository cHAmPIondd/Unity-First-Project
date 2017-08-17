using UnityEngine;
using System.Collections;

public class 陨石 : MonoBehaviour {

    public float 速度;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            other.transform.parent.gameObject.SendMessage("BeHit");
            other.transform.parent.gameObject.SendMessage("BeHit");
            Destroy(gameObject);
        }
    }
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.left*速度/Time.deltaTime);
        Destroy(gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
