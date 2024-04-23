using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketGameProtocol;

public class RequestManager : BaseManager
{
    public RequestManager(GameFace face) : base(face) { }

    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

    public void AddRequest(BaseRequest request)
    {
        Debug.Log(request.ToString());
        requestDict.Add(request.GetActionCode, request);
        Debug.Log(requestDict.Count);
    }

    public void RemoveRequest(ActionCode action)
    {
        requestDict.Remove(action);
    }

    public void HandleResponse(MainPack pack)
    {
        if(requestDict.TryGetValue(pack.Actioncode,out BaseRequest request))
        {
            request.OnResponse(pack);
        }
        else
        {
            Debug.Log(pack.Actioncode);
            Debug.LogWarning("不能找到对应的处理");
        }
    }
}
