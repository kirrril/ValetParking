using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GizmoX : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // [SerializeField]
    // private CarManager carManager;

    [SerializeField]
    private GizmoZ gizmoZ;

    // private GameObject carBody;
    // private GameObject frontLeftWheelDirection;
    // private GameObject frontRightWheelDirection;


    private float speedCoeffX;
    private float startPointX;
    private float maxDragDistance = 5f;
    private float rotationSpeed = 200f;

    private float targetSteeringAngle = 0f;
    private float currentSteeringAngle = 0f;
    private float maxSteeringAngle = 45f;
    private float steeringSmoothness = 5f;

    // void OnEnable()
    // {
    //     carBody = carManager.selectedCar.transform.Find("CarBody").gameObject;
    //     frontLeftWheelDirection = carBody.transform.Find("FrontLeftWheelDirection").gameObject;
    //     frontRightWheelDirection = carBody.transform.Find("FrontRightWheelDirection").gameObject;
    // }

    // void OnDisable()
    // {
    //     carBody = null;
    //     frontLeftWheelDirection = null;
    //     frontRightWheelDirection = null;
    // }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPointX = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float currentPointX = eventData.position.x;
        float dragDistance = currentPointX - startPointX;

        speedCoeffX = Math.Clamp(dragDistance / maxDragDistance, -1f, 1f);
        targetSteeringAngle = speedCoeffX * maxSteeringAngle;
    }

    void LateUpdate()
    {
        if (gizmoZ.car == null) return;

        currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, targetSteeringAngle, Time.deltaTime * steeringSmoothness);

        gizmoZ.frontLeftWheelDirection.transform.localRotation = Quaternion.Euler(0, 0, currentSteeringAngle);
        gizmoZ.frontRightWheelDirection.transform.localRotation = Quaternion.Euler(0, 0, currentSteeringAngle);

        if (!gizmoZ.collisionOccured && gizmoZ.isDriving)
        {
            float carRotationSpeed = speedCoeffX * rotationSpeed * gizmoZ.speedCoeff * Time.deltaTime;
            Vector3 carRotation = new Vector3(0, carRotationSpeed, 0);
            gizmoZ.car.transform.Rotate(carRotation, Space.Self);
        }
        else if (gizmoZ.collisionOccured && gizmoZ.isDriving)
        {
            float carRotationSpeed = -1 * speedCoeffX * rotationSpeed * gizmoZ.speedCoeff * Time.deltaTime;
            Vector3 carRotation = new Vector3(0, carRotationSpeed, 0);
            gizmoZ.car.transform.Rotate(carRotation, Space.Self);
        }
        else if (!gizmoZ.collisionOccured && !gizmoZ.isDriving)
        {
            speedCoeffX = 0;
            currentSteeringAngle = 0;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        speedCoeffX = 0;
        targetSteeringAngle = 0;
    }
}
