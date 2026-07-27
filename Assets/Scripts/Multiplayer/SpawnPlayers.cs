using UnityEngine;
using Photon.Pun;

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
    }
}