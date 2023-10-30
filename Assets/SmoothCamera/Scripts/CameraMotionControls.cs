using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ZoomMode {
    CameraFieldOfView,
    ZAxisDistance
}

public class CameraMotionControls : MonoBehaviour {

    [Header("Automatic Rotation")]
    [Tooltip("Toggles whether the camera will automatically rotate around it's target")]
    public bool autoRotate = true;
    [Tooltip("The speed at which the camera will auto-pan.")]
    public float rotationSpeed = 0.1f;
    [Tooltip("The rotation along the y-axis the camera will have at start.")]
    public float startRotation = 180;
    [Header("Manual Rotation")]
    [Tooltip("The smoothness coming to a stop of the camera afer the uses pans the camera and releases. Lower values result in significantly smoother results. This means the camera will take longer to stop rotating")]
    public float rotationSmoothing = 2f;
    [Tooltip("The object the camera will focus on.")]
    public Transform target;
    [Tooltip("How sensative the camera-panning is when the user pans -- the speed of panning.")]
    public float rotationSensitivity = 1f;
    [Tooltip("The min and max distance along the Y-axis the camera is allowed to move when panning.")]
    public Vector2 rotationLimit = new Vector2(5, 80);
    [Tooltip("The position along the Z-axis the camera game object is.")]
    public float zAxisDistance = 0.45f;
    [Header("Zooming")]
    [Tooltip("Whether the camera should zoom by adjusting it's FOV or by moving it closer/further along the z-axis")]
    public ZoomMode zoomMode = ZoomMode.CameraFieldOfView;
    [Tooltip("The minimum and maximum range the camera can zoon using the camera's FOV.")]
    public Vector2 cameraZoomRangeFOV = new Vector2(10, 60);
    [Tooltip("The minimum and maximum range the camera can zoon using the camera's z-axis position.")]
    public Vector2 cameraZoomRangeZAxis = new Vector2(10, 60);
    public float zoomSoothness = 10f;
    [Tooltip("How sensative the camera zooming is -- the speed of the zooming.")]
    public float zoomSensitivity = 2;

    new private Camera camera;
    private float cameraFieldOfView;
    new private Transform transform;
    private float xVelocity;
    private float yVelocity;
    private float xRotationAxis;
    private float yRotationAxis;
    private float zoomVelocity;
    private float zoomVelocityZAxis;

    //variaveis para o pinchZoom para Mobile - verificar
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    public Text texto;
    public Text labelCameraSize;

    public GameObject detectDvice;
    DisplayPCOrMobile ehmobile;

    [Header("Pan Cam")]
    [Tooltip("Velocidade e sensibilidade do Pan")]
    //public float panSpeed = 10f;
    //public float sensibility = -0.04f;
    Vector3 touchStart;
    public GameObject refCenterPan;
    Vector3 delta = new Vector3 (0.0f,0.0f,0.0f);


    [Header("Ortho PC Zoom Cam")]
    public float zoom;
    public float zoomMultiplier = 4f;
    public  float minZoom = .2f;
    public float maxZoom = 2.80f;
    public float velocity = 0f;
    public float smoothTime = 0.25f;

    [Header("Mobile Zoom Infos")]
    public float zoomOutMin = 0.015f;
    public float zoomOutMax = 0.35f;

    // PanCam Mobile
    private Vector3 touchEnd;
    private Vector2 touchMidPoint;

    //DebugText
    public Text debugZoom;
    public Text debugUpdateRot,ToqueInicial;

    public Text debugRotTarget;
    public Text debugTransTarget;
    

    private void Awake () {
        camera = GetComponent<Camera>();
        transform = GetComponent<Transform>();
    }

    private void Start () {
        cameraFieldOfView = camera.fieldOfView;
        zoom = camera.orthographicSize;
        //Sets the camera's rotation along the y axis.
        //The reason we're dividing by rotationSpeed is because we'll be multiplying by rotationSpeed in LateUpdate.
        //So we're just accouinting for that at start.
        xRotationAxis = startRotation / rotationSpeed;
        ehmobile = detectDvice.GetComponent<DisplayPCOrMobile>();


    }

    private void Update () {

        // Rotação é via LateUpdate
        // aqui trata Pan e Zoom         
        if(ehmobile.isMobile) 
        {
            texto.text = "MOBILE";
        // ZoomMobile();
         //PanCamMobile();
         
        }
        
        else
        {
            texto.text = "Aplicação PC";
         //   ZoomPC(); 
            // Botão direito do mouse pressionado
            PanCamPC();
        } 
        
    }

    
    private void LateUpdate () 

    { // Para rotação tanto PC qto mobile
        //If auto rotation is enabled, just increment the xVelocity value by the rotationSensitivity.
        //As that value's tied to the camera's rotation, it'll rotate automatically.
    //    if (autoRotate) {
    //        xVelocity += rotationSensitivity * Time.deltaTime;
    //    }

        if (target) 
        {
            Quaternion rotation;
            Vector3 position;
            Vector3 point;
            float deltaTime = Time.deltaTime;


            if (Input.touchCount != 2)
            {
                //We only really want to capture the position of the cursor when the screen when the user is holding down left click/touching the screen
                //That's why we're checking for that before campturing the mouse/finger position.
                //Otherwise, on a computer, the camera would move whenever the cursor moves. 
                
                //if mobile 
                if(ehmobile.isMobile)
                {
                    if (Input.GetMouseButton(0) && Input.GetTouch(0).phase == TouchPhase.Moved) {
                        xVelocity += Input.GetAxis("Mouse X") * rotationSensitivity;
                        yVelocity -= Input.GetAxis("Mouse Y") * rotationSensitivity;
                        debugUpdateRot.text = "ta passando aqui = MOB";

                    } else debugUpdateRot.text = "sem tocar"; 
                }
                else
                {
                //if desktop
                    if (Input.GetMouseButton(0)) {
                        xVelocity += Input.GetAxis("Mouse X") * rotationSensitivity;
                        yVelocity -= Input.GetAxis("Mouse Y") * rotationSensitivity;
                        debugUpdateRot.text = "ta passando aqui = PC";
                    } 
                }    

            }
                
                xRotationAxis += xVelocity;
                yRotationAxis += yVelocity;

                //Clamp the rotation along the y-axis between the limits we set. 
                //Limits of 360 or -360 on any axis will allow the camera to rotate unrestricted
                yRotationAxis = ClampAngleBetweenMinAndMax(yRotationAxis, rotationLimit.x, rotationLimit.y);

                rotation = Quaternion.Euler(yRotationAxis, xRotationAxis * rotationSpeed, 0);
                position = rotation * new Vector3(0f, 0f, -zAxisDistance) + target.position;


                transform.rotation = rotation;
                transform.position = position;
                

//                Debug.Log("Targer rot: " + transform.rotation);
//                Debug.Log("Targer trans: " + transform.position);
                

                xVelocity = Mathf.Lerp(xVelocity, 0, deltaTime * rotationSmoothing);
                yVelocity = Mathf.Lerp(yVelocity, 0, deltaTime * rotationSmoothing);
                
            
        } 

    } 
   
    private void ZoomPC() 
    {
        
        debugZoom.text = "Passou no ZoomPC";

        float deltaTime = Time.deltaTime;
                if (Input.touchCount != 2)
        {
        /*If the user's on a touch screen device like:
        an Android iOS or Windows phone/tablet, we'll detect if there are two fingers touching the screen.
        If the touches are moving closer together from where they began, we zoom out.
        If the touches are moving further apart, then we zoom in.*/
          //Zooms the camera in using the mouse scroll wheel
          //Ortho Zoom PC
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            zoom -= scroll * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            if(!ehmobile.isMobile) camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, zoom, ref velocity, smoothTime);
            labelCameraSize.text = camera.orthographicSize.ToString();
        }  

        }
        
    }


    //Prevents the camera from locking after rotating a certain amount if the rotation limits are set to 360 degrees.
    private float ClampAngleBetweenMinAndMax (float angle, float min, float max) {
        if (angle < -360) {
            angle += 360;
        }
        if (angle > 360) {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }


    public void PanCamPC()
    {
        if (Input.touchCount != 2)
        {
             if(Input.GetMouseButtonDown(1)){
            touchStart = camera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("BTN Direito");
        }
        
        if(Input.GetMouseButton(1)){

            Vector3 direction = touchStart - camera.ScreenToWorldPoint(Input.mousePosition);
            target.transform.position += direction;
            refCenterPan.SetActive(true);
        } else refCenterPan.SetActive(false);

        }
       
	}

    

    public void ZoomMobile()
    {
  
        if(Input.touchCount == 2){
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            //float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;

            //float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;
            
            //float difference = currentMagnitude - prevMagnitude;
            float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

            Myzoom(deltaMagnitudeDiff * -0.001f);

        }      

    }
        void Myzoom(float increment){
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
      //  labelCameraSize.text = Camera.main.orthographicSize.ToString();
        //camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, zoom, ref velocity, smoothTime);
    }

    public void ZoomMobile2()
    {
            if (Input.touchCount == 2) 
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
                float touchDeltaMag = (touch0.position - touch1.position).magnitude;
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                Myzoom(deltaMagnitudeDiff * 0.001f);

                //camera.fieldOfView = cameraFieldOfView;
                //cameraFieldOfView += deltaMagnitudeDiff * zoomSensitivity;
                //cameraFieldOfView = Mathf.Clamp(cameraFieldOfView, cameraZoomRangeFOV.x, cameraZoomRangeFOV.y);
            }
    }


}
