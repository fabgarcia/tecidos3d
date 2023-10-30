using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Texture2D handGrabCursor, handOpenCursor;
    
    // Start is called before the first frame update
    
    private void Awake() {

    }
    void Start()
    {

        Cursor.SetCursor(handOpenCursor, Vector2.zero, CursorMode.ForceSoftware); 
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetMouseButton(0) || Input.GetMouseButton(1) ){
            Cursor.SetCursor(handGrabCursor, Vector2.zero, CursorMode.ForceSoftware); 
        } else Cursor.SetCursor(handOpenCursor, Vector2.zero, CursorMode.ForceSoftware); 
    }
}
