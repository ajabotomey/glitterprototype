using UnityEngine;

// Each gun will also have a ultimate ability of some sort...maybe??

[CreateAssetMenu (fileName = "New Weapon", menuName = "Gun")]
public class Gun : ScriptableObject
{
    [Header("Base Abiities")]
    public new string name;
    public int damage;
    public int fireRate;
    public int clipSize;
    public int reloadSpeed;
    [Range(0, 100)] public int accuracy;

    [Header("Abilities")]
    public bool explosiveRounds;
    public bool rapidFire;
    public bool grenadeLauncher;
}
