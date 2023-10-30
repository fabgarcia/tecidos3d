using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventarioController : MonoBehaviour
{
    [Header("Item do Painel de Configuração")]
    [Tooltip("Inserir os items do painel de configuração dos tecidos")]
    [SerializeField] private Slider _sliderESC, _sliderOFFX, _sliderOFFY;
	[SerializeField] private TextMeshProUGUI _sliderText, debugtext;
    public static float escalaTile;
    
    [Header("Texturas & Thumbs")]
    [Tooltip("Texturas e imagens para os thumbnails dos tecidos")]
    public Object [] texturasTecidos, texturasThumbs;
    public string texPath = "Texture/outros";
    public string spritePath = "Texture/thumbs";

    public GameObject scroolViewContent;
    public GameObject btnTemplate;

    [Header("Mesh que chega do objetos atual em uso - é dinamico")]
    [Tooltip("Mesh que chega do objetos atual em uso - é dinamico")]
    public GameObject referencia_Mesh;
    public GameObject referencia_tecido;

    float offsetx, offsety;
	float scaleX,scaleY;

    public GiraBase _girabase;
    private int index=0; 

    // Start is called before the first frame update
    void Start()
    {
        MudaEscala();
		OffsetX();
		OffsetY();
        texturasThumbs = Resources.LoadAll(spritePath, typeof(Sprite)); 
        texturasTecidos = Resources.LoadAll(texPath, typeof(Texture)); 
        populatePanel();
    }

    // Update is called once per frame
    void Update()
    {
        if(GltfLoader.modelOk)
        {
            TrocaTexturaIndice(index);
            //Debug.Log("Escala que chegou: "+ GltfLoader.escalaMatAtivo);
        }
    }

    public void populatePanel()
    {
        for(int i = 0; i < texturasThumbs.Length; i++)
        {
            GameObject btn = (GameObject)Instantiate(btnTemplate);
            btn.transform.SetParent(scroolViewContent.transform);
            btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            btn.GetComponent<Image>().sprite = (Sprite)texturasThumbs[i];
            // Configura o texto do botão para mostrar o índice
            // Adiciona um evento de clique ao botão para mostrar o índice quando clicado
            int indice = i; // Captura o índice atual para o evento
            btn.GetComponent<Button>().onClick.AddListener(() => TrocaTexturaIndice(indice));
            btn.GetComponent<Button>().onClick.AddListener(() => MostrarIndiceDoBotao(indice));
           
        }
    }

    public void TrocaTexturaIndice(int indice)
	{
        if(GltfLoader.modelOk)
        {
        referencia_Mesh = GltfLoader.ModelMesh;
		referencia_Mesh.GetComponent<Renderer>().material.mainTexture = (Texture)texturasTecidos[indice];
		referencia_tecido.GetComponent<Image>().sprite = (Sprite)texturasThumbs[indice];
        GltfLoader.materialAtivo = referencia_Mesh.GetComponent<Renderer>().material;
        Debug.Log("passou na carga");
        index = indice;
        }

	}

    void MostrarIndiceDoBotao(int indice)
    {
        Debug.Log("Botão clicado. Índice: " + indice);
        if(!GltfLoader.modelOk)
        {
        //
        }
    }

    public void MudaEscalaSync (float value)
	{
        referencia_Mesh = GltfLoader.ModelMesh;
      // if(GltfLoader.modelOk)
       {
       Debug.Log(value);
	   value = value * escalaTile;
       if(referencia_Mesh == null) referencia_Mesh = GltfLoader.ModelMesh;
	   referencia_Mesh.GetComponent<Renderer>().material.mainTextureScale = new Vector2(value, value);
       }

	}


	public void OffsetX()
	{
        referencia_Mesh = GltfLoader.ModelMesh;
       //  if(GltfLoader.modelOk)
         {
            _sliderOFFX.onValueChanged.AddListener((v) => {
        //  _sliderText.text = v.ToString("0.00");
            offsetx = v;
            referencia_Mesh.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetx, offsety);
            }); 
         }
	}

	public void OffsetY()
	{
            referencia_Mesh = GltfLoader.ModelMesh;
      //   if(GltfLoader.modelOk)
         {
            _sliderOFFY.onValueChanged.AddListener((v) => {
        //  _sliderText.text = v.ToString("0.00");
            offsety = v;
            if(referencia_Mesh == null) referencia_Mesh = GltfLoader.ModelMesh;
            referencia_Mesh.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetx,offsety);
            }); 
         }
	}

    public void MudaEscala()
	{
        
      //  if(GltfLoader.modelOk)
        {
            referencia_Mesh = GltfLoader.ModelMesh;
            _sliderESC.onValueChanged.AddListener((v) => {
            _sliderText.text = (10*v).ToString("0") + "%";
            scaleX = v*escalaTile;
            scaleY = v*escalaTile;
            //debugtext.text = scaleX.ToString();
            referencia_Mesh.GetComponent<Renderer>().material.mainTextureScale = new Vector2(scaleX, scaleY);
            });        
        } 
      //  else Debug.Log("modelOK chegou false");
	}

    public void GiraToggle (bool tog)
	{
		Debug.Log(tog);
		_girabase.enabled = !_girabase.enabled; 
		
	}
}
