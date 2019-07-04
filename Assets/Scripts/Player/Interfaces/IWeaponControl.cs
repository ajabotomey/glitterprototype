using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponControl
{
    int WeaponCount { get; set; }

    void SelectWeapon(int index);
    void SelectGun();
    void SelectNoise();
    void SelectMask();
}
