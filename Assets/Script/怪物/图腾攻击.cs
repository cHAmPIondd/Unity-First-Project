using UnityEngine;
using System.Collections;

public class 图腾攻击 : MonoBehaviour
{
    public float 攻击距离;
    public float 攻击预备时间;
    public float 攻击间隔时间;
    private float 上次攻击时间=-10;
    private Transform playerTransform_1P;
    private Transform playerTransform_2P;
    private Vector3 target;
    public GameObject 激光;
    private LineRenderer 真激光;
    private AudioSource 激光声;
    private LayerMask playerLayer;
    private bool is攻击;
    public float 攻击持续时间 = 0.1f;
    private float 攻击timer=0;
    
    void BeHit()
    {

    }
    // Use this for initialization
    void Start()
    {
        playerTransform_1P = GameObject.Find("火柴人1P").transform;
        playerTransform_2P = GameObject.Find("火柴人2P").transform;
        真激光 = 激光.GetComponent<LineRenderer>();
        playerLayer = LayerMask.GetMask("实体","虚体");
        激光声=激光.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (游戏进度控制._instance.当前1P状态 && Vector2.SqrMagnitude(playerTransform_1P.position - 激光.transform.position) < 攻击距离 * 攻击距离
            || !游戏进度控制._instance.当前1P状态 && Vector2.SqrMagnitude(playerTransform_2P.position - 激光.transform.position) < 攻击距离 * 攻击距离)//实体在攻击范围内
        {
            if (上次攻击时间 + 攻击间隔时间 < Time.time)//可攻击
            {
                上次攻击时间 = Time.time;
                float tempAngle;
                if (游戏进度控制._instance.当前1P状态)
                {
                    tempAngle = Mathf.Rad2Deg *
                        Mathf.Atan((playerTransform_1P.position.y + 0.5f - 激光.transform.position.y) / (playerTransform_1P.position.x - 激光.transform.position.x));
                    if (transform.position.x < playerTransform_1P.position.x)
                        tempAngle = 180 + tempAngle;
                    target = playerTransform_1P.position + new Vector3(0, 0.5f, 0);
                }
                else
                {
                    tempAngle = Mathf.Rad2Deg *
                        Mathf.Atan((playerTransform_2P.position.y + 0.5f - 激光.transform.position.y) / (playerTransform_2P.position.x - 激光.transform.position.x));
                    if (transform.position.x < playerTransform_2P.position.x)
                        tempAngle = 180 + tempAngle;
                    target = playerTransform_2P.position + new Vector3(0, 0.5f, 0);
                }
                激光.transform.localEulerAngles = new Vector3(0, 0, tempAngle);
                激光.GetComponent<Animator>().enabled = true;
                Invoke("攻击", 攻击预备时间);
            }
        }
        if (is攻击)
        {
            攻击timer += Time.deltaTime;
            if(攻击timer>攻击持续时间)
            {
                is攻击 = false;
                真激光.enabled = false;
                攻击timer = 0;
            }
        }
        
    } 
    void 攻击()
    {
        激光.GetComponent<Animator>().enabled = false;
        激光.GetComponent<SpriteRenderer>().enabled = false;

        RaycastHit2D hit = Physics2D.Raycast(激光.transform.position, target - 激光.transform.position , 100, playerLayer);
        真激光.enabled = true;
        真激光.SetPosition(0, 激光.transform.position - new Vector3(0, 0, 1));
        激光声.Play();
        if (hit)
        {
            if (hit.transform.tag == "floor")
            {
                真激光.SetPosition(1, (Vector3)hit.point-new Vector3(0,0,2));
            }
            else
            {
                if (hit.transform.gameObject.GetComponent<Player>().is1P && !游戏进度控制._instance.当前1P状态
              || !hit.transform.gameObject.GetComponent<Player>().is1P && 游戏进度控制._instance.当前1P状态)
                    真激光.SetPosition(1, hit.point);
                else
                    真激光.SetPosition(1, new Vector3((激光.transform.position + 100 * (target - 激光.transform.position)).x, (激光.transform.position + 100 * (target - 激光.transform.position)).y, 0));
                hit.transform.gameObject.SendMessage("BeHit");
            }
        }
        else
        {
            真激光.SetPosition(1, new Vector3((激光.transform.position + 100 * (target - 激光.transform.position)).x, (激光.transform.position + 100 * (target - 激光.transform.position)).y,0));
        }
        is攻击 = true;
    }




}
