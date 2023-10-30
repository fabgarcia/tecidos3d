using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomMouse : MonoBehaviour
{
    [SerializeField]
    private float ScroolSpeed = 10;
    private Camera Zoomcam;

     public float perspectiveZoomSpeed = 2.0f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f; 

    [Header("Mouse PC Zoom Cam")]
    public float zoom;
    public float zoomMultiplier = 4f;
    public  float minZoom = .2f;
    public float maxZoom = 2.80f;
    public float velocity = 0f;
    public float smoothTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        Zoomcam = Camera.main;
        zoom = GetComponent<Camera>().orthographicSize;
       // zoom = Camera.main;

    }

    public void MouseZoom()
    {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            zoom -= scroll * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            //if(!ehmobile.isMobile) 
            GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, zoom, ref velocity, smoothTime);
        
    }

    // Update is called once per frame
    void Update()
    {
         // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (GetComponent<Camera>().orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.0f, 79.9f);
            }
        }
        else
        Zoomcam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScroolSpeed;
        
        //MouseZoom();
    }
}
