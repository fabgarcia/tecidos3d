using UnityEngine;

public class touchZoomPan_limitandoPan : MonoBehaviour
{
    public float panSpeed = 0.1f;
    public float zoomSpeed = 0.5f;
    public float minZoom = 0.1f;
    public float maxZoom = 2f;
    public GameObject myMainObject; // Set this from the Unity Editor

    private Vector2 lastTouchDelta;
    private Vector3 lastPanPosition;
    private Vector3 offset;

    void Start()
    {
        // Calculate the initial offset between the camera and myMainObject
        offset = transform.position - myMainObject.transform.position;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            Vector2 touchDelta = touchZero.position - touchOne.position;
            Vector2 prevTouchDelta = touchZeroPrevPos - touchOnePrevPos;

            // Handle panning
            if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved)
            {
                Vector3 touchDeltaPosition = (touchZero.position + touchOne.position) / 2;
                PanCamera(touchDeltaPosition);
            }

            // Handle zooming
            float deltaMagnitudeDiff = prevTouchDelta.magnitude - touchDelta.magnitude;
            ZoomCamera(deltaMagnitudeDiff * zoomSpeed);
        }

        // Update the camera position based on myMainObject's position and the offset
        transform.position = myMainObject.transform.position + offset;
    }

    void PanCamera(Vector3 newPanPosition)
    {
        Vector3 delta = lastPanPosition - newPanPosition;
        transform.Translate(delta, Space.World);
        lastPanPosition = newPanPosition;

        // Update the offset based on the new camera position
        offset = transform.position - myMainObject.transform.position;
    }

    void ZoomCamera(float offset)
    {
        float newSize = Camera.main.orthographicSize + offset;
        Camera.main.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);

        // Optionally adjust the offset based on zoom level if necessary
        // For example, you might want to move the camera closer or further from myMainObject based on the zoom level
    }
}
