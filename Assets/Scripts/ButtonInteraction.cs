using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator btnAnimator;
    public GameObject BtnAnimatorController;
    public GameObject glowfx;
    public bool check = false;

    void Start() 
    {
       btnAnimator = BtnAnimatorController.GetComponent<Animator>();   
       if(check)  BtnCliked();
    } 
    public void OnPointerEnter(PointerEventData eventData)
    {
      if(!check)
      {
     //   Debug.Log("pointer enter");
        btnAnimator.SetTrigger("cresce");
        StartCoroutine(DelayEnter());
        glowfx.SetActive(true);
      }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      if(!check)
      {
     //   Debug.Log("pointer exit");
        btnAnimator.SetTrigger("decresce");
        StartCoroutine(DelayExit());
        glowfx.SetActive(false);
      }
    }

    IEnumerator DelayEnter()
    {
      // wait seconds    
     // Debug.Log("Esperando segundos ....");
      yield return new WaitForSeconds(0.32f);
    //  Debug.Log("Acabou");
      btnAnimator.ResetTrigger("cresce");
    }

        IEnumerator DelayExit()
    {
      // wait seconds    
     // Debug.Log("Esperando segundos ....");
      yield return new WaitForSeconds(0.32f);
    //  Debug.Log("Acabou");
      btnAnimator.ResetTrigger("decresce");

    }

    public void BtnCliked()
    {
      if(GltfLoader.modelOk)
      {
        btnAnimator.SetTrigger("cresce");
        StartCoroutine(DelayEnter());
        glowfx.SetActive(true);
        check = true;
      }


    }

    public void Uncheck()
    {
      check = false;
    //  Debug.Log("pointer exit");
        btnAnimator.SetTrigger("decresce");
        StartCoroutine(DelayExit());
        glowfx.SetActive(false);
    }
}
