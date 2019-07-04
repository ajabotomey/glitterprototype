using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour, IWeaponControl
{
    public static WeaponControl instance = null;

    [SerializeField] private Texture2D noisemaker;
    [SerializeField] private Texture2D smokebomb;

    public enum WeaponState { GUN, GRENADE, NOISE, MASK }
    public WeaponState CurrentWeapon { get; set; }

    public int WeaponCount { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        WeaponCount = Enum.GetValues(typeof(WeaponState)).Length;
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard input
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SelectGun();
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            CurrentWeapon = WeaponState.GRENADE;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SelectNoise();
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SelectMask();
        }

        // Mouse scrollwheel input
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if (CurrentWeapon == WeaponState.GUN)
                CurrentWeapon = (WeaponState)(WeaponCount - 1);
            else
                CurrentWeapon--;

            SelectWeapon((int)CurrentWeapon);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            int value = (int)CurrentWeapon;
            if (value == WeaponCount - 1)
                CurrentWeapon = WeaponState.GUN;
            else
                CurrentWeapon++;

            SelectWeapon((int)CurrentWeapon);
        }

        if (!UIController.instance.isInMenu) {
            if (InputController.instance.SelectWeapon()) {
                UIController.instance.ShowWeaponWheel();
            } else {
                UIController.instance.HideWeaponWheel();
            }
        }
    }

    public void SelectWeapon(int index)
    {
        if (index == 0) {
            SelectGun();
        } else if (index == 1) {
            Debug.Log("Selected Grenade");
        } else if (index == 2) {
            SelectNoise();
        } else if (index == 3) {
            SelectMask();
        }
    }

    public void SelectGun()
    {
        Debug.Log("Return to Gun");
        Cursor.visible = false;
        CurrentWeapon = WeaponState.GUN;
    }

    public void SelectNoise()
    {
        Cursor.visible = true;
        Debug.Log("Swap to Noisemaker");
        Cursor.SetCursor(noisemaker, new Vector3(0, 0, -1), CursorMode.Auto);
        CurrentWeapon = WeaponState.NOISE;
    }

    public void SelectMask()
    {
        Debug.Log("Put on / take off SmartMask");
        Cursor.visible = false;
        CurrentWeapon = WeaponState.MASK;
    }
}
