using UnityEngine;
[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunScript : ScriptableObject
{
    public string name;
    public string description;
    public float ammo;
    public float ammoInClip;
    public float damage;
    public float damageOnHeadshot;
    public Sprite gunSprite;
    public float areaDamage;
    public float areaRange;
    public float xYScale;
    public float gunDelay;
}
