using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3PIDController : MonoBehaviour {

    private PIDController pidControllerX = null;
    private PIDController pidControllerY = null;
    private PIDController pidControllerZ = null;

    private float pFactorX, iFactorX, dFactorX;
    private float pFactorY, iFactorY, dFactorY;
    private float pFactorZ, iFactorZ, dFactorZ;

    public Vector3PIDController(float pFactor, float iFactor, float dFactor) {
        this.pFactorX = pFactor;
        this.iFactorX = iFactor;
        this.dFactorX = dFactor;

        this.pFactorY = pFactor;
        this.iFactorY = iFactor;
        this.dFactorY = dFactor;

        this.pFactorZ = pFactor;
        this.iFactorZ = iFactor;
        this.dFactorZ = dFactor;

        intantiatePIDControllers();
    }

    public Vector3PIDController(float pFactorX, float iFactorX, float dFactorX, float pFactorY, float iFactorY, float dFactorY, float pFactorZ, float iFactorZ, float dFactorZ) {
        this.pFactorX = pFactorX;
        this.iFactorX = iFactorX;
        this.dFactorX = dFactorX;

        this.pFactorY = pFactorY;
        this.iFactorY = iFactorY;
        this.dFactorY = dFactorY;

        this.pFactorZ = pFactorZ;
        this.iFactorZ = iFactorZ;
        this.dFactorZ = dFactorZ;

        intantiatePIDControllers();
    }

    public Vector3PIDController(Vector3 factorsX, Vector3 factorsY, Vector3 factorsZ) {
        this.pFactorX = factorsX.x;
        this.iFactorX = factorsX.y;
        this.dFactorX = factorsX.z;

        this.pFactorY = factorsY.x;
        this.iFactorY = factorsY.y;
        this.dFactorY = factorsY.z;

        this.pFactorZ = factorsZ.x;
        this.iFactorZ = factorsZ.y;
        this.dFactorZ = factorsZ.z;

        intantiatePIDControllers();
    }

    private void intantiatePIDControllers() {
        pidControllerX = new PIDController(pFactorX, iFactorX, dFactorX);
        pidControllerY = new PIDController(pFactorY, iFactorY, dFactorY);
        pidControllerZ = new PIDController(pFactorZ, iFactorZ, dFactorZ);
    }

    public Vector3 updatePID(Vector3 setpoint, Vector3 actual, float timeFrame) {
        float pidX = pidControllerX.updatePID(setpoint.x, actual.x, Time.fixedDeltaTime);
        float pidY = pidControllerY.updatePID(setpoint.y, actual.y, Time.fixedDeltaTime);
        float pidZ = pidControllerZ.updatePID(setpoint.z, actual.z, Time.fixedDeltaTime);

        Vector3 newVector = new Vector3(pidX, pidY, pidZ);
        return newVector;
    }

    public Vector3 updatePID(Vector3 setpoint, Vector3 actual, float timeFrame, float pFactor, float iFactor, float dFactor) {
        float pidX = pidControllerX.updatePID(setpoint.x, actual.x, Time.fixedDeltaTime, pFactor, iFactor, dFactor);
        float pidY = pidControllerY.updatePID(setpoint.y, actual.y, Time.fixedDeltaTime, pFactor, iFactor, dFactor);
        float pidZ = pidControllerZ.updatePID(setpoint.z, actual.z, Time.fixedDeltaTime, pFactor, iFactor, dFactor);

        Vector3 newVector = new Vector3(pidX, pidY, pidZ);
        return newVector;
    }

    public Vector3 updatePID(Vector3 setpoint, Vector3 actual, float timeFrame, float pFactorX, float iFactorX, float dFactorX, float pFactorY, float iFactorY, float dFactorY, float pFactorZ, float iFactorZ, float dFactorZ) {
        float pidX = pidControllerX.updatePID(setpoint.x, actual.x, Time.fixedDeltaTime, pFactorX, iFactorX, dFactorX);
        float pidY = pidControllerY.updatePID(setpoint.y, actual.y, Time.fixedDeltaTime, pFactorY, iFactorY, dFactorY);
        float pidZ = pidControllerZ.updatePID(setpoint.z, actual.z, Time.fixedDeltaTime, pFactorZ, iFactorZ, dFactorZ);

        Vector3 newVector = new Vector3(pidX, pidY, pidZ);
        return newVector;
    }
}