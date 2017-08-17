using UnityEngine;
using System.Collections;

public class 第六关岩浆 : MonoBehaviour {

    private bool isStay;
    private GameObject GameObject;
    private bool is1P;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player" && (other.transform.parent.gameObject.GetComponent<Player>().is1P && 游戏进度控制._instance.当前1P状态 ||
            !other.transform.parent.gameObject.GetComponent<Player>().is1P && !游戏进度控制._instance.当前1P状态))
        {
            is1P = 游戏进度控制._instance.当前1P状态;
            isStay = true;
            GameObject = other.gameObject;
        }
        if(other.tag=="敌人")
        {
            Destroy(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "player" && (other.transform.parent.gameObject.GetComponent<Player>().is1P && 游戏进度控制._instance.当前1P状态 ||
            !other.transform.parent.gameObject.GetComponent<Player>().is1P && !游戏进度控制._instance.当前1P状态))
        {
            isStay = false;
        }
    }
    void Update()
    {
        if (is1P != 游戏进度控制._instance.当前1P状态)
        {
            isStay = false;
            is1P = 游戏进度控制._instance.当前1P状态;
        }
        if (isStay)
            GameObject.transform.parent.gameObject.SendMessage("BeHit");

    }
}
