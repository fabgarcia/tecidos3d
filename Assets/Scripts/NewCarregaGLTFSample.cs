using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using GLTFast.Loading;
using System.Threading.Tasks;
using TMPro;

public class NewCarregaGLTFSample : MonoBehaviour
{
public string url;
    public Slider progressSlider;
    public TextMeshProUGUI sliderValue;

    private void Start()
    {
        LoadFileURLProgress();
    }
    async void LoadFileURLProgress()
    {
        var gltf = new GLTFast.GltfImport();
        bool downloadComplete = false;

        // Inicialize a barra de progresso
        progressSlider.value = 0;

        var downloadTask = DownloadGLB(url, gltf);

        while (!downloadTask.IsCompleted)
        {
            // Atualize a barra de progresso com base no progresso da tarefa
            await Task.Yield(); // Aguarda um quadro para evitar bloqueio da thread principal
        }

        downloadComplete = downloadTask.Result;

        if (downloadComplete)
        {
            GameObject go = GameObject.Find("base");
            await gltf.InstantiateMainSceneAsync(go.transform);
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
            sliderValue.text = "Progress: "+ (int)(request.progress * 100)+ "%";
            progressSlider.value = (request.progress);
            // Aguarde o download ser concluído
            await Task.Yield();
        }

        if (string.IsNullOrEmpty(request.error))
        {
            // O download foi bem-sucedido, carregue o GLB
            sliderValue.text = "Progress:  100%";
            GameObject.Find("SliderModelProgress").SetActive(false);
            return await gltf.Load(request.bytes);
            
        }
        else
        {
            Debug.LogError("Failed to download GLB: " + request.error);
            return false;
        }
    }

}
