using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanTesteGPT : MonoBehaviour
{
    public float panSpeed = 10f;
    public float sensibility = -0.04f;

    void Update()
    {
        if (Input.GetMouseButton(1)) // Botão direito do mouse pressionado
        {
            PanCamera();
        }
    }

    void PanCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibility;
        float mouseY = Input.GetAxis("Mouse Y") * sensibility;

        Vector3 panDirection = new Vector3(mouseX, mouseY, 0f) * panSpeed * Time.deltaTime;
        transform.Translate(panDirection, Space.Self);
    }
}
