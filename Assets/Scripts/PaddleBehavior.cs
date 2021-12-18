using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    [SerializeField] private float screenWidthInUnits = 16f;
    [SerializeField] private float xMin = 0f;
    [SerializeField] private float xMax = 16f;
    private void Update()
    {
        var mousePosition = (Input.mousePosition / Screen.width) * screenWidthInUnits;
        var xMousePosition = Mathf.Clamp(mousePosition.x, xMin, xMax);
        
        //Debug.Log(mousePosition.x);
        Vector2 paddlePosition = new Vector2(xMousePosition, transform.position.y);
        transform.position = paddlePosition;
    }
}
