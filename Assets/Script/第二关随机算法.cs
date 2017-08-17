using UnityEngine;
using System.Collections;

public class 第二关随机算法 : MonoBehaviour {

    public GameObject 普通踏板;
    public GameObject 移动踏板;
    public GameObject 树懒踏板;
    public GameObject 移动树懒踏板;

    public float 横向最小距离=6.5f;
    public float 横向最大距离=12.5f;//=纵向距离-7.5/-0.6
    public float 纵向最小距离=1.4f;
    public float 纵向最大距离=2.4f;

    public float 地图top = 180f;
    public float 地图buttom = -15f;
    public float 地图left = -10;
    public float 地图right = 30;

    public float 移动踏板最大移动距离=30f;
    public float 移动踏板最小移动距离=10f;
    public float 移动踏板最大移动速度 = 5f;
    public float 移动踏板最小移动速度 = 1f;

    public Rigidbody2D[] 自动不锁定踏板;
    private int 自动不锁定踏板数=0;
    public 第二关移动踏板[] 自动不静止踏板;
    private int 自动不静止踏板数 = 0;
	// Use this for initialization
    void Start()
    {
        自动不锁定踏板=new Rigidbody2D[300];
        自动不静止踏板 = new 第二关移动踏板[300];
        for(int i=0;i<地图top/纵向最大距离;i++)
        {
            float last踏板位置 = 地图right + 横向最小距离;
            for(int j=0;j<50;j++)
            {
                GameObject temp = 随机踏板();
                temp.transform.parent = transform;
                temp.transform.position = new Vector2(last踏板位置 - Random.Range(横向最小距离, 横向最大距离), i * 纵向最大距离 + Random.Range(纵向最小距离, 纵向最大距离)+地图buttom);
                if (temp.GetComponent<第二关移动踏板>() != null)//是移动踏板
                {
                    last踏板位置 = -temp.GetComponent<第二关移动踏板>().徘徊距离 + temp.transform.position.x;
                    if (last踏板位置 < 地图left)
                    {
                        break;
                    }
                }
                else//不是移动踏板
                {
                    last踏板位置 = temp.transform.position.x;
                    if (last踏板位置 < 地图left) break;
                }
            }
        }
    }
    GameObject 随机踏板()
    {
        float 随机数 = Random.Range(0, 10.0f);
        if (随机数 < 5)//踏板
        {
            自动不锁定踏板[自动不锁定踏板数] = Instantiate(普通踏板).GetComponent<Rigidbody2D>();
            自动不锁定踏板数++;
            return 自动不锁定踏板[自动不锁定踏板数-1].gameObject;
        }
        else if (随机数 < 7)//移动踏板
        {
            GameObject temp = Instantiate(移动踏板);
            自动不锁定踏板[自动不锁定踏板数] = temp.GetComponent<Rigidbody2D>();
            自动不锁定踏板数++;
            自动不静止踏板[自动不静止踏板数] = temp.GetComponent<第二关移动踏板>();
            自动不静止踏板数++;
            temp.GetComponent<第二关移动踏板>().徘徊距离 = Random.Range(移动踏板最小移动距离, 移动踏板最大移动距离);
            temp.GetComponent<第二关移动踏板>().moveSpeed = Random.Range(移动踏板最小移动速度, 移动踏板最大移动速度);
            return temp;
        }
        else if (随机数 < 9)//树懒踏板
        {
            return Instantiate(树懒踏板);
        }
        else//移动树懒踏板
        {
            GameObject temp = Instantiate(移动树懒踏板);
            自动不静止踏板[自动不静止踏板数] = temp.GetComponent<第二关移动踏板>();
            自动不静止踏板数++;
            temp.GetComponent<第二关移动踏板>().徘徊距离 = Random.Range(移动踏板最小移动距离, 移动踏板最大移动距离);
            temp.GetComponent<第二关移动踏板>().moveSpeed = Random.Range(移动踏板最小移动速度, 移动踏板最大移动速度);
            return temp;
        }
    }
    void Update()
    {

    }
}
