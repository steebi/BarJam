using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RequestHandler
{
    // to be used for running requests on a thread.
    private RequestBehaviour _requestBehaviour;
    private GameObject _requestBehaviourGO;

    public RequestHandler()
    {
        this._requestBehaviourGO = new GameObject();
        this._requestBehaviour = this._requestBehaviourGO.AddComponent<RequestBehaviour>();
    }

    public void MakeRequest(object requestObject, string endPoint, string value, System.Action<bool, UnityWebRequest> callback)
    {
        this._requestBehaviour.MakeRequest(this.CreateRequestEnumerator(requestObject, endPoint, value, callback));
        
    }

    private IEnumerator CreateRequestEnumerator(object requestObject, string endPoint, string value, System.Action<bool, UnityWebRequest> callback)
    {
        string json = JsonUtility.ToJson(requestObject);
        UnityWebRequest request = UnityWebRequest.Put(endPoint + value, json);
        request.SetRequestHeader("x-api-key", Endpoints.X_API_KEY);
        request.method = "POST";
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.Send();
        callback(true, request);
    }
}

class RequestBehaviour : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void MakeRequest(IEnumerator requestEnumerator)
    {
        StartCoroutine(requestEnumerator);
    }
}

