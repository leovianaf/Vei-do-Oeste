using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float bulletSpeed = 10f;
    public float range;
    public float fireRate; // Tempo entre disparos
    public int maxBullets;
    public int cost;
    public GameObject bulletPrefab;
    public Sprite weaponIcon; // Ícone da arma para UI
}
