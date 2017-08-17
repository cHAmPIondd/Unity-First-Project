using UnityEngine;
using System.Collections;

public class 第五关镜头显示 : MonoBehaviour {

    private Transform playerTramsform_1P;
    private Transform playerTramsform_2P;
    private Transform cameraTransform;
    private Camera Camera;

    private Player playerPlayer_1P;
    private Player playerPlayer_2P;
    void Start()
    {
        playerTramsform_1P = GameObject.Find("火柴人1P").transform;
        playerTramsform_2P = GameObject.Find("火柴人2P").transform;
        cameraTransform = GameObject.Find("Main Camera").transform;
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        playerPlayer_1P = playerTramsform_1P.gameObject.GetComponent<Player>();
        playerPlayer_2P = playerTramsform_2P.gameObject.GetComponent<Player>();
    }

	// Update is called once per frame
	void Update () {
        cameraTransform.position = new Vector3(10, cameraTransform.position.y, cameraTransform.position.z);
        if (playerTramsform_1P.position.y > cameraTransform.position.y + 3.5f)
            cameraTransform.position = new Vector3(10, playerTramsform_1P.position.y - 3.5f, cameraTransform.position.z);
        if (playerTramsform_2P.position.y > cameraTransform.position.y +3.5f)
            cameraTransform.position = new Vector3(10, playerTramsform_2P.position.y -3.5f, cameraTransform.position.z);
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
        if (playerTramsform_1P.position.y < cameraTransform.position.y - 6 && !场景切换管理._instance.is正在切换场景() && !playerPlayer_1P.isDeath && !playerPlayer_2P.isDeath)
        {
            playerTramsform_1P.SendMessage("GameOver");
     
        }
        else if (playerTramsform_2P.position.y < cameraTransform.position.y - 6 && !场景切换管理._instance.is正在切换场景() && !playerPlayer_2P.isDeath && !playerPlayer_1P.isDeath)
        {
            playerTramsform_2P.SendMessage("GameOver");
        }
    }
}
