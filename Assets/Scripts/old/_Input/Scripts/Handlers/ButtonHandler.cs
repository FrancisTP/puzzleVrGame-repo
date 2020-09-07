using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CreateAssetMenu(fileName = "NewButtonHandler")]
public class ButtonHandler : InputHandler {

    private float customAxisToPressThreshold = 0.001f;

    public InputHelpers.Button button = InputHelpers.Button.None;

    public delegate void StateChange(XRController controller);
    public event StateChange OnButtonDown;
    public event StateChange OnButtonUp;

    private bool previousPress = false;
    public bool IsPressed {
        get { return previousPress; }
    }

    public override void HandleState(XRController controller)
    {
        if(controller.inputDevice.IsPressed(button, out bool pressed, customAxisToPressThreshold)) {
            if (previousPress != pressed) {
                previousPress = pressed;

                if (pressed) {
                    OnButtonDown?.Invoke(controller);
                } else {
                    OnButtonUp?.Invoke(controller);
                }
            }
        }
    }
}