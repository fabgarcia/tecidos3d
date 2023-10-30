using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchDetect : MonoBehaviour
{
    public float Speed;
    public Transform target;
    public GameObject centerIcon;

    public Text debugRotyCamera;
    private float angleY;
    private int anglefator;

    // Start is called before the first frame update
    // ajustar pan lateral
    void Start()
    {
        
    }
    
        void Update()
    {
         angleY = Camera.main.transform.localEulerAngles.y;
         debugRotyCamera.text = angleY.ToString();

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            
            if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved)
            {
                Vector3 delPosition = (Vector3)touchZero.deltaPosition*Speed;
                //if((angleY>=90) && (angleY <= 270)) anglefator = -1;
                if((angleY>270) && (angleY < 90)) anglefator = 1;

                delPosition.x = anglefator*delPosition.x;
                delPosition.y = 1*delPosition.y;
                delPosition.z = 1*delPosition.z;
                
                target.transform.position += delPosition;
              //  centerIcon.SetActive(true);
            } 

            if (touchZero.phase == TouchPhase.Ended && touchOne.phase == TouchPhase.Ended)
            {
             // centerIcon.SetActive(false);
            }

        }



    }
   
}

