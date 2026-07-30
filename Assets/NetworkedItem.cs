using UnityEngine;
using Photon.Pun;

public class NetworkedItem : MonoBehaviourPun
{
    void Awake()
    {
        // InstantiationData delivered to all clients that spawn this prefab
        object[] data = photonView.InstantiationData;
        if (data != null && data.Length > 0)
        {
            if (data[0] is int parentViewId && parentViewId != -1)
            {
                PhotonView parentPV = PhotonView.Find(parentViewId);
                if (parentPV != null)
                {
                    transform.SetParent(parentPV.transform, worldPositionStays: true);
                }
            }
        }
    }
}