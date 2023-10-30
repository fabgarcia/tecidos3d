using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectivePan : MonoBehaviour {
    private Vector3 touchStart;
    public Camera cam;
    public float groundZ = 0;

    void Start()
    {
        touchStart = GetWorldPosition(groundZ);
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1)){
            touchStart = GetWorldPosition(groundZ);
        }
        if (Input.GetMouseButton(1)){
            Vector3 direction = touchStart - GetWorldPosition(groundZ);
            cam.transform.position += direction;
            Debug.Log("botão direito");
            GameObject.Find("Main Camera").GetComponent<CameraMotionControls>().enabled = false;
        } else GameObject.Find("Main Camera").GetComponent<CameraMotionControls>().enabled = true;
    }
    private Vector3 GetWorldPosition(float z){
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0,0,z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
}