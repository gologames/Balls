using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Ball : MonoBehaviour
{
    private const float BALL_SPEED = 0.9f;
    private const float SCALE_FACTOR = 0.3f;
    private const float BALL_X_MARGIN = 0.95f;
    private const int TIME_LIVE_SEC = 20;
    [SerializeField]
    private Sprite[] colors;
    private SceneController scc;
    private float speed;
    private float scale;

    void Start()
    {
        #region FIND_AND_CHECK_GAMEOBJECTS
        if (colors.Length == 0)
        {
            Debug.Log("Need add colors sprites to Ball prefab");
            Destroy(gameObject);
            return;
        }
        GameObject sccObj = GameObject.Find("Scene Controller");
        if (!sccObj)
        {
            Debug.Log("Need add Scene Controller to scene");
            Destroy(gameObject);
            return;
        }
        scc = sccObj.GetComponent<SceneController>();
        GameObject background = GameObject.Find("Background");
        if (!background)
        {
            Debug.Log("Need add Background to scene");
            Destroy(gameObject);
            return;
        }
        #endregion

        #region INIT_BALL
        SpriteRenderer backSr = background.GetComponent<SpriteRenderer>();
        float backX = background.transform.position.x;
        float backY = background.transform.position.y;
        float backWidth = backSr.size.x;
        float backHeight = backSr.size.y;

        SpriteRenderer circleSr = GetComponent<SpriteRenderer>();
        scale = Random.Range(SCALE_FACTOR, 1.0f);
        float circleSide = circleSr.size.x * scale;
        circleSr.size = new Vector2(circleSide, circleSide);
        GetComponent<CircleCollider2D>().radius = circleSide / 2.0f;

        float widthRange = (backWidth / 2.0f - circleSide /2.0f) * BALL_X_MARGIN;
        float newX = Random.Range(backX - widthRange, backX + widthRange);
        float newY = backY - backHeight / 2.0f - circleSide / 2.0f;
        float newZ = Random.value;
        transform.position = new Vector3(newX, newY, newZ);
        circleSr.sprite = colors[Random.Range(0, colors.Length)];
        speed = BALL_SPEED * (1.0f / scale) * scc.GetSpeedFactor();
        Destroy(gameObject, TIME_LIVE_SEC);
        #endregion
    }

    void Update()
    {
        float newY = transform.position.y + speed * Time.deltaTime;
        transform.position = new Vector3(
            transform.position.x, newY, transform.position.z);
    }

    public void OnMouseDown()
    {
        scc.AddScore(scale);
        Destroy(gameObject);
    }
}
