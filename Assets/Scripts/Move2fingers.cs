using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2fingers : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    private Vector3 touchStart;
    private Vector3 touchEnd;
    private Vector2 touchMidPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

        // Update is called once per frame
    void Update()
    {
        CameraPan();
    }

    // Update is called once per frame
    private void CameraPan()
    {
        // Pan with two touches
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
 
            // When you touch the screen
            if (touchZero.phase == TouchPhase.Began && touchOne.phase == TouchPhase.Began)
            {
                // Find mid point between two touches
                touchMidPoint = (touchZero.position + touchOne.position) / 2;
                // Get initial touch world position
               /// touchStart = GetWorldPosition(touchMidPoint);
                touchStart = _camera.ScreenToWorldPoint(touchMidPoint);
                
            }
 
            // When you move the finger
            if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved)
            {
                
                // Find mid point between two touches
                touchMidPoint = (touchZero.position + touchOne.position) / 2;
                // Get touch end position
                ///touchEnd = GetWorldPosition(touchMidPoint);
                touchEnd = _camera.ScreenToWorldPoint(touchMidPoint);
               /*
                // Calculate touch vector direction
                Vector3 touchDirection = touchStart - touchEnd;
                // Pan by moving camera rig
                cameraRig.position += touchDirection;
                // Limit pan to screen bounds
              ///  Vector3 boundPosition = new Vector3(Mathf.Clamp(cameraRig.position.x, -screenBounds.x, screenBounds.x), 0, Mathf.Clamp(cameraRig.position.z, screenBounds.z, -screenBounds.z));
              ///  cameraRig.position = boundPosition;
               */
              Vector3 point = new Vector3 (0.0f, 0.25f, 0.0f);
              _target.transform.position += point * 0.01f;

              //Vector3 direction = touchStart - touchEnd;
              //_target.transform.position += direction;
                
            }
        }
    }

        // Get World Position
    private Vector3 GetWorldPosition(Vector3 touchPosition)
    {
        // Creates a ray from touch position
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        // Creates an intersection plane at 0,0,0
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        // Distance along the ray
        float distance;
        // Find the distance
        ground.Raycast(ray, out distance);
        //Return intersection point as Vector3
        return ray.GetPoint(distance);
    }
        
}
