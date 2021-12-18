using UnityEngine;

public class CanvasGetCam : MonoBehaviour
{
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCam();
    }

    void UpdateCam()
    {
        canvas = GetComponentInParent<Canvas>();
        canvas.worldCamera = Camera.main;
    }

    void Update()
    {
        if (canvas.worldCamera == null)
        {
            UpdateCam();
        }
    }
    
}
