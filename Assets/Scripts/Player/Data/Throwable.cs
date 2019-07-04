using UnityEngine;

// Each gun will also have a ultimate ability of some sort...maybe??

[CreateAssetMenu (fileName = "New Weapon", menuName = "Gun")]
public class Throwable : ScriptableObject
{
    [Header("Base Abiities")]
    public new string name;
    public int damage;
    public int radius;
}
