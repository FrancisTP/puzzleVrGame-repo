using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3PIDController : MonoBehaviour
{
    public float pFactor, iFactor, dFactor;

    private Vector3 integral;
    private Vector3 lastError;

    public Vector3PIDController(float pFactor, float iFactor, float dFactor) {
        this.pFactor = pFactor;
        this.iFactor = iFactor;
        this.dFactor = dFactor;
    }

    public Vector3 updatePid(Vector3 setpoint, Vector3 actual, float timeFrame) {
        Vector3 present = setpoint - actual;
        integral += present * timeFrame;
        Vector3 deriv = (present - lastError) / timeFrame;
        lastError = present;
        return (present * pFactor) + (integral * iFactor) + (deriv * dFactor);
    }

    public Vector3 updatePid(Vector3 setpoint, Vector3 actual, float timeFrame, float pFactor, float iFactor, float dFactor) {
        Vector3 present = setpoint - actual;
        integral += present * timeFrame;
        Vector3 deriv = (present - lastError) / timeFrame;
        lastError = present;
        return (present * pFactor) + (integral * iFactor) + (deriv * dFactor);
    }
}
