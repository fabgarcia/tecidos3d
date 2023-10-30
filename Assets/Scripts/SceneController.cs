using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [System.Serializable]
    public class estruturaModelos
    {
        public GameObject modelo3d;
        public GameObject painelTecido;
        public GameObject painelConfig;
        public GameObject painelRef;
    }

    public GameObject[] button3d;
    public estruturaModelos[] myEstrutura;



    private int count, countBTN;

    // Start is called before the first frame update
    void Start()
    {
        count = myEstrutura.Length;
        countBTN = button3d.Length;

//        Debug.Log(count);
//        Debug.Log(countBTN);

       // showOneHideOthers(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showItem(int i)
    {
        myEstrutura[i].modelo3d.SetActive(true);
        myEstrutura[i].painelTecido.SetActive(true);
        myEstrutura[i].painelConfig.SetActive(true);
        myEstrutura[i].painelRef.SetActive(true);
        button3d[i].GetComponent<ButtonInteractionMat>().BtnCliked();
    }

    public void showItemSemModelo(int i)
    {
       // myEstrutura[i].modelo3d.SetActive(true);
        myEstrutura[i].painelTecido.SetActive(true);
        myEstrutura[i].painelConfig.SetActive(true);
        myEstrutura[i].painelRef.SetActive(true);
        button3d[i].GetComponent<ButtonInteractionMat>().BtnCliked();
    }


    

    public void hideItem(int i)
    {
        myEstrutura[i].modelo3d.SetActive(false);
        myEstrutura[i].painelTecido.SetActive(false);
        myEstrutura[i].painelConfig.SetActive(false);
        myEstrutura[i].painelRef.SetActive(false);
        button3d[i].GetComponent<ButtonInteractionMat>().Uncheck();
    }

        public void hideItemSemLodelo(int i)
    {
        //myEstrutura[i].modelo3d.SetActive(false);
        myEstrutura[i].painelTecido.SetActive(false);
        myEstrutura[i].painelConfig.SetActive(false);
        myEstrutura[i].painelRef.SetActive(false);
        button3d[i].GetComponent<ButtonInteractionMat>().Uncheck();
    }

    public void showOneHideOthers(int i)
    {
     for(int j = 0 ; j < count; j++)
     {
        if (j == i) 
        {
            showItem (j);

        }
        
        else
        {
            hideItem (j);

        } 
        
     }
    }

    public void showOneHideOthersSemModelo(int i)
    {
     for(int j = 0 ; j < count; j++)
     {
        if (j == i) 
        {
            showItemSemModelo (j);

        }
        
        else
        {
            hideItemSemLodelo (j);

        } 
        
     }
    }

    public void selectOneUncheckAll(int i)
    {
      for(int j = 0 ; j < countBTN; j++)
      {
        if (j == i) button3d[j].GetComponent<ButtonInteractionMat>().BtnCliked();
        else button3d[j].GetComponent<ButtonInteractionMat>().Uncheck();
      }
    }

}
