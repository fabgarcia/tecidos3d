using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanMobileTouch : MonoBehaviour
{
    [SerializeField] private Transform cameraRig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
void Update()
{

    if (Input.touchCount == 2)
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        // Verifique se ambos os toques estão na tela
        if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved)
        {
            // Obtenha os deltas de posição dos dedos
            Vector2 touchZeroDelta = touchZero.deltaPosition;
            Vector2 touchOneDelta = touchOne.deltaPosition;

            // Calcule a média dos deltas de posição para obter o panning
            Vector2 pan = (touchZeroDelta + touchOneDelta) * 0.5f;

            // Aplique o panning à câmera
         //   Camera.main.transform.Translate(-pan.x, -pan.y, 0);
            cameraRig.transform.Translate(-pan.x, -pan.y, 0);
        }
    }
}

}
