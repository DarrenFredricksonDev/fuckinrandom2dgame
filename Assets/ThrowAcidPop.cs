using UnityEngine;
using Photon.Pun;

public class ThrowAcidPop : MonoBehaviour
{
    public GameObject acidParticlesPrefab;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D()
    {
        PhotonNetwork.Instantiate(acidParticlesPrefab.name, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
