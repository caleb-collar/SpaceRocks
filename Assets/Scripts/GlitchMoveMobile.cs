using System.Collections;
using UnityEngine;

public class GlitchMoveMobile : MonoBehaviour
{
    [Range(0.1f, 200f)]
    public float GlitchSpeed = 90.0f;
    [Range(0.001f, 0.1f)]
    public float GlitchAmt = 0.1f;
    private Vector2 startingPos;
    private Vector2 newPos;

    void Start ()
    {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
    }
     
    void Update ()
    {
        startingPos = GetComponentInParent<Transform>().position;
        newPos.y = startingPos.y;
        newPos.x = startingPos.x + Mathf.Sin(Time.time * GlitchSpeed) * GlitchAmt;
        transform.position = newPos;
    }
}
