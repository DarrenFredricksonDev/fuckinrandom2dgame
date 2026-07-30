using UnityEngine;
using Photon.Pun;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject basePreFab1;
    [SerializeField] private GameObject basePreFab2;
    [SerializeField] private GameObject basePreFab3;
    [SerializeField] private GameObject smallPreFab1;
    [SerializeField] private GameObject smallPreFab2;
    [SerializeField] private GameObject smallPreFab3;
    [SerializeField] private float baseYPos = -3f;
    [SerializeField] private float baseXPos = 0f;
    [SerializeField] private float smallYPos = 2f;
    [SerializeField] private float itemChance = 0.5f;
    public GameObject item1;
    public int genSize = 3;

    void Start()
    {
        int playerCount = PhotonNetwork.PlayerList.Length;
        if (playerCount <= 2) genSize = 3;
        else if (playerCount < 4) genSize = 5;
        else genSize = 8;

        int randomBase = Random.Range(0, 3);
        Vector2 basePos = new Vector2(baseXPos, baseYPos);

        if (!PhotonNetwork.IsMasterClient) return;

        GameObject basePrefab = randomBase == 0 ? basePreFab1 : (randomBase == 1 ? basePreFab2 : basePreFab3);
        PhotonNetwork.Instantiate(basePrefab.name, basePos, Quaternion.identity);

        for (int i = 0; i < genSize; i++)
        {
            float smallXPos = Random.Range(-20f, 21f);
            int randomSmall = Random.Range(0, 3);
            Vector2 smallPos = new Vector2(smallXPos, smallYPos);
            Vector2 itemPos = new Vector2(smallXPos, smallYPos + 2f);

            GameObject smallPrefab = randomSmall == 0 ? smallPreFab1 : (randomSmall == 1 ? smallPreFab2 : smallPreFab3);
            GameObject smallInstance = PhotonNetwork.Instantiate(smallPrefab.name, smallPos, Quaternion.identity);

            if (item1 != null && Random.value < itemChance)
            {
                // Pass parent PhotonView ID in instantiation data so all clients parent the item
                PhotonView parentPV = smallInstance.GetComponent<PhotonView>();
                int parentViewId = parentPV != null ? parentPV.ViewID : -1;
                object[] instData = new object[] { parentViewId };
                PhotonNetwork.Instantiate(item1.name, itemPos, Quaternion.identity, 0, instData);
                // Do NOT call SetParent locally here.
            }
        }
    }
}