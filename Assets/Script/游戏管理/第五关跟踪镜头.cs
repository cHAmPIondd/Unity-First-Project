using UnityEngine;
using System.Collections;

public class 第五关跟踪镜头 : MonoBehaviour {

    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, 9);
    }
}
