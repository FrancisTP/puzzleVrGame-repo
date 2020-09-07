using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExampleListener : MonoBehaviour {

    public AxisHandler2D primary2DAxisHandler_left = null;

    public AxisHandler2D primary2DAxisHandler_right = null;


    public AxisHandler triggerHandler_left = null;
    public AxisHandler gripHandler_left = null;

    public AxisHandler triggerHandler_right = null;
    public AxisHandler gripHandler_right = null;


    public ButtonHandler primaryButtonHandler_left = null;
    public ButtonHandler primaryTouchHandler_left = null;
    public ButtonHandler secondaryButtonHandler_left = null;
    public ButtonHandler secondaryTouchHandler_left = null;
    public ButtonHandler gripButtonHandler_left = null;
    public ButtonHandler triggerButtonHandler_left = null;
    public ButtonHandler menuButtonHandler_left = null;
    public ButtonHandler primary2DAxisClickHandler_left = null;
    public ButtonHandler primary2DAxisTouchHandler_left = null;

    public ButtonHandler primaryButtonHandler_right = null;
    public ButtonHandler primaryTouchHandler_right = null;
    public ButtonHandler secondaryButtonHandler_right = null;
    public ButtonHandler secondaryTouchHandler_right = null;
    public ButtonHandler gripButtonHandler_right = null;
    public ButtonHandler triggerButtonHandler_right = null;
    public ButtonHandler primary2DAxisClickHandler_right = null;
    public ButtonHandler primary2DAxisTouchHandler_right = null;



    public void OnEnable()
    {
        primary2DAxisHandler_left.OnValueChange += PrintPrimary2DAxisHandler_left;

        primary2DAxisHandler_right.OnValueChange += PrintPrimary2DAxisHandler_right;


        triggerHandler_left.OnValueChange += PrintTriggerHandler_left;
        gripHandler_left.OnValueChange += PrintGipHandler_left;

        triggerHandler_right.OnValueChange += PrintTriggerHandler_right;
        gripHandler_right.OnValueChange += PrintGipHandler_right;


        primaryButtonHandler_left.OnButtonDown += PrintPrimaryButtonHandlerDown_left;
        primaryButtonHandler_left.OnButtonUp += PrintPrimaryButtonHandlerUp_left;
        primaryTouchHandler_left.OnButtonDown += PrintPrimaryTouchHandlerDown_left;
        primaryTouchHandler_left.OnButtonUp += PrintPrimaryTouchHandlerUp_left;
        secondaryButtonHandler_left.OnButtonDown += PrintSecondaryButtonHandlerDown_left;
        secondaryButtonHandler_left.OnButtonUp += PrintSecondaryButtonHandlerUp_left;
        secondaryTouchHandler_left.OnButtonDown += PrintSecondaryTouchHandlerDown_left;
        secondaryTouchHandler_left.OnButtonUp += PrintSecondaryTouchHandlerUp_left;
        gripButtonHandler_left.OnButtonDown += PrintGripButtonHandlerDown_left;
        gripButtonHandler_left.OnButtonUp += PrintGripButtonHandlerUp_left;
        triggerButtonHandler_left.OnButtonDown += PrintTriggerButtonHandlerDown_left;
        triggerButtonHandler_left.OnButtonUp += PrintTriggerButtonHandlerUp_left;
        menuButtonHandler_left.OnButtonDown += PrintMenuButtonHandlerDown_left;
        menuButtonHandler_left.OnButtonUp += PrintMenuButtonHandlerUp_left;
        primary2DAxisClickHandler_left.OnButtonDown += PrintPrimary2DAxisClickHandlerDown_left;
        primary2DAxisClickHandler_left.OnButtonUp += PrintPrimary2DAxisClickHandlerUp_left;
        primary2DAxisTouchHandler_left.OnButtonDown += PrintPrimary2DAxisTouchHandlerDown_left;
        primary2DAxisTouchHandler_left.OnButtonUp += PrintPrimary2DAxisTouchHandlerUp_left;

        primaryButtonHandler_right.OnButtonDown += PrintPrimaryButtonHandlerDown_right;
        primaryButtonHandler_right.OnButtonUp += PrintPrimaryButtonHandlerUp_right;
        primaryTouchHandler_right.OnButtonDown += PrintPrimaryTouchHandlerDown_right;
        primaryTouchHandler_right.OnButtonUp += PrintPrimaryTouchHandlerUp_right;
        secondaryButtonHandler_right.OnButtonDown += PrintSecondaryButtonHandlerDown_right;
        secondaryButtonHandler_right.OnButtonUp += PrintSecondaryButtonHandlerUp_right;
        secondaryTouchHandler_right.OnButtonDown += PrintSecondaryTouchHandlerDown_right;
        secondaryTouchHandler_right.OnButtonUp += PrintSecondaryTouchHandlerUp_right;
        gripButtonHandler_right.OnButtonDown += PrintGripButtonHandlerDown_right;
        gripButtonHandler_right.OnButtonUp += PrintGripButtonHandlerUp_right;
        triggerButtonHandler_right.OnButtonDown += PrintTriggerButtonHandlerDown_right;
        triggerButtonHandler_right.OnButtonUp += PrintTriggerButtonHandlerUp_right;
        primary2DAxisClickHandler_right.OnButtonDown += PrintPrimary2DAxisClickHandlerDown_right;
        primary2DAxisClickHandler_right.OnButtonUp += PrintPrimary2DAxisClickHandlerUp_right;
        primary2DAxisTouchHandler_right.OnButtonDown += PrintPrimary2DAxisTouchHandlerDown_right;
        primary2DAxisTouchHandler_right.OnButtonUp += PrintPrimary2DAxisTouchHandlerUp_right;

    }



    public void OnDisable()
    {
        primary2DAxisHandler_left.OnValueChange -= PrintPrimary2DAxisHandler_left;

        primary2DAxisHandler_left.OnValueChange -= PrintPrimary2DAxisHandler_right;


        triggerHandler_left.OnValueChange -= PrintTriggerHandler_left;
        gripHandler_left.OnValueChange -= PrintGipHandler_left;

        triggerHandler_right.OnValueChange -= PrintTriggerHandler_right;
        gripHandler_right.OnValueChange -= PrintGipHandler_right;


        primaryButtonHandler_left.OnButtonDown -= PrintPrimaryButtonHandlerDown_left;
        primaryButtonHandler_left.OnButtonUp -= PrintPrimaryButtonHandlerUp_left;
        primaryTouchHandler_left.OnButtonDown -= PrintPrimaryTouchHandlerDown_left;
        primaryTouchHandler_left.OnButtonUp -= PrintPrimaryTouchHandlerUp_left;
        secondaryButtonHandler_left.OnButtonDown -= PrintSecondaryButtonHandlerDown_left;
        secondaryButtonHandler_left.OnButtonUp -= PrintSecondaryButtonHandlerUp_left;
        secondaryTouchHandler_left.OnButtonDown -= PrintSecondaryTouchHandlerDown_left;
        secondaryTouchHandler_left.OnButtonUp -= PrintSecondaryTouchHandlerUp_left;
        gripButtonHandler_left.OnButtonDown -= PrintGripButtonHandlerDown_left;
        gripButtonHandler_left.OnButtonUp -= PrintGripButtonHandlerUp_left;
        triggerButtonHandler_left.OnButtonDown -= PrintTriggerButtonHandlerDown_left;
        triggerButtonHandler_left.OnButtonUp -= PrintTriggerButtonHandlerUp_left;
        menuButtonHandler_left.OnButtonDown -= PrintMenuButtonHandlerDown_left;
        menuButtonHandler_left.OnButtonUp -= PrintMenuButtonHandlerUp_left;
        primary2DAxisClickHandler_left.OnButtonDown -= PrintPrimary2DAxisClickHandlerDown_left;
        primary2DAxisClickHandler_left.OnButtonUp -= PrintPrimary2DAxisClickHandlerUp_left;
        primary2DAxisTouchHandler_left.OnButtonDown -= PrintPrimary2DAxisTouchHandlerDown_left;
        primary2DAxisTouchHandler_left.OnButtonUp -= PrintPrimary2DAxisTouchHandlerUp_left;

        primaryButtonHandler_right.OnButtonDown -= PrintPrimaryButtonHandlerDown_right;
        primaryButtonHandler_right.OnButtonUp -= PrintPrimaryButtonHandlerUp_right;
        primaryTouchHandler_right.OnButtonDown -= PrintPrimaryTouchHandlerDown_right;
        primaryTouchHandler_right.OnButtonUp -= PrintPrimaryTouchHandlerUp_right;
        secondaryButtonHandler_right.OnButtonDown -= PrintSecondaryButtonHandlerDown_right;
        secondaryButtonHandler_right.OnButtonUp -= PrintSecondaryButtonHandlerUp_right;
        secondaryTouchHandler_right.OnButtonDown -= PrintSecondaryTouchHandlerDown_right;
        secondaryTouchHandler_right.OnButtonUp -= PrintSecondaryTouchHandlerUp_right;
        gripButtonHandler_right.OnButtonDown -= PrintGripButtonHandlerDown_right;
        gripButtonHandler_right.OnButtonUp -= PrintGripButtonHandlerUp_right;
        triggerButtonHandler_right.OnButtonDown -= PrintTriggerButtonHandlerDown_right;
        triggerButtonHandler_right.OnButtonUp -= PrintTriggerButtonHandlerUp_right;
        primary2DAxisClickHandler_right.OnButtonDown -= PrintPrimary2DAxisClickHandlerDown_right;
        primary2DAxisClickHandler_right.OnButtonUp -= PrintPrimary2DAxisClickHandlerUp_right;
        primary2DAxisTouchHandler_right.OnButtonDown -= PrintPrimary2DAxisTouchHandlerDown_right;
        primary2DAxisTouchHandler_right.OnButtonUp -= PrintPrimary2DAxisTouchHandlerUp_right;
    }



    private void PrintPrimary2DAxisHandler_left(XRController controller, Vector2 value) {
        print("Primary 2D axis value: " + value + " - left");
    }

    private void PrintPrimary2DAxisHandler_right(XRController controller, Vector2 value) {
        print("Primary 2D axis value: " + value + " - right");
    }



    private void PrintTriggerHandler_left(XRController controller, float value) {
        print("Trigger value: " + value + " - left");
    }

    private void PrintGipHandler_left(XRController controller, float value) {
        print("Grip value: " + value + " - left");
    }

    private void PrintTriggerHandler_right(XRController controller, float value) {
        print("Trigger value: " + value + " - right");
    }

    private void PrintGipHandler_right(XRController controller, float value) {
        print("Grip value: " + value + " - right");
    }



    private void PrintPrimaryButtonHandlerDown_left(XRController controller) {
        print("Primary button down - left");
    }

    private void PrintPrimaryButtonHandlerUp_left(XRController controller) {
        print("Primary button up - left");
    }

    private void PrintPrimaryTouchHandlerDown_left(XRController controller) {
        print("Primary touch down - left");
    }

    private void PrintPrimaryTouchHandlerUp_left(XRController controller) {
        print("Primary touch up - left");
    }

    private void PrintSecondaryButtonHandlerDown_left(XRController controller) {
        print("Secondary button down - left");
    }

    private void PrintSecondaryButtonHandlerUp_left(XRController controller) {
        print("Secondary button up - left");
    }

    private void PrintSecondaryTouchHandlerDown_left(XRController controller) {
        print("Secondary touch down - left");
    }

    private void PrintSecondaryTouchHandlerUp_left(XRController controller) {
        print("Secondary touch up - left");
    }

    private void PrintGripButtonHandlerDown_left(XRController controller) {
        print("Grip button down - left");
    }

    private void PrintGripButtonHandlerUp_left(XRController controller) {
        print("Grip button up - left");
    }

    private void PrintTriggerButtonHandlerDown_left(XRController controller) {
        print("Trigger button down - left");
    }

    private void PrintTriggerButtonHandlerUp_left(XRController controller) {
        print("Trigger button up - left");
    }

    private void PrintMenuButtonHandlerDown_left(XRController controller) {
        print("Menu button down - left");
    }

    private void PrintMenuButtonHandlerUp_left(XRController controller) {
        print("Menu button up - left");
    }

    private void PrintPrimary2DAxisClickHandlerDown_left(XRController controller) {
        print("Primary 2D axis click down - left");
    }

    private void PrintPrimary2DAxisClickHandlerUp_left(XRController controller) {
        print("Primary 2D axis click up - left");
    }

    private void PrintPrimary2DAxisTouchHandlerDown_left(XRController controller) {
        print("Primary 2D axis touch down - left");
    }
    private void PrintPrimary2DAxisTouchHandlerUp_left(XRController controller) {
        print("Primary 2D axis touch up - left");
    }

    private void PrintPrimaryButtonHandlerDown_right(XRController controller) {
        print("Primary button down - right");
    }

    private void PrintPrimaryButtonHandlerUp_right(XRController controller) {
        print("Primary button up - right");
    }

    private void PrintPrimaryTouchHandlerDown_right(XRController controller) {
        print("Primary touch down - right");
    }

    private void PrintPrimaryTouchHandlerUp_right(XRController controller) {
        print("Primary touch up - right");
    }

    private void PrintSecondaryButtonHandlerDown_right(XRController controller) {
        print("Secondary button down - right");
    }

    private void PrintSecondaryButtonHandlerUp_right(XRController controller) {
        print("Secondary button up - right");
    }

    private void PrintSecondaryTouchHandlerDown_right(XRController controller) {
        print("Secondary touch down - right");
    }

    private void PrintSecondaryTouchHandlerUp_right(XRController controller) {
        print("Secondary touch up - right");
    }

    private void PrintGripButtonHandlerDown_right(XRController controller) {
        print("Grip button down - right");
    }

    private void PrintGripButtonHandlerUp_right(XRController controller) {
        print("Grip button up - right");
    }

    private void PrintTriggerButtonHandlerDown_right(XRController controller) {
        print("Trigger button down - right");
    }

    private void PrintTriggerButtonHandlerUp_right(XRController controller) {
        print("Trigger button up - right");
    }

    private void PrintPrimary2DAxisClickHandlerDown_right(XRController controller) {
        print("Primary 2D axis click down - right");
    }

    private void PrintPrimary2DAxisClickHandlerUp_right(XRController controller) {
        print("Primary 2D axis click up - right");
    }

    private void PrintPrimary2DAxisTouchHandlerDown_right(XRController controller) {
        print("Primary 2D axis touch down - right");
    }
    private void PrintPrimary2DAxisTouchHandlerUp_right(XRController controller) {
        print("Primary 2D axis touch up - right");
    }
}
