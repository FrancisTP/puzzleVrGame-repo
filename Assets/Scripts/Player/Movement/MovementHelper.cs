using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MovementHelper : MonoBehaviour
{
    public static readonly float MIN_PLAYER_HEIGHT = 0.20f; // in meters
    public static readonly float MAX_PLAYER_HEIGHT = 2.0f; // in meters

    private static readonly InputDeviceCharacteristics leftHandCharacteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left;
    private static readonly InputDeviceCharacteristics rightHandCharacteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right;
}
