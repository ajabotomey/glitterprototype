using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskControl : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask target;

    private bool maskOn = false;
    public bool BeingChased { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // No accidentally firing the weapon while selecting a weapon
        if (InputController.instance.SelectWeapon())
            return;

        if (WeaponControl.instance.CurrentWeapon == WeaponControl.WeaponState.MASK) {

            if (InputController.instance.FireWeapon()) {

                // Check if I'm being chased
                if (!BeingChased) {
                    if (maskOn) {
                        // Change the sprite

                        // Change the layermask
                        this.gameObject.layer = (int)Mathf.Log(player.value, 2);

                        maskOn = false;
                    } else {
                        // Change the sprite

                        // Change the layermask
                        this.gameObject.layer = (int)Mathf.Log(target.value, 2);

                        maskOn = true;
                    }
                }
            }
        }
    }
}
