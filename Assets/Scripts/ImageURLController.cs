using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;

public class ImageURLController : MonoBehaviour
{
    // Start is called before the first frame update
    public string uri = "https://tecidos3d.com.br/images/";
   // public string uri = "http://www.flowservelive.com/models/";
    void Start()
    {
        test();
        /*
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info) 
        {
            string[] lines = File.ReadAllLines (f.FullName);
            foreach (string line in lines) {
 
                if (line.Contains ("001")) {
 
                    Debug.Log ("Link: " + f.FullName);
                    break;
                }
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void test()
    {
        
        WebRequest request = WebRequest.Create(uri);
        WebResponse response = request.GetResponse();
        Regex regex = new Regex("<a href=\".*\">(?<name>.*)</a>");
        Debug.Log("here");
        using(var reader = new StreamReader(response.GetResponseStream()))
        {
            string result = reader.ReadToEnd();
            Debug.Log(result);
            MatchCollection matches = regex.Matches(result);
            if(matches.Count == 0)
            {
                Debug.Log("parse failed.");
                return;
            }
 
            foreach(Match match in matches)
            {
                if(!match.Success) { continue; }
                Debug.Log(match.Groups["name"]);
            }
        }
    }
}
