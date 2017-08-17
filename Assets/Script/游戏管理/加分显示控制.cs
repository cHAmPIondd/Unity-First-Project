using UnityEngine;
using System.Collections;

public class 加分显示控制 : MonoBehaviour {

    public GameObject 加分显示物;
    public float 向上偏移量;
	void OnDestroy()
    {
        Instantiate(加分显示物, transform.position + new Vector3(0, 向上偏移量, -10), Quaternion.identity);
    }
}
