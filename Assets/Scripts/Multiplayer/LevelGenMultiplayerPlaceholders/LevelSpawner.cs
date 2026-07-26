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

    void Start()
    {
        int randomBase = Random.Range(0, 3);
        Vector2 basePos = new Vector2(baseXPos, baseYPos);

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (randomBase == 0)
        {
            PhotonNetwork.Instantiate(basePreFab1.name, basePos, Quaternion.identity);
        }
        else if (randomBase == 1)
        {
            PhotonNetwork.Instantiate(basePreFab2.name, basePos, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(basePreFab3.name, basePos, Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {
            float smallXPos = Random.Range(-10f, 11f);
            int randomSmall = Random.Range(0, 3);
            Vector2 smallPos = new Vector2(smallXPos, smallYPos);

            if (randomSmall == 0)
            {
                PhotonNetwork.Instantiate(smallPreFab1.name, smallPos, Quaternion.identity);
            }
            else if (randomSmall == 1)
            {
                PhotonNetwork.Instantiate(smallPreFab2.name, smallPos, Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(smallPreFab3.name, smallPos, Quaternion.identity);
            }
        }
    }
}
