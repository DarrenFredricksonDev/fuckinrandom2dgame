using UnityEngine;
using Photon.Pun;

public class M1GarandPlayerScript : MonoBehaviourPun
{
    public bool hasGarand;
    // ScriptableObject asset (put in Resources/GunScripts or keep a naming convention)
    public GunScript weaponData;
    // Path under Resources to the networked weapon prefab (e.g. "Weapons/M1GarandPrefab")
    public GameObject weaponPrefab;
    public float spawnDistance = 1f;

    GameObject weaponInstance;

    void Start()
    {
        hasGarand = false;
    }

    void Update()
    {
        // Only local player handles input
        if (!photonView.IsMine) return;
        if (!Camera.main) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        if (!hasGarand) return;

        if (Input.GetMouseButtonDown(0))
        {
            // Ask the weapon to shoot (owner-only)
            var wb = weaponInstance ? weaponInstance.GetComponent<WeaponBehaviour>() : null;
            if (wb != null) wb.TryShoot();
        }

        // Rotate weapon to face mouse if present
        if (weaponInstance != null)
        {
            Vector2 dir = mousePos - weaponInstance.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weaponInstance.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void HasGarandTrue()
    {
        // Only the owner/client that wants the weapon should instantiate it
        if (!photonView.IsMine) return;

        hasGarand = true;
        Vector3 spawnPos = transform.position + transform.right * spawnDistance;

        // Prefab must be in Resources for PhotonNetwork.Instantiate with a string path
        weaponInstance = PhotonNetwork.Instantiate(weaponPrefab.name, spawnPos, Quaternion.identity);
        weaponInstance.transform.SetParent(transform, true);

        // Sync which ScriptableObject to use by name (requires your GunScript assets to be loadable via Resources)
        var wpv = weaponInstance.GetComponent<PhotonView>();
        if (wpv != null && weaponData != null)
        {
            // Broadcast the name so every client can load the same ScriptableObject locally
            wpv.RPC("RPC_SetWeaponData", RpcTarget.AllBuffered, weaponData.name);
        }
    }

    public void HasGarandFalse()
    {
        if (!photonView.IsMine) return;

        hasGarand = false;
        if (weaponInstance != null)
        {
            PhotonNetwork.Destroy(weaponInstance);
            weaponInstance = null;
        }
    }
}