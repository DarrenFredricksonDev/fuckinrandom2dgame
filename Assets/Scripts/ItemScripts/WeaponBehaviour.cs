using UnityEngine;
using Photon.Pun;

public class WeaponBehaviour : MonoBehaviourPun
{
    // Local reference to the ScriptableObject containing weapon data
    [SerializeField] GunScript data;

    // Fallback range if your GunScript doesn't include a range field
    public float range = 20f;

    // Called over network to set the ScriptableObject by name (requires assets in Resources/GunScripts)
    [PunRPC]
    public void RPC_SetWeaponData(string scriptableName)
    {
        data = Resources.Load<GunScript>($"GunScripts/{scriptableName}");
        // fallback: leave null if not found
    }

    // Owner triggers shooting; this method runs on the owner only
    public void TryShoot()
    {
        if (!photonView.IsMine) return;

        // Ask MasterClient to validate & perform hit detection
        photonView.RPC(nameof(RPC_RequestShoot), RpcTarget.MasterClient, transform.position, transform.right);
    }

    // Runs on MasterClient: validate then perform hit detection and damage assignment
    [PunRPC]
    void RPC_RequestShoot(Vector3 origin, Vector3 direction, PhotonMessageInfo info)
    {
        // Validate requester here (rate limiting, ammo, ownership) as needed.

        if (data == null)
        {
            // No weapon data: optionally broadcast VFX and return
            photonView.RPC(nameof(RPC_PerformShootVfx), RpcTarget.AllBuffered);
            return;
        }

        // Perform 2D raycast (adjust to Physics.Raycast for 3D)
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, range);
        if (hit.collider != null)
        {
            // Determine damage amount (headshot if collider tagged "Head")
            float damageToApply = hit.collider.CompareTag("Head") ? data.damageOnHeadshot : data.damage;

            // Try to find the PhotonView on the hit object or its parents
            PhotonView hitPv = hit.collider.GetComponentInParent<PhotonView>();
            if (hitPv != null)
            {
                // Tell the hit object's owner to apply damage locally (authoritative owner modifies its HP)
                hitPv.RPC("RPC_TakeDamage", hitPv.Owner, damageToApply);
            }

            // Optionally handle environment hits (decals, penetration, etc.)
        }

        // Broadcast shoot VFX to everyone
        photonView.RPC(nameof(RPC_PerformShootVfx), RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_PerformShootVfx()
    {
        // Play muzzle flash, sound, spawn tracer, etc. (use local `data` for visuals/stats)
    }
}