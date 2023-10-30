using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using GLTFast;
using GLTFast.Schema;
using System.IO;
using System.Threading.Tasks;

public class LoadGLBWithProgress : MonoBehaviour
 {
    /*
    public string glbURL = "https://cdn.glitch.global/217dd616-e9a6-4ba7-a405-9acee339d3f6/ShapeDress.glb";
    public GameObject progressBar;
    public Text progressText;

    private void Start()
    {
        StartCoroutine(LoadGLBWithProgressCoroutine());
    }

    private IEnumerator LoadGLBWithProgressCoroutine()
    {
        // Baixar o GLB
        UnityWebRequest www = UnityWebRequest.Get(glbURL);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download GLB: " + www.error);
        }
        else
        {
            byte[] glbData = www.downloadHandler.data;

            // Inicialize a barra de progresso
            progressBar.SetActive(true);
            float progress = 0f;

            var settings = new ImportSettings();
           // settings.useStreamCache = false; // Pode ser configurado conforme sua necessidade
           // settings.bufferCacheSize = 100 * 1024 * 1024; // Pode ser configurado conforme sua necessidade
           // settings.maximumLod = 100; // Pode ser configurado conforme sua necessidade

            using (var stream = new System.IO.MemoryStream(glbData))
            {
                // Inicialize o carregador glTF
                var loader = new GlTFast.GlbLoader();

                // Associe a barra de progresso ao carregador
                loader.Progress = p =>
                {
                    progress = p;
                    progressText.text = $"{Mathf.FloorToInt(p * 100)}%";
                };

                // Carregue o GLB
                var importResult = loader.LoadFromStream(stream, settings);

                if (importResult != null)
                {
                    var root = importResult.ImportedObjects[0].root;

                    // O GLB foi carregado com sucesso, você pode acessar os objetos do GLB aqui

                    // Exemplo: Coloque o objeto na cena
                    Instantiate(root, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Failed to load GLB.");
                }
            }

            // Esconda a barra de progresso após o carregamento
            progressBar.SetActive(false);
        }
    }
    */
}



