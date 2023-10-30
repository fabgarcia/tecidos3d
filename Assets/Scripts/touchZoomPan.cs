using UnityEngine;

public class touchZoomPan : MonoBehaviour
{
    public float panSpeed = 0.1f;
    public float zoomSpeed = 0.5f;
    public float minZoom = 0.1f;
    public float maxZoom = 2f;

    private Vector2 lastTouchDelta;
    private Vector3 lastPanPosition;

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
    }

    void PanCamera(Vector3 newPanPosition)
    {
        transform.Translate(lastPanPosition - newPanPosition, Space.World);
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset)
    {
        float newSize = Camera.main.orthographicSize + offset;
        Camera.main.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
    }
}