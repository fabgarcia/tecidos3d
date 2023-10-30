using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GLTFast.Loading;
using TMPro;
using UnityEngine.Networking;
using System.Threading.Tasks;

[System.Serializable]
public class MaterialGameObjectInfo
{
    public Material material;
    public GameObject gameObject;
}

public class GltfLoader : MonoBehaviour
{
    public string[] url;

    // https://cdn.glitch.global/73b3024a-4c58-4015-ac8c-b80b3e39c008/Bola_base_mesh.glb
    //https://cdn.glitch.global/73b3024a-4c58-4015-ac8c-b80b3e39c008/myshirt.glb
    //// https://cdn.glitch.global/217dd616-e9a6-4ba7-a405-9acee339d3f6/ShapeDress.glb
 
    [SerializeField] 
    //private List<MaterialGameObjectInfo> listaMateriaisGameObject = new List<MaterialGameObjectInfo>();

    public Transform objetoTransf;

    public static GameObject ModelMesh;
    public static bool modelOk = false;
    public static Material materialAtivo;
    
    [SerializeField] 
    private Material materialTecidoEmUso;

    public GLTFast.GltfAsset gltf;

    public Renderer[] osRendersAtivos;
    public GameObject[] osFilhosAtivos;
    public float[] escalaAtivos;
    private int indexAtivo;

    public Slider _sliderEscala;

    // slider components
    public Slider progressSlider;
    public TextMeshProUGUI sliderValue;
    public GameObject sliderObjeto;
    

    // Start is called before the first frame update
   void Awake()
   {
    sliderObjeto.SetActive(false);
   }
   void Start()
   {
     indexAtivo = 0;
   }
   
   async public void CarregaModelo(int index)
   {

            var success = await gltf.Load(url[index]);
            Debug.Log("Carregando ...");
            if (success) 
            {
                modelOk = true;
                Debug.Log("Carregou!!!!!!!!!!!!!!! ...");
                Debug.Log(modelOk);
                GameObject go = GameObject.Find("base");
                osRendersAtivos[indexAtivo] = go.transform.GetChild(go.transform.childCount-1).gameObject.GetComponentInChildren<Renderer>();
                osFilhosAtivos[indexAtivo] = go.transform.GetChild(go.transform.childCount-1).gameObject;
                ModelMesh = osRendersAtivos[indexAtivo].gameObject;   
                Debug.Log(ModelMesh.GetComponent<Renderer>().material.mainTextureScale);    
                Vector2 f = ModelMesh.GetComponent<Renderer>().material.mainTextureScale;
                Debug.Log(f.x);
                InventarioController.escalaTile = ((float)f.x/10);
                escalaAtivos[indexAtivo] = ((float)f.x/10);
            }

            else Debug.Log("Erro de carga!!");
        
   }

   public void DefineAtivo(int index)
   {
        indexAtivo = index;
      //  if(osRendersAtivos[index] == null)  CarregaModelo(index);
        if(osRendersAtivos[index] == null)  CarregaModeloProgress(index);
        
        for(int i=0; i<osFilhosAtivos.Length; i++)
        {
            if(osFilhosAtivos[i] !=null)
            {
               
                if (index == i) 
                {
                    osFilhosAtivos[i].SetActive(true);
                    ModelMesh = osRendersAtivos[i].gameObject;
                    InventarioController.escalaTile = escalaAtivos[i];
                    ModelMesh.GetComponent<Renderer>().material = materialAtivo;
                    ModelMesh.GetComponent<Renderer>().material.mainTextureScale = new Vector2(escalaAtivos[i]*_sliderEscala.value, escalaAtivos[i]*_sliderEscala.value);
                    Debug.Log("Define ativo valor da slider escala: " + _sliderEscala.value);

                    
                }
               
                else
                {
                    osFilhosAtivos[i].SetActive(false);
                } 
                
            } 
                     
        }
        
   }

    async void CarregaModeloProgress(int index)
    {
        var gltf2 = new GLTFast.GltfImport();
        bool downloadComplete = false;

        // Inicialize a barra de progresso
        progressSlider.value = 0;
        sliderObjeto.SetActive(true);
        var downloadTask = DownloadGLB(url[index], gltf2);

        while (!downloadTask.IsCompleted)
        {
            // Atualize a barra de progresso com base no progresso da tarefa
            await Task.Yield(); // Aguarda um quadro para evitar bloqueio da thread principal
        }

        downloadComplete = downloadTask.Result;

        if (downloadComplete)
        {
                //modelOk = true;
                Debug.Log("Carregou!!!!!!!!!!!!!!! ...");
                Debug.Log(modelOk);
                GameObject go = GameObject.Find("base");
                await gltf2.InstantiateMainSceneAsync(go.transform);

                osRendersAtivos[indexAtivo] = go.transform.GetChild(go.transform.childCount-1).gameObject.GetComponentInChildren<Renderer>();
                osFilhosAtivos[indexAtivo] = go.transform.GetChild(go.transform.childCount-1).gameObject;
                ModelMesh = osRendersAtivos[indexAtivo].gameObject;   
                Debug.Log(ModelMesh.GetComponent<Renderer>().material.mainTextureScale);    
                Vector2 f = ModelMesh.GetComponent<Renderer>().material.mainTextureScale;
                Debug.Log(f.x);
                InventarioController.escalaTile = ((float)f.x/10);
                escalaAtivos[indexAtivo] = ((float)f.x/10); 
                StartCoroutine(CompletouCargaModelo());
            
        }
        else
        {
            Debug.LogError("Loading glTF failed!");
        }
    }

    private async Task<bool> DownloadGLB(string url, GLTFast.GltfImport gltf)
    {
        var request = new WWW(url);
        while (!request.isDone)
        {
            Debug.Log("Progress: "+ (int)(request.progress * 100)+ "%");
            sliderValue.text = "Carregando . . . "+ (int)(request.progress * 100)+ "%";
            progressSlider.value = (request.progress);
            // Aguarde o download ser concluído
            await Task.Yield();
        }

        if (string.IsNullOrEmpty(request.error))
        {
            // O download foi bem-sucedido, carregue o GLB
            sliderValue.text = "Carregado!  100%";
            GameObject.Find("SliderModelProgress").SetActive(false);
            
            return await gltf.Load(request.bytes);
            
        }
        else
        {
            Debug.LogError("Failed to download GLB: " + request.error);
            return false;
        }
    }

    IEnumerator CompletouCargaModelo()
    {
         yield return new WaitForSeconds(0.5f);
         modelOk = true;
    }

/*
    public void ListarAtivo(Transform ativoTransform)
    {
        Renderer render = ativoTransform.GetComponent<Renderer>();
        if (render != null)
        {
            // Adiciona o material e o GameObject à lista
            foreach (Material material in render.materials)
            {
                MaterialGameObjectInfo info_ativo = new MaterialGameObjectInfo
                {
                    material = material,
                    gameObject = ativoTransform.gameObject,
                };
                listaMateriaisGameObject.Add(info_ativo);
               
            }
        }

        foreach (Transform child in ativoTransform)
        {
            ListarAtivo(child);
        }
    }



    void ListarMateriaisRecursivamente(Transform objetoTransform)
    {
        Renderer renderizador = objetoTransform.GetComponent<Renderer>();

        if (renderizador != null)
        {
            // Adiciona o material e o GameObject à lista
            foreach (Material material in renderizador.materials)
            {
                MaterialGameObjectInfo info = new MaterialGameObjectInfo
                {
                    material = material,
                    gameObject = objetoTransform.gameObject,
                };
                listaMateriaisGameObject.Add(info);
               
            }
        }

        foreach (Transform filho in objetoTransform)
        {
            ListarMateriaisRecursivamente(filho);
        }
        
    }

    public void MostraGameObject()
    {
        // Acesse o GameObject do primeiro item
        GameObject primeiroGameObject = listaMateriaisGameObject[0].gameObject;
        GameObject go = GameObject.Find("base");
        Debug.Log(go.name + " has " + go.transform.childCount + " children");
        for(int i=0;i<go.transform.childCount;i++)
        {
            Debug.Log("O nome do filho " + (i+1) + " é: " + go.transform.GetChild(i).gameObject.name);
        }
    }
    */

}
