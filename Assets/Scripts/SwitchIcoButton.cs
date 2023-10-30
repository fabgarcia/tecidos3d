using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchIcoButton : MonoBehaviour
{
    public GameObject Painel;
    public Sprite[] btnSprites;
    public Image targetButton;


    public void ChangeSprite()
    {
        if(targetButton.sprite == btnSprites[0])
        {
            targetButton.sprite = btnSprites[1];
            Painel.SetActive(false);
            return;
        }
        targetButton.sprite = btnSprites[0];
        Painel.SetActive(true);
    }


    
}
