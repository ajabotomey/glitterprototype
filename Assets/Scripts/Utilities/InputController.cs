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
    private Joystick joystick;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = ReInput.players.GetPlayer(playerID);

        // Check which input should be used.
        if (player.controllers.Joysticks.Count == 1) {
            joystick = player.controllers.Joysticks[0]; // Only ever be one joystick
        } else {
            mouse = ReInput.controllers.Mouse;
            joystick = null;
        }
    }

    public bool IsControllerActive()
    {
        //return player.controllers.joystickCount == 1;
        Controller controller = player.controllers.GetLastActiveController();
        if (controller != null) {
            switch (controller.type) {
                case ControllerType.Keyboard:
                    return false;
                case ControllerType.Joystick:
                    return true;
                case ControllerType.Mouse:
                    return false;
                case ControllerType.Custom:
                    // Do something custom controller
                    break;
            }
        }

        return false;
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

    // Set rumble in all motors
    public void SetRumble(float duration)
    {
        if (joystick == null)
            return;

        var sensitivity = SettingsManager.Instance.GetRumbleSensitivity();

        if (!joystick.supportsVibration) return;
        for (int i = 0; i < joystick.vibrationMotorCount; i++) {
            joystick.SetVibration(i, sensitivity, duration);
        }
    }

    public void StopRumble()
    {
        joystick.StopVibration();
    }

    public bool UICancel()
    {
        return player.GetButton("UICancel");
    }
}
