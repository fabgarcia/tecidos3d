using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using GLTFast;
using GLTFast.Schema;
using System.IO;
using System.Threading.Tasks;
using System;
using UnityEditor;

public class CarregaGLTFSample : MonoBehaviour
{
    public string url;
    public Slider progressSlider;

private void Start()
{
  
  LoadURL();
 // LoadGltfBinaryFromMemory() ;
}

async void LoadURL()
{
    var gltf = new GLTFast.GltfImport();
    var success = await gltf.Load(url);
    
    if (success) {
        GameObject go = GameObject.Find("base");
        await gltf.InstantiateMainSceneAsync(go.transform);
    }
    else {
        Debug.LogError("Loading glTF failed!");
    }
}

async void LoadGltfBinaryFromMemory() {
    var filePath = url;
    byte[] data = File.ReadAllBytes(filePath);
    var gltf = new GLTFast.GltfImport();
    bool success = await gltf.LoadGltfBinary(
        data, 
        // The URI of the original data is important for resolving relative URIs within the glTF
        new Uri(filePath)
        );
    if (success) {
        success = await gltf.InstantiateMainSceneAsync(transform);
    }
}

private void UpdateProgressBar(float progress)
    {
        if (progressSlider != null) {
            progressSlider.value = progress;
        }
    }


}
