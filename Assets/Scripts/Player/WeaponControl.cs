using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public static WeaponControl instance = null;

    [SerializeField] private Texture2D noisemaker;
    [SerializeField] private Texture2D smokebomb;

    public enum WeaponState { GUN, NOISE }
    public WeaponState CurrentWeapon { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("Return to Gun");
            Cursor.visible = false;
            CurrentWeapon = WeaponState.GUN;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Cursor.visible = true;
            Debug.Log("Swap to Noisemaker");
            Cursor.SetCursor(noisemaker, new Vector3(0, 0, -1), CursorMode.Auto);
            CurrentWeapon = WeaponState.NOISE;
        }
    }
}
