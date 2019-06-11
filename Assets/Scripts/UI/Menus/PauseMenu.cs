using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (WeaponControl.instance.CurrentWeapon == WeaponControl.WeaponState.NOISE) {
            WeaponControl.instance.SelectNoise(); // totally a cop out here
        } else {
            Cursor.visible = false;
        }

        gameObject.SetActive(false);
    }
}
