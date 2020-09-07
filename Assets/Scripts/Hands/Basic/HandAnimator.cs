using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{

    public float speed = 5.0f;

    private Animator animator = null;

    // Add different animation for when the player picks up something
    private XRDirectInteractor interactor = null;

    private readonly List<Finger> gripFingers = new List<Finger>() {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky)
    };

    private readonly List<Finger> pointFingers = new List<Finger> {
        new Finger(FingerType.Index),
        new Finger(FingerType.Thumb)
    };

    private readonly List<Finger> allFingers = new List<Finger>();


    // Input handlers needed for hand animator
    public AxisHandler gripHandler = null;
    public AxisHandler triggerHandler = null;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        allFingers.AddRange(gripFingers);
        allFingers.AddRange(pointFingers);
    }

    public void OnEnable() {
        gripHandler.OnValueChange += CheckGrip;
        triggerHandler.OnValueChange += CheckPointer;
    }

    public void onDisable() {
        gripHandler.OnValueChange -= CheckGrip;
        triggerHandler.OnValueChange -= CheckPointer;
    }

    private void Update()
    {
        // Smooth input values
        SmoothFinger(allFingers);

        // Apply smoothed values
        AnimateFinger(allFingers);
    }

    private void CheckGrip(XRController controller, float value) {
        SetFingerTargets(gripFingers, value);
    }

    private void CheckPointer(XRController controller, float value) {
        SetFingerTargets(pointFingers, value);
    }

    private void SetFingerTargets(List<Finger> fingers, float value) {
        foreach(Finger finger in fingers) {
            finger.target = value;
        }
    }

    private void SmoothFinger(List<Finger> fingers) {
        foreach(Finger finger in fingers) {
            float time = speed * Time.unscaledDeltaTime;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    private void AnimateFinger(List<Finger> fingers) {
        foreach(Finger finger in fingers) {
            AnimateFinger(finger.type.ToString(), finger.current);
        }
    }

    private void AnimateFinger(string finger, float blend) {
        animator.SetFloat(finger, roundToDec(blend, 2));
    }

    private float roundToDec(float num, int dec) {
        return Mathf.Round(num * (10f * dec)) / (10f * dec);
    }
}