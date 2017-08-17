using UnityEngine;
using System.Collections;

public class Boss镜头显示 : MonoBehaviour {

    private float targetX = 400.5f;
    private float targetY = -7.5f;
    private Transform cameraTransform;
    private Camera camera;
    private float timer;
    private GameObject 左挡板;
    private GameObject 右挡板;
    private Transform playerTramsform_1P;
    private Transform playerTramsform_2P;
    private float width = 20;
    void Start()
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        左挡板 = GameObject.Find("左挡板");
        右挡板 = GameObject.Find("右挡板");
        playerTramsform_1P = GameObject.Find("火柴人1P").transform;
        playerTramsform_2P = GameObject.Find("火柴人2P").transform;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (timer < 2)
        {
            timer += Time.deltaTime;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, new Vector3(targetX, targetY, -30), 2f * Time.deltaTime);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 7.5f, 2f * Time.deltaTime);
            左挡板.transform.localPosition = new Vector2(10 - 10 * camera.orthographicSize / 7.5f, 左挡板.transform.localPosition.y);
            右挡板.transform.localPosition = new Vector2(10 + 10 * camera.orthographicSize / 7.5f, 右挡板.transform.localPosition.y);
        }
        else
        {
            cameraTransform.position = new Vector3(targetX, targetY, -30);
        }
        //人物位置限制
        //1P
        if (playerTramsform_1P.position.x > cameraTransform.position.x + width / 2)
        {
            playerTramsform_1P.position = new Vector3(cameraTransform.position.x + width / 2 - 0.1f,
                                                   playerTramsform_1P.position.y, playerTramsform_1P.position.z);
        }
        if (playerTramsform_1P.position.x < cameraTransform.position.x - width / 2)
        {
            playerTramsform_1P.position = new Vector3(cameraTransform.position.x - width / 2 + 0.1f,
                                                 playerTramsform_1P.position.y, playerTramsform_1P.position.z);
        }
        if (playerTramsform_1P.position.y < -12.75f)
        {
            playerTramsform_1P.position = new Vector3(playerTramsform_1P.position.x, -12.75f, playerTramsform_1P.position.z);
        }
        if (playerTramsform_1P.position.y > -1)
        {
            playerTramsform_1P.position = new Vector3(playerTramsform_1P.position.x, -1f, playerTramsform_1P.position.z);
        }
        //2P
        if (playerTramsform_2P.position.x > cameraTransform.position.x + width / 2)
        {
            playerTramsform_2P.position = new Vector3(cameraTransform.position.x + width / 2 - 0.1f,
                                                   playerTramsform_2P.position.y, playerTramsform_2P.position.z);
        }
        if (playerTramsform_2P.position.x < cameraTransform.position.x - width / 2)
        {
            playerTramsform_2P.position = new Vector3(cameraTransform.position.x - width / 2 + 0.1f,
                                                   playerTramsform_2P.position.y, playerTramsform_2P.position.z);
        }
        if (playerTramsform_2P.position.y < -12.75f)
        {
            playerTramsform_2P.position = new Vector3(playerTramsform_2P.position.x, -12.75f, playerTramsform_2P.position.z);
        }
        if (playerTramsform_2P.position.y > -1)
        {
            playerTramsform_2P.position = new Vector3(playerTramsform_2P.position.x, -1, playerTramsform_2P.position.z);
        }
    }
}
