using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    void Start()
    {
        string url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

        StartCoroutine(FetchJSON(url, (data) =>
        {
            data.Sort(new DataComparer());
            foreach (Data d in data) JengaBuilder.Instance.CreateBlock(d);
        })); 
    }

    public IEnumerator FetchJSON(string url, Action<List<Data>> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            yield break;
        }

        string jsonResponse = request.downloadHandler.text;
        List<Data> data = JsonConvert.DeserializeObject<List<Data>>(jsonResponse);

        callback(data);
    }
}
