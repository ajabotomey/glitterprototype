using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class will eventually abstract main input so I can potentially implement Unity's experimental system later on or resort to Rewired

public class InputController : MonoBehaviour
{
    public static InputController instance = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public bool FireWeapon()
    {
        return Input.GetButtonDown("Fire1");
    }

    public bool SmokeBomb()
    {
        return Input.GetButtonDown("Smokebomb");
    }

    public bool SelectWeapon()
    {
        return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }
}
