using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class RestApi : MonoBehaviour
{
    public string URL = "";
   // public string link = "https://cdn.glitch.global/217dd616-e9a6-4ba7-a405-9acee339d3f6/vanGogOficial.png";

    public Slider slider;

    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ExpText;
     public TextMeshProUGUI TextoSaida;

    public int index;
    private bool urlok=false;

    public Image image;
    private string imageString;

    // Start is called before the first frame update
    void Start()
    {
        
       // 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LerFromFile()
    {
        StartCoroutine(GetDatas());
    }

    IEnumerator GetDatas()
    {
        using(UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();
            

            if(request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
                Debug.Log(stats[index][1]);

                TextoSaida.text = stats[index][1].ToString();

                LevelText.text = "URL: " + stats[index]["fullUrl"];
                imageString = stats[index]["fullUrl"];
              //  Debug.Log(imageString);
                

              //  StartCoroutine(LoadImage(imageString));

            }

        }
    }

    public IEnumerator LoadImage(string link)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(link);
        
        request.SendWebRequest();

                    while(!request.isDone)
            {
                Debug.Log("Progress: "+ (int)(request.downloadProgress * 100)+ "%");
                ExpText.text = "Progress: "+ (int)(request.downloadProgress * 100)+ "%";
                Debug.Log((request.downloadProgress));

                slider.value = (request.downloadProgress);
                yield return null;
            }

        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Sucesso!!");
            Texture2D myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite newSprite = Sprite.Create(myTexture, new Rect(0,0,myTexture.width, myTexture.height), new Vector2(0.5f,0.5f));

            image.sprite = newSprite;
        }
    }







}
