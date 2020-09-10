using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDController : MonoBehaviour
{
	private float pFactor, iFactor, dFactor;

	private float integral;
	private float lastError;


	public PIDController(float pFactor, float iFactor, float dFactor) {
		this.pFactor = pFactor;
		this.iFactor = iFactor;
		this.dFactor = dFactor;
	}


	public float updatePID(float setpoint, float actual, float timeFrame) {
		float present = setpoint - actual;
		integral += present * timeFrame;
		float deriv = (present - lastError) / timeFrame;
		lastError = present;
		return present * pFactor + integral * iFactor + deriv * dFactor;
	}

	public float updatePID(float setpoint, float actual, float timeFrame, float pFactor, float iFactor, float dFactor) {
		float present = setpoint - actual;
		integral += present * timeFrame;
		float deriv = (present - lastError) / timeFrame;
		lastError = present;
		return (present * pFactor) + (integral * iFactor) + (deriv * dFactor);
	}
}
