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
        // just a base kinda prototype for now
        // Keyboard will look like D F J K L, matching GREEN(A), RED(B), YELLOW(Y), BLUE(X), ORANGE(LB)
        if(Keyboard.current.dKey.isPressed || Gamepad.current.aButton.isPressed)
        {
            GreenHeld = true;
            Debug.Log("green pressed");
        }
        if(Keyboard.current.fKey.isPressed || Gamepad.current.bButton.isPressed)
        {
            RedHeld = true;
            Debug.Log("red pressed");
        }
        if(Keyboard.current.jKey.isPressed || Gamepad.current.yButton.isPressed)
        {
            YellowHeld = true;
            Debug.Log("yellow pressed");
        }
        if(Keyboard.current.kKey.isPressed || Gamepad.current.xButton.isPressed)
        {
            BlueHeld = true;
            Debug.Log("blue pressed");
        }
        if(Keyboard.current.lKey.isPressed || Gamepad.current.leftShoulder.isPressed)
        {
            OrangeHeld = true;
            Debug.Log("orange pressed");
        }
    }
}
