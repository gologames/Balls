using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundSize : MonoBehaviour
{    
	void Start ()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float orSize = Camera.main.orthographicSize;
        float height = orSize * 2.0f;
        float aspect = Screen.width / (float)Screen.height;
        float width = height * aspect;
        sr.size = new Vector2(width, height);
    }
}
