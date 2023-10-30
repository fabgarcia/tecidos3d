using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;


public class MaterialChanger : MonoBehaviour
{

	
	[SerializeField] private Slider _sliderESC, _sliderOFFX, _sliderOFFY;
	[SerializeField] private TextMeshProUGUI _sliderText, debugtext;
	public float escalaTile;
	private int texId;

	public GameObject referencia_Mesh;
	public GameObject referencia_tecido;

	public Object [] texDress;
	public Object [] tecidoDress;
	public string texPath = "Texture/Dress";
		
	public Color settingColorMat = Color.white;

	float offsetx, offsety;
	float scaleX,scaleY;

	public GiraBase _girabase;// _girabaseShirt;


	void Start () 
	{
        MudaEscala();
		OffsetX();
		OffsetY();
		texDress = Resources.LoadAll(texPath, typeof(Texture));
		tecidoDress = Resources.LoadAll(texPath, typeof(Sprite));
//		_girabase = GameObject.Find("BaseDress").GetComponent<GiraBase>();
//		_girabaseShirt = GameObject.Find("MeshShirt").GetComponent<GiraBase>();
        _girabase = _girabase.GetComponent<GiraBase>();
	}

	public void MudaEscala()
	{
		_sliderESC.onValueChanged.AddListener((v) => {
        _sliderText.text = (10*v).ToString("0") + "%";
		scaleX = v*escalaTile;
        scaleY = v*escalaTile;
		//debugtext.text = scaleX.ToString();
		referencia_Mesh.GetComponent<Renderer>().material.mainTextureScale = new Vector2(scaleX, scaleY);
        });
	}

    
	public void MudaEscalaSync (float value)
	{
       Debug.Log(value);
	   value = value * escalaTile;
	   referencia_Mesh.GetComponent<Renderer>().material.mainTextureScale = new Vector2(value, value);
	}


	public void OffsetX()
	{
		_sliderOFFX.onValueChanged.AddListener((v) => {
      //  _sliderText.text = v.ToString("0.00");
		offsetx = v;
		referencia_Mesh.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetx, offsety);
        }); 
	}

	public void OffsetY()
	{
		_sliderOFFY.onValueChanged.AddListener((v) => {
      //  _sliderText.text = v.ToString("0.00");
		offsety = v;
		referencia_Mesh.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetx,offsety);
        }); 
	}
	
    public void TrocaTextura()
	{
		if(texId < texDress.Length -1)
		{
			texId++;
		}
		else
		{
			texId = 0;
		}
		referencia_Mesh.GetComponent<Renderer>().material.SetTexture("_MainTex", (Texture)texDress[texId]);
	}


	public void TrocaTexturaIndice(int indice)
	{
		//dressObject.GetComponent<Renderer>().material.SetTexture("_MainTex", (Texture)texDress[indice]);
		referencia_Mesh.GetComponent<Renderer>().material.mainTexture = (Texture)texDress[indice];
		referencia_tecido.GetComponent<Image>().sprite = (Sprite)tecidoDress[indice];
		//referencia.GetComponent<Image>().material.SetTexture("_MainTex", (Texture)texDress[indice]);
	}

	public void GiraToggle (bool tog)
	{
		Debug.Log(tog);
		_girabase.enabled = !_girabase.enabled; 
		
	}
    public void GiraToggleShirt (bool tog)
	{
		Debug.Log(tog);
		//_girabaseShirt.enabled = !_girabaseShirt.enabled; 
		
	}


	void Update()
    {

    }
   

} // end of file
