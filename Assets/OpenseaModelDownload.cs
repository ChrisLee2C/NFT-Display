using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Siccity.GLTFUtility;
using System.IO;
using UnityEngine.UI;

public class OpenseaModelDownload: MonoBehaviour
{
    [SerializeField] private string openSeaUrl;
    [SerializeField] private Text text;
    private GameObject downloadModel;

    class Model
    {
        public string animation_url;
    }

    void Awake()
    {
        StartCoroutine(DownloadModel(openSeaUrl,
    (UnityWebRequest req) =>
            {
                if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log(req.error);
                }
                else
                {
                    Debug.Log(req.downloadHandler.text);
                    ResetModel();
                    Model model = JsonUtility.FromJson<Model>(req.downloadHandler.text);
                    StartCoroutine(LoadModel(model.animation_url));
                }
            }
        ));
    }

    IEnumerator LoadModel(string modelUrl) {
        UnityWebRequest loadModel = UnityWebRequest.Get(modelUrl);
        yield return loadModel.SendWebRequest();
        if (loadModel.result == UnityWebRequest.Result.ConnectionError || loadModel.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(loadModel.error);
        }
        else
        {
            byte[] downloadData = loadModel.downloadHandler.data;
            string filePath = Application.persistentDataPath + "/downloadModel.gltf";
            File.WriteAllBytes(filePath, downloadData);
            print(filePath);
            text.text = filePath;
            GameObject importModel = Importer.LoadFromFile(filePath);
            importModel.transform.SetParent(gameObject.transform);
        }
    }

    void ResetModel()
    {
        if(downloadModel != null)
        {
            foreach(Transform trans in downloadModel.transform)
            {
                Destroy(trans.gameObject);
            }
        }
    }

    IEnumerator DownloadModel(string url, Action<UnityWebRequest> callback)
    {
        //To ensure disposal and prevent overloading memory
        using (UnityWebRequest req = UnityWebRequest.Get(url.Replace("https://opensea.io/assets/matic/", "https://api.opensea.io/api/v2/metadata/matic/"))) 
        {
            yield return req.SendWebRequest();
            callback(req);
        }
    }

    /*{"image":"https://i.seadn.io/gcs/files/42d9aa903f4498dacacba212ea6216d9.png?w=500&auto=format",
     * "name":"Crazy Slot ",
     * "description":null,
     * "external_link":null,
     * "animation_url":"https://openseauserdata.com/files/1997dbe983570f6badc2efa160f3ab2e.gltf",
     * "traits":[]}
     */
}
