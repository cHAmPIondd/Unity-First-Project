using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class 开始界面动画 : MonoBehaviour {

    public Image 文字SpriteRenderer;
    private SpriteRenderer 火柴人SpriteRenderer;

    public Sprite 文字Sprite_1;
    public Sprite 文字Sprite_2;


    void Start()
    {
        火柴人SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnMouseEnter()
    {
        火柴人SpriteRenderer.color = new Color(1, 1, 1, 0);
        文字SpriteRenderer.sprite = 文字Sprite_2;
    }

    void OnMouseOver()
    {
        火柴人SpriteRenderer.color += new Color(0, 0, 0, 1f) * Time.deltaTime;
    }
    void OnMouseExit()
    {
        火柴人SpriteRenderer.color = new Color(1, 1, 1, 0);
        文字SpriteRenderer.sprite = 文字Sprite_1;
            
    }
	// Update is called once per frame
	void Update () {
	
	}
}
