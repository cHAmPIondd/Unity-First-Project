using UnityEngine;
using System.Collections;

public class 普通子弹 : MonoBehaviour {

    public float moveSpeed = 10;
    private float worldWidth = 1000;
    private float worldHeight=100;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            other.transform.parent.gameObject.SendMessage("BeHit");
            Destroy(gameObject);
        }
    }
	void Update () {
        this.gameObject.transform.position =
                new Vector2(transform.position.x - Mathf.Cos(gameObject.transform.eulerAngles.z * Mathf.Deg2Rad) * moveSpeed * Time.deltaTime,
                             transform.position.y - Mathf.Sin(gameObject.transform.eulerAngles.z * Mathf.Deg2Rad) * moveSpeed * Time.deltaTime);
        if (gameObject.transform.position.x > worldWidth || gameObject.transform.position.x < -worldWidth || gameObject.transform.position.y > worldHeight || gameObject.transform.position.y < -worldHeight)
            Destroy(gameObject);
    }
}
