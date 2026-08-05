using UnityEngine;
using UnityEngine.InputSystem;

public class GuitarWizardController : MonoBehaviour
{
     bool GreenHeld = false;
     bool RedHeld = false;
     bool YellowHeld = false;
     bool BlueHeld = false;
     bool OrangeHeld = false;

    void Start()
    {
        
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        var gamepad = Gamepad.current;

        // just a base kinda prototype for now
        // Keyboard will look like D F J K L, matching GREEN(A), RED(B), YELLOW(Y), BLUE(X), ORANGE(LB)

        GreenHeld = (keyboard != null && keyboard.dKey.isPressed) || (gamepad != null && gamepad.aButton.isPressed);
        RedHeld = (keyboard != null && keyboard.fKey.isPressed) || (gamepad != null && gamepad.bButton.isPressed);
        YellowHeld = (keyboard != null && keyboard.jKey.isPressed) || (gamepad != null && gamepad.yButton.isPressed);
        BlueHeld = (keyboard != null && keyboard.kKey.isPressed) || (gamepad != null && gamepad.xButton.isPressed);
        OrangeHeld = (keyboard != null && keyboard.lKey.isPressed) || (gamepad != null && gamepad.leftShoulder.isPressed);

        if (GreenHeld) Debug.Log("green pressed");
        if (RedHeld) Debug.Log("red pressed");
        if (YellowHeld) Debug.Log("yellow pressed");
        if (BlueHeld) Debug.Log("blue pressed");
        if (OrangeHeld) Debug.Log("orange pressed");
    }
}
