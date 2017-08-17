using UnityEngine;
using System.Collections;

public class 树懒静止 : MonoBehaviour
{

    public float 减速半径 = 4;
    public float 减速百分比 = 0.5f;
    public GameObject 树懒眼睛;
    private float timer;
    private bool is静止;
    public float 静止时间=5;

    private ArrayList 静止物品;
    private ArrayList 静止物品K;
    // Use this for initialization
    void Awake()
    {
        静止物品 = new ArrayList(10);
        静止物品K = new ArrayList(10);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 静止时间)
        {
            timer = 0;
            is静止 = !is静止;
            树懒眼睛.SetActive(is静止 ? true : false);
        }
        if (is静止)
        {
            for (int i = 0; i < 静止物品.Count; i++)
            {
                if (Vector2.SqrMagnitude(transform.position - ((第二关移动踏板)静止物品[i]).transform.position) > (2 + 减速半径) * (2 + 减速半径))
                {
                    ((第二关移动踏板)静止物品[i]).is静止 = false;
                    静止物品.RemoveAt(i);
                }
            }
            for (int i = 0; i < 静止物品K.Count; i++)
            {
                if (Vector2.SqrMagnitude(transform.position - ((Rigidbody2D)静止物品K[i]).transform.position) > (2 + 减速半径) * (2 + 减速半径))
                {
                    ((Rigidbody2D)静止物品K[i]).isKinematic = false;
                    静止物品K.RemoveAt(i);
                }
            }
        }
        else
        {
            for (int i = 0; i < 静止物品.Count; i++)
            {
                ((第二关移动踏板)静止物品[i]).is静止 = false;
                静止物品.RemoveAt(i);
            }
            for (int i = 0; i < 静止物品K.Count; i++)
            {
                ((Rigidbody2D)静止物品K[i]).isKinematic = false;
                静止物品K.RemoveAt(i);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "floor")
        {
            if (other.GetComponent<Rigidbody2D>() != null&&is静止)
            {
                if (other.name != "移动树懒踏板(Clone)")
                {
                    int i = 0;
                    for (; i < 静止物品K.Count; i++)
                    {
                        if ((Rigidbody2D)静止物品K[i] == other.GetComponent<Rigidbody2D>())
                            break;
                    }
                    if (i == 静止物品K.Count)
                        静止物品K.Add(other.GetComponent<Rigidbody2D>());
                    other.GetComponent<Rigidbody2D>().isKinematic = true;
                }
                if (other.GetComponent<第二关移动踏板>() != null)
                {
                    int i = 0;
                    for (; i < 静止物品.Count; i++)
                    {
                        if ((第二关移动踏板)静止物品[i] == other.GetComponent<第二关移动踏板>())
                            break;
                    }
                    if (i == 静止物品.Count)
                        静止物品.Add(other.GetComponent<第二关移动踏板>());
                    other.GetComponent<第二关移动踏板>().is静止 = true;
                }
            }
        }
    }



}
