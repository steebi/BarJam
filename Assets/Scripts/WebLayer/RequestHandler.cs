using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RequestHandler
{
    // to be used for running requests on a thread.
    private RequestBehaviour _requestBehviour;
    private GameObject _requestBehaviourGO;

    public RequestHandler()
    {
        this._requestBehaviourGO = new GameObject();
        this._requestBehaviourGO.AddComponent<RequestBehaviour>();
    }

    public void MakeRequest(object requestObject, string endPoint, string value)
    {
        string json = JsonUtility.ToJson(requestObject);
        UnityWebRequest request = UnityWebRequest.Put(endPoint + value, json);
        request.method = "POST";
        request.SetRequestHeader("Content-Type", "application/json");
        //TODO: handle timeouts?
        //TODO: store a reference to a MonoBehaviour which can run this on a thread - requests might be slow
        request.Send();
    }
}

class RequestBehaviour : MonoBehaviour
{

}

