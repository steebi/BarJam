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

    public void MakeRequest(object requestObject, string endPoint, string value, System.Action<bool> callback)
    {
        this._requestBehaviour.MakeRequest(this.CreateRequestEnumerator(requestObject, endPoint, value, callback));
        
    }

    private IEnumerator CreateRequestEnumerator(object requestObject, string endPoint, string value, System.Action<bool> callback)
    {
        string json = JsonUtility.ToJson(requestObject);
        UnityWebRequest request = UnityWebRequest.Put(endPoint + value, json);
        request.method = "POST";
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.Send();
        // TODO: check if request is successful or not.
        callback(true);
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

