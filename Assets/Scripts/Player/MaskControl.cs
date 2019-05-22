using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskControl : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask target;

    private bool maskOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponControl.instance.CurrentWeapon == WeaponControl.WeaponState.MASK) {

            if (Input.GetButtonDown("Fire1")) {

                // Check if I'm being chased

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
