using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinchZoomPan : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    public float zoomOutMin = 0.01f;
    public float zoomOutMax = 0.25f;

    public Text labelCamera;
    public Text toutchStartLabel;
    public Text toutchEndLabel;
    public Text midTouch;
    

    [Header("Infos para Pan")]
    public Camera _camera;
    public Transform _target;
    private Vector3 touchStart;
    private Vector3 touchEnd;

    private Vector2 touchMidPoint;
    private Vector2 touchMidPointZero;


    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);


            // When you touch the screen
            if (touchZero.phase == TouchPhase.Began && touchOne.phase == TouchPhase.Began)
            {
                 Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                 Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                 labelCamera.text = Camera.main.orthographicSize.ToString();

                  // Find mid point between two touches
               // touchMidPoint = (touchZero.position + touchOne.position) / 2;
                touchMidPointZero = (touchZero.position + touchOne.position) / 2;
                touchStart = _camera.ScreenToWorldPoint(touchMidPointZero);
                
               // _target.transform.position = touchStart;
                

            }
            // When you move the finger
            if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved)
            {

            // para o pan
            // Find mid point between two touches
               touchMidPoint = (touchZero.position + touchOne.position) / 2;
               touchEnd = _camera.ScreenToWorldPoint(touchMidPoint);

              // midTouch.text = "Mid touch: " + touchMidPoint.ToString();
               
              
            // toutchStartLabel.text ="Start: " + touchZeroPrevPos.ToString();
            // toutchEndLabel.text = "End: "+ touchOnePrevPos.ToString();

               //Vector3 direction = new Vector3 (0.0f, 0.23f, 0.0f);
               Vector3 direction = touchEnd;
              _target.transform.position += direction * 0.001f;

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

             toutchStartLabel.text ="Start: " + touchZeroPrevPos.ToString();
             toutchEndLabel.text = "End: "+ touchOnePrevPos.ToString();

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Myzoom(deltaMagnitudeDiff * 0.001f);
            }

        }
    }

        void Myzoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
      //  GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 0.1f);
      //  labelCamera.text = Camera.main.orthographicSize.ToString();

    }

    public void PinchZoom_()
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
                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}
