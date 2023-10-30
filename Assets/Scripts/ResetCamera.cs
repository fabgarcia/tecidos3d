using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    public GameObject _cam;
    public Slider _slider;

    private Quaternion rotationCam;
    private Vector3 positionCam;

    // Start is called before the first frame update
    void Start()
    {
        rotationCam = _cam.transform.rotation;
        positionCam = _cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetCam()
    {
        _camera.orthographicSize = 0.25f ;
        _cam.transform.position = new Vector3(0f,0.2f, 0f);
        _cam.transform.rotation = Quaternion.Euler(0,180,0);
       _slider.value = 1;
    }
}
