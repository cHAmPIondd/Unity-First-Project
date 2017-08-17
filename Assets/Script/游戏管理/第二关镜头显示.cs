using UnityEngine;
using System.Collections;

public class 第二关镜头显示 : MonoBehaviour {

    private Transform playerTramsform_1P;
    private Transform playerTramsform_2P;
    private Transform cameraTransform;

    void Start()
    {
        playerTramsform_1P = GameObject.Find("火柴人1P").transform;
        playerTramsform_2P = GameObject.Find("火柴人2P").transform;
        cameraTransform = GameObject.Find("Main Camera").transform;
    }
	// Update is called once per frame
	void Update () {
        //摄像头y轴位置
        if (playerTramsform_1P.position.y > cameraTransform.position.y+5.5f)
            cameraTransform.position = new Vector3(10, playerTramsform_1P.position.y - 5.5f, cameraTransform.position.z);
        if (playerTramsform_2P.position.y > cameraTransform.position.y + 5.5f)
            cameraTransform.position = new Vector3(10, playerTramsform_2P.position.y - 5.5f, cameraTransform.position.z);
        //摄像头x轴位置
        cameraTransform.position = new Vector3((playerTramsform_1P.position.x + playerTramsform_2P.position.x) / 2,
                                                    cameraTransform.position.y, cameraTransform.position.z);
        if (cameraTransform.position.x < 0)
            cameraTransform.position = new Vector3(0, cameraTransform.position.y, cameraTransform.position.z);
        if (cameraTransform.position.x > 20)
            cameraTransform.position = new Vector3(20, cameraTransform.position.y, cameraTransform.position.z);
        //人物位置限制
        if (playerTramsform_1P.position.x < cameraTransform.position.x - 20f / 2)
            playerTramsform_1P.position = new Vector3(cameraTransform.position.x - 20f / 2, playerTramsform_1P.position.y, playerTramsform_1P.position.z);
        if (playerTramsform_1P.position.x > cameraTransform.position.x + 20f / 2)
            playerTramsform_1P.position = new Vector3(cameraTransform.position.x + 20f / 2, playerTramsform_1P.position.y, playerTramsform_1P.position.z);

        if (playerTramsform_2P.position.x < cameraTransform.position.x - 20f / 2)
            playerTramsform_2P.position = new Vector3(cameraTransform.position.x - 20f / 2, playerTramsform_2P.position.y, playerTramsform_1P.position.z);
        if (playerTramsform_2P.position.x > cameraTransform.position.x + 20f / 2)
            playerTramsform_2P.position = new Vector3(cameraTransform.position.x + 20f / 2, playerTramsform_2P.position.y, playerTramsform_1P.position.z);
        //死亡判定
        if (playerTramsform_1P.position.y < cameraTransform.position.y - 6&&!场景切换管理._instance.is正在切换场景())
            again();
        else if (playerTramsform_2P.position.y < cameraTransform.position.y - 6 && !场景切换管理._instance.is正在切换场景())
            again();
        

    }

    public GameObject 第二关关卡预设体;
    void again()
    {
        生涯概况记录._instance.记录关卡死亡次数();
        cameraTransform.position = new Vector3(10, -7.5f, cameraTransform.position.z);
        playerTramsform_1P.position = new Vector3(6.06f, -10.47f, 1f);
        playerTramsform_2P.position = new Vector3(4.8f, -9.07f, 1f);
        GameObject temp = 花瓣.当前关卡;
        temp.SetActive(false);
        花瓣.当前关卡 = (GameObject)Instantiate(第二关关卡预设体, Vector3.zero, Quaternion.identity);
        花瓣.当前关卡.SetActive(true);
        Destroy(temp);
    }
}
