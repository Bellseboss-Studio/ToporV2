using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class CameraWidthFit : MonoBehaviour
{
    public float targetWidth = 10f; // ancho del gameplay en unidades

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        AdjustCamera();
    }

    void Update()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float targetAspect = targetWidth / (targetWidth / screenAspect);

        cam.orthographicSize = targetWidth / (2f * screenAspect);
    }
}