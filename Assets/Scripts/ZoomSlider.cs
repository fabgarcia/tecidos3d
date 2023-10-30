using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZoomSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private Camera mycam;
    private float value; 

    public  float minZoom = 0.25f;
    public float maxZoom = 0.01f;

    //DisplayPCOrMobile ehmobile;
    public GameObject detectDvice;


    // Start is called before the first frame update
    void Start()
    {
        minZoom = mycam.orthographicSize;
        //ehmobile = detectDvice.GetComponent<DisplayPCOrMobile>();
        _slider.onValueChanged.AddListener((v) => {
    //  _sliderText.text = v.ToString("0.00");
           value = v;
                       // If the camera is orthographic...
            if (mycam.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                mycam.orthographicSize = minZoom - (v * (minZoom/_slider.maxValue)); 
                if(mycam.orthographicSize <= maxZoom) mycam.orthographicSize = maxZoom;
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                 mycam.fieldOfView = 60f - (v * 5.5f);
            }
           
        });
    }



}
