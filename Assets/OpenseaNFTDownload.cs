using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OpenseaNFTDownload : MonoBehaviour
{
    public string openseaLink;

    [System.Serializable]
    public class NFT
    {
        public string image;
        public string name;
        public string description;
        public string external_link;
        public string animation_url;
        public Traits traits;
    }

    [System.Serializable]
    public class Traits
    {
        public string trait_type;
        public string value;
        public int trait_count;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest(openseaLink.Replace("https://opensea.io/assets/matic/", "https://api.opensea.io/api/v2/metadata/matic/"))); 
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            NFT result = JsonUtility.FromJson<NFT>(webRequest.downloadHandler.text);
            if(result.image != null)
            {
                UnityWebRequest nftImage = UnityWebRequestTexture.GetTexture(result.image);
                yield return nftImage.SendWebRequest();
                if (nftImage.result != UnityWebRequest.Result.Success) 
                {
                    Debug.Log(nftImage.error);
                }
                else 
                {
                    Texture2D tex = ((DownloadHandlerTexture)nftImage.downloadHandler).texture;
                    GetComponent<Renderer>().material.mainTexture = tex;
                    float aspectRatio = (float)tex.height / (float)tex.width;
                    transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1,1,aspectRatio));
                }
            }
        }
    }
}
