using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

// Class will eventually abstract main input so I can potentially implement Unity's experimental system later on or resort to Rewired

public class InputController : MonoBehaviour
{
    public static InputController instance = null;

    private int playerID = 0;
    private Player player;

    // Controller Maps
    private ControllerMap mouseMap;
    private ControllerMap keyboardMap;
    private ControllerMap joystickMap;
    private Mouse mouse;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = ReInput.players.GetPlayer(playerID);

        // Retrieve input maps from Rewired
        mouseMap = player.controllers.maps.GetMap(ControllerType.Mouse, 0, 1);
        keyboardMap = player.controllers.maps.GetMap(ControllerType.Keyboard, 0, 0);
        joystickMap = player.controllers.maps.GetMap(ControllerType.Joystick, 0, 2);

        // Check which input should be used.
        if (player.controllers.Joysticks.Count == 1) {
            // By Default, disable the keyboard map if a controller is connected
            keyboardMap.enabled = false;
            mouseMap.enabled = false;
        } else {
            // If no controller is connected, then ensure that the keyboard are enabled
            keyboardMap.enabled = true;
            mouseMap.enabled = true;
        }

        mouse = ReInput.controllers.Mouse;
    }

    public bool isControllerActive()
    {
        return player.controllers.joystickCount == 1;
    }

    public Vector2 MousePosition()
    {
        return mouse.screenPosition;
    }

    public float Horizontal()
    {
        return player.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        return player.GetAxis("Vertical");
    }

    public bool FireWeapon()
    {
        return player.GetButton("UseGadget");
    }

    public bool SmokeBomb()
    {
        return player.GetButton("UseSmokebomb");
    }

    public float Rotation()
    {
        Vector2 rotateRaw = new Vector2(player.GetAxis("RotateVertical"), player.GetAxis("RotateHorizontal"));
        rotateRaw.Normalize();
        return Mathf.Atan2(rotateRaw.x, rotateRaw.y) * Mathf.Rad2Deg;
    }

    public bool SelectWeapon()
    {
        return player.GetButton("GadgetWheel");
    }
}
