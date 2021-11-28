using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SineDance2D : MonoBehaviour
{
    private RectTransform rect;

    private float startPositionX;
    private float startPositionY;
    private Vector2 startScale;

    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();

        startPositionX = rect.anchoredPosition.x;
        startPositionY = rect.anchoredPosition.y;
        startScale = rect.localScale;
    }

    //ROTATE
    public bool rotate;
    public float sinRotSpeed = 5;
    public float sinRotAmp = 5;

    //SCALE
    public bool scale;
    public float sinSizeSpeed = 1;
    public float sinSizeAmp = 1;

    //SCALE OFFSETS
    public bool offsetX;
    public float offsetXAmp = 0;
    public float offsetXSpeed = 0;
    public bool offsetY;
    public float offsetYAmp = 0;
    public float offsetYSpeed = 0;

    //X
    public bool moveX;
    public float sinXSpeed = 1;
    public float sinXAmp = 1;

    //Y
    public bool moveY;
    public float sinYSpeed = 1;
    public float sinYAmp = 1;

    void Update()
    {
        if (rotate)
        {
            Vector3 rotationVector = new Vector3(0, 0, (sinRotAmp * Mathf.Sin(Time.time * sinRotSpeed)));
            Quaternion rotation = Quaternion.Euler(rotationVector);
            rect.rotation = rotation;
        }

        if (scale)
        {
            Vector3 scaleVector = new Vector3(startScale.x + offsetXAmp + (sinSizeAmp * Mathf.Sin(Time.time * sinSizeSpeed + offsetXSpeed)), startScale.y + offsetYAmp + (sinSizeAmp * Mathf.Sin(Time.time * sinSizeSpeed + offsetYSpeed)), 0f);
            rect.localScale = scaleVector;
        }

        if (moveX)
        {
            rect.anchoredPosition = new Vector3(startPositionX + (sinXAmp * Mathf.Sin(Time.time * sinXSpeed)), rect.anchoredPosition.y, 0f);
        }

        if (moveY)
        {
            rect.anchoredPosition = new Vector3(rect.anchoredPosition.x, startPositionY + (sinYAmp * Mathf.Sin(Time.time * sinYSpeed)), 0f);
        }
    }
}
