using UnityEngine;
using System.Collections;

public class 鼠标操控 : MonoBehaviour {

    public static 鼠标操控 _instance;

    public void 隐藏鼠标()
    {
        this.gameObject.SetActive(false);
    }
    public void 显示鼠标()
    {
        this.gameObject.SetActive(true);
    }
    void Start()
    {
        Cursor.visible = false;
        _instance = this;
    }


    void Update()
    {
        this.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 10f, -1);

    }
}
