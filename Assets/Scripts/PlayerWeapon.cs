using UnityEngine;
using UnityEngine.UI;
public class PlayerWeapon : MonoBehaviour
{
    public static PlayerWeapon instance;
    public Weapon currentWeapon;
    private Shooter shooter;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;        
            shooter = GetComponent<Shooter>();
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        if (shooter == null)
        {
            Debug.LogError("Shooter não encontrado!");
            return;
        }
        currentWeapon = newWeapon;
        shooter.UpdateWeaponStats(newWeapon);
    }
}