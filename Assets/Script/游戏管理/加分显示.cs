using UnityEngine;
using System.Collections;

public class 加分显示 : MonoBehaviour
{
    public float 存在时间 = 1f;
    private SpriteRenderer SpriteRenderer;
    public float 上升高度 = 1f;
    // Use this for initialization
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Destroy(this.gameObject, 存在时间);
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer.color -= new Color(0, 0, 0, 1 / 存在时间 * Time.deltaTime);
        transform.position += new Vector3(0,上升高度/存在时间*Time.deltaTime,0);
    }
}
