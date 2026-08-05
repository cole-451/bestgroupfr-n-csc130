using UnityEngine;
using UnityEngine.InputSystem;

public class GuitarWizardController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // Keyboard will look like D F J K L, matching GREEN(A), RED(B), YELLOW(Y), BLUE(X), ORANGE(LB)
        if(Keyboard.current.dKey.isPressed || Gamepad.current.aButton.isPressed)
        {

        }
    }
}
