using UnityEngine;
using System.Collections;

public class 冰子弹 : MonoBehaviour {
    public float moveSpeed = 10;
    public float bigSpeed = 10;
    public float 销毁时间 = 2;
    private float timer = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            other.transform.parent.gameObject.SendMessage("BeHit");
            Destroy(gameObject);
        }
    }
    void Update()
    {
        this.gameObject.transform.position =
                new Vector2(transform.position.x - Mathf.Cos(gameObject.transform.eulerAngles.z * Mathf.Deg2Rad) * moveSpeed * Time.deltaTime,
                             transform.position.y - Mathf.Sin(gameObject.transform.eulerAngles.z * Mathf.Deg2Rad) * moveSpeed * Time.deltaTime);
        this.gameObject.transform.localScale =
                new Vector3(transform.localScale.x + bigSpeed * Time.deltaTime, transform.localScale.y + bigSpeed * Time.deltaTime, 1);
        timer += Time.deltaTime;
        if(timer>销毁时间)
            Destroy(gameObject);
    }
}
