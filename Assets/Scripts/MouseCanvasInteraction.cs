using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCanvasInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _camera;
    //public Texture2D handPointerCursor, handOpenCursor;

    // Start is called before the first frame update
    void Start()
    {
        //_camera = GameObject.GetComponent<CameraMotionControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      {
      //  Debug.Log("pointer entrou no painel");
        _camera.GetComponent<CameraMotionControls>().enabled = false;
     //   Cursor.SetCursor(handPointerCursor, Vector2.zero, CursorMode.ForceSoftware); 
      }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      {
    //    Debug.Log("pointer saiu do painel");
        _camera.GetComponent<CameraMotionControls>().enabled = true;
    //    Cursor.SetCursor(handOpenCursor, Vector2.zero, CursorMode.ForceSoftware); 
      }
    }
}
