using UnityEngine;
using System.Collections;

public class 图腾状态控制 : MonoBehaviour {
    public GameObject[] 状态;
    private bool is地下=true;
    private int groundLayer;//地面层
    public float 切换时间=1;
    public Animator 激光;
    private bool is下一次;
    void Start()
    {
         groundLayer = LayerMask.GetMask("floor","moveFloor");
         InvokeRepeating("状态切换", 0, 切换时间);
	}
	
	
    void 状态切换()
    {
        if (!激光.enabled)
        {
            if (Random.Range(0, 1f) > 0.8||is下一次)
            {
                if (is地下)
                {
                    if (!Physics2D.Raycast(transform.position, Vector2.up, 3.5f, groundLayer))
                    {
                        状态[0].SetActive(false);
                        状态[1].SetActive(true);
                        Invoke("出土", 0.5f);
                        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                        is地下 = false;
                        is下一次 = false;
                    }
                    else
                        is下一次 = true;
                }
                else
                {
                    状态[2].SetActive(false);
                    状态[3].SetActive(true);
                    Invoke("入土", 0.5f);
                    is地下 = true;
                }
            }
        }
    }
    void 出土()
    {
        状态[1].SetActive(false);//出土状态
        状态[2].SetActive(true);//攻击状态
    }
    void 入土()
    {
        状态[3].SetActive(false);//入土状态
        状态[0].SetActive(true);//地下状态
    }
}
