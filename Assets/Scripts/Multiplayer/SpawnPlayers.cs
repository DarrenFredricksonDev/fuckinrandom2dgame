using UnityEngine;
using Photon.Pun;
using Unity.Cinemachine;

public class SpawnPlayersScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public float maxX = 5f;
    public float minX = -5f;
    public float maxY = 5f;
    public float minY = -5f;

    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        // Only set the camera for the local player's instance
        PhotonView pv = player.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine)
        {
            // Find the Cinemachine virtual camera in the scene (or cache a reference)
            CinemachineCamera vcam = FindFirstObjectByType<CinemachineCamera>();
            if (vcam != null)
            {
                vcam.Follow = player.transform;
                vcam.LookAt = player.transform; // optional for aiming the camera
            }
            else
            {
                Debug.LogWarning("No CinemachineCamera found in scene.");
            }
        }
    }
}