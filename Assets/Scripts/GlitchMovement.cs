using UnityEngine;

public class GlitchMovement : MonoBehaviour
{
    [Range(0.1f, 100f)]
    public float GlitchSpeed = 90.0f;
    [Range(0.001f, 0.1f)]
    public float GlitchAmt = 0.005f;
    private Vector2 startingPos;
    private Vector2 newPos;

    void Start ()
    {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
    }
     
    void Update ()
    {
        newPos.x = startingPos.x + Mathf.Cos(Time.time * GlitchSpeed) * GlitchAmt;
        newPos.y = startingPos.y + Mathf.Sin(Time.time * GlitchSpeed) * GlitchAmt;
        transform.position = newPos;
    }
}
