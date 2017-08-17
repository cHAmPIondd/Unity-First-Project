using UnityEngine;
using System.Collections;

public class 正常镜头显示 : MonoBehaviour {
    private Transform playerTransform_1P;
    private Transform playerTransform_2P;
    private Transform cameraTransform;
    private Camera Camera;
    private float width = 220 / 15f;
    private GameObject 左挡板;
    private GameObject 右挡板;
    private Rigidbody2D playerRigidbody2D_1P;
    private Rigidbody2D playerRigidbody2D_2P;
    void Start()
    {
        playerTransform_1P = GameObject.Find("火柴人1P").transform;
        playerTransform_2P = GameObject.Find("火柴人2P").transform;
        cameraTransform = GameObject.Find("Main Camera").transform;
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        左挡板 = GameObject.Find("左挡板");
        右挡板 = GameObject.Find("右挡板");
        playerRigidbody2D_1P = playerTransform_1P.gameObject.GetComponent<Rigidbody2D>();
        playerRigidbody2D_2P = playerTransform_2P.gameObject.GetComponent<Rigidbody2D>();

    }
    void Update()//横版
    {
        //摄像头大小与位置
        if (playerTransform_1P.position.y > playerTransform_2P.position.y)
        {
            if (playerTransform_1P.position.y > -3.6f)
            {
                if (playerTransform_2P.position.y < -10.1)//上下
                {
                    Camera.orthographicSize = (playerTransform_1P.position.y + 3.6f - playerTransform_2P.position.y - 10.1f) / 2 + 5.5f;
                    cameraTransform.position = new Vector3((playerTransform_1P.position.x + playerTransform_2P.position.x) / 2,
                                             (playerTransform_1P.position.y + 3.6f + playerTransform_2P.position.y + 10.1f) / 2 - 7.5f,
                                             cameraTransform.position.z);
                }
                else//上
                {
                    Camera.orthographicSize = (playerTransform_1P.position.y + 3.6f) / 2 + 5.5f;
                    cameraTransform.position = new Vector3((playerTransform_1P.position.x + playerTransform_2P.position.x) / 2,
                                            (playerTransform_1P.position.y + 3.6f) / 2 - 7.5f,
                                            cameraTransform.position.z);
                }
            }
            else
            {
                if (playerTransform_2P.position.y < -10.1)//下
                {
                    Camera.orthographicSize = (-playerTransform_2P.position.y - 10.1f) / 2 + 5.5f;
                    cameraTransform.position = new Vector3((playerTransform_1P.position.x + playerTransform_2P.position.x) / 2,
                                            (playerTransform_2P.position.y + 10.1f) / 2 - 7.5f,
                                            cameraTransform.position.z);
                }
                else//空
                {
                    Camera.orthographicSize = 5.5f;
                    cameraTransform.position = new Vector3((playerTransform_1P.position.x + playerTransform_2P.position.x) / 2,
                                                        -7.5f, cameraTransform.position.z);
                }
            }
        }
        else
        {
            if (playerTransform_2P.position.y > -3.6f)
            {
                if (playerTransform_1P.position.y < -10.1)//上下
                {
                    Camera.orthographicSize = (playerTransform_2P.position.y + 3.6f - playerTransform_1P.position.y - 10.1f) / 2 + 5.5f;
                    cameraTransform.position = new Vector3((playerTransform_1P.position.x + playerTransform_2P.position.x) / 2,
                                            (playerTransform_2P.position.y + 3.6f + playerTransform_1P.position.y + 10.1f) / 2 - 7.5f,
                                            cameraTransform.position.z);
                }
                else//上
                {
                    Camera.orthographicSize = (playerTransform_2P.position.y + 3.6f) / 2 + 5.5f;
                    cameraTransform.position = new Vector3((playerTransform_1P.position.x + playerTransform_2P.position.x) / 2,
                                            (playerTransform_2P.position.y + 3.6f) / 2 - 7.5f,
                                            cameraTransform.position.z);
                }
            }
            else
            {
                if (playerTransform_1P.position.y < -10.1)//下
                {
                    Camera.orthographicSize = (-playerTransform_1P.position.y - 10.1f) / 2 + 5.5f;
                    cameraTransform.position = new Vector3((playerTransform_1P.position.x + playerTransform_2P.position.x) / 2,
                                                (playerTransform_1P.position.y + 10.1f) / 2 - 7.5f,
                                                 cameraTransform.position.z);
                }
                else//空
                {
                    Camera.orthographicSize = 5.5f;
                    cameraTransform.position = new Vector3((playerTransform_1P.position.x + playerTransform_2P.position.x) / 2,
                                                    -7.5f, cameraTransform.position.z);
                }
            }
        }
        //摄像头大小限制
        if (Camera.orthographicSize > 7.5)
            Camera.orthographicSize = 7.5f;
        //摄像头位置限制
        if (cameraTransform.position.x < 8)
            cameraTransform.position = new Vector3(8, cameraTransform.position.y, cameraTransform.position.z);

        if (cameraTransform.position.y + Camera.orthographicSize > 0)
            cameraTransform.position = new Vector3(cameraTransform.position.x, -Camera.orthographicSize, cameraTransform.position.z);
        if (cameraTransform.position.y - Camera.orthographicSize < -15)
            cameraTransform.position = new Vector3(cameraTransform.position.x, -15f + Camera.orthographicSize, cameraTransform.position.z);
        //人物位置限制
        //1P
        if (playerTransform_1P.position.x > cameraTransform.position.x + width / 2)
        {
            playerTransform_1P.position = new Vector3(cameraTransform.position.x + width / 2 - 0.1f,
                                                   playerTransform_1P.position.y, playerTransform_1P.position.z);
        }
        if (playerTransform_1P.position.x < cameraTransform.position.x - width / 2)
        {
            playerTransform_1P.position = new Vector3(cameraTransform.position.x - width / 2 + 0.1f,
                                                 playerTransform_1P.position.y, playerTransform_1P.position.z);
        }
    /*    if (playerTransform_1P.position.y < -12.75f)
        {
            playerTransform_1P.position = new Vector3(playerTransform_1P.position.x, -12.75f, playerTransform_1P.position.z);
        }
        if (playerTransform_1P.position.y > -1)
        {
            playerTransform_1P.position = new Vector3(playerTransform_1P.position.x, -1f, playerTransform_1P.position.z);
            playerRigidbody2D_1P.velocity = new Vector2(playerRigidbody2D_2P.velocity.x, -playerRigidbody2D_1P.velocity.y / 4);
        }*/
        //2P
        if (playerTransform_2P.position.x > cameraTransform.position.x + width / 2)
        {
            playerTransform_2P.position = new Vector3(cameraTransform.position.x + width / 2 - 0.1f,
                                                   playerTransform_2P.position.y, playerTransform_2P.position.z);
        }
        if (playerTransform_2P.position.x < cameraTransform.position.x - width / 2)
        {
            playerTransform_2P.position = new Vector3(cameraTransform.position.x - width / 2 + 0.1f,
                                                   playerTransform_2P.position.y, playerTransform_2P.position.z);
        }
   /*     if (playerTransform_2P.position.y < -12.75f)
        {
            playerTransform_2P.position = new Vector3(playerTransform_2P.position.x, -12.75f, playerTransform_2P.position.z);
        }
        if (playerTransform_2P.position.y > -1)
        {
            playerTransform_2P.position = new Vector3(playerTransform_2P.position.x, -1, playerTransform_2P.position.z);
            playerRigidbody2D_2P.velocity = new Vector2(playerRigidbody2D_2P.velocity.x, -playerRigidbody2D_2P.velocity.y/4);

        }*/
        //挡板位置
        左挡板.transform.localPosition = new Vector2(10 - 10 * Camera.orthographicSize / 7.5f, 左挡板.transform.localPosition.y);
        右挡板.transform.localPosition = new Vector2(10 + 10 * Camera.orthographicSize / 7.5f, 右挡板.transform.localPosition.y);
    }


}
