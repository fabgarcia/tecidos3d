using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkybox : MonoBehaviour
{
    public Material[] skyboxMat;
     public Sprite[] btnSprites;
     public Image targetButton;

    // Start is called before the first frame update
    void Start()
    {
     //
    }

    public void ChangeSkyboxes(int i)
    {
        if(targetButton.sprite == btnSprites[0])
        {
            targetButton.sprite = btnSprites[1];
            RenderSettings.skybox = skyboxMat[i];
            return;
        }
        targetButton.sprite = btnSprites[0];
        RenderSettings.skybox = skyboxMat[0];
    }

    public void showSkybox (int i)
    {
        RenderSettings.skybox = skyboxMat[i];

    }
}
