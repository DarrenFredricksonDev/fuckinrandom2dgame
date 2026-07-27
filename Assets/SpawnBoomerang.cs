using Photon.Pun;
using UnityEngine;

public class SpawnBoomerang : MonoBehaviourPun
{
    public GameObject boomerangPrefab;
    public Transform boomerangHoldPoint;
    public PhotonView PhotonView;

    void Start()
    {
        PhotonView = GetComponent<PhotonView>();
        if (PhotonView.IsMine)
        {
            GameObject boomerang = PhotonNetwork.Instantiate(
                boomerangPrefab.name,
                boomerangHoldPoint.position,
                Quaternion.identity
            );

            boomerang.transform.SetParent(transform);
        }
    }
}