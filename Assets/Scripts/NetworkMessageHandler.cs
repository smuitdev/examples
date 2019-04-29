using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMessage
{

}

public class NetMessageLogin : NetMessage
{

}

public class NetworkMessageHandler : MonoBehaviour
{
    public delegate void NetMessageDelegate(NetMessage msg);

    private Dictionary<System.Type, NetMessageDelegate> m_Handlers;

    public void Register<MsgType>(NetMessageDelegate delegateFunction)
    {
        m_Handlers.Add(typeof(MsgType), delegateFunction);
    }
}
