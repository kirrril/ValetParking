using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GizmoZ : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private CarManager carManager;

    public float speedCoeff;
    private float startPointY;
    private float maxDragDistance = 500f;

    public bool isDriving;

    public GameObject car;
    public GameObject carBody;
    private GameObject carTrigger;
    private CollisionHandler collisionHandler;
    public GameObject frontLeftWheelDirection;
    public GameObject frontRightWheelDirection;
    private GameObject frontLeftWheel;
    private GameObject frontRightWheel;
    private GameObject rearLeftWheel;
    private GameObject rearRightWheel;

    Vector3 carMovement;

    private float fullTurn = 360f;
    public bool collisionOccured;


    void OnEnable()
    {
        car = carManager.selectedCar;
        if (car != null)
        {
            carBody = car.transform.Find("CarBody").gameObject;
            carTrigger = car.transform.Find("CarTrigger").gameObject;
            collisionHandler = carTrigger.GetComponent<CollisionHandler>();
            frontLeftWheelDirection = carBody.transform.Find("FrontLeftWheelDirection").gameObject;
            frontRightWheelDirection = carBody.transform.Find("FrontRightWheelDirection").gameObject;
            frontLeftWheel = frontLeftWheelDirection.transform.Find("FrontLeftWheel").gameObject;
            frontRightWheel = frontRightWheelDirection.transform.Find("FrontRightWheel").gameObject;
            rearLeftWheel = carBody.transform.Find("RearLeftWheel").gameObject;
            rearRightWheel = carBody.transform.Find("RearRightWheel").gameObject;

            collisionHandler.crush += OnCrushHandler;
            collisionHandler.triggerExit += OutOfTrigger;
        }

        collisionOccured = false;
        isDriving = false;
        speedCoeff = 0f;
    }

    void OnDisable()
    {
        if (collisionHandler != null)
        {
            collisionHandler.crush -= OnCrushHandler;
            collisionHandler.triggerExit -= OutOfTrigger;
        }

        car = null;
        carBody = null;
        carTrigger = null;
        frontLeftWheel = null;
        frontRightWheel = null;
        rearLeftWheel = null;
        rearRightWheel = null;
    }

    private void OnCrushHandler()
    {
        collisionOccured = true;
    }

    private void OutOfTrigger()
    {
        collisionOccured = false;
        carManager.DeselectCar(car);
        car = null;
        isDriving = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPointY = eventData.position.y;

    }

    public void OnDrag(PointerEventData eventData)
    {
        float currentPointY = eventData.position.y;
        float dragDistance = currentPointY - startPointY;

        speedCoeff = Mathf.Clamp(dragDistance / maxDragDistance, -1f, 1f);
        isDriving = true;
    }

    void Update()
    {
        if (car == null) return;

        bool moveForward = !collisionOccured && isDriving;
        bool moveBackward = collisionOccured && isDriving;
        bool stopMoving = !collisionOccured && !isDriving;

        if (moveForward)
        {
            carMovement = new Vector3(0, 0, speedCoeff * Time.deltaTime);
            car.transform.Translate(carMovement, Space.Self);
            float carMovementDistance = carMovement.z;
            float wheelRotationSpeed = fullTurn * carMovementDistance * 10;

            frontLeftWheel.transform.Rotate(Vector3.right, wheelRotationSpeed, Space.Self);
            frontRightWheel.transform.Rotate(Vector3.right, wheelRotationSpeed, Space.Self);
            rearLeftWheel.transform.Rotate(Vector3.right, wheelRotationSpeed, Space.Self);
            rearRightWheel.transform.Rotate(Vector3.right, wheelRotationSpeed, Space.Self);
        }
        else if (moveBackward)
        {
            isDriving = false;

            Vector3 recoilMovement = new Vector3(0, 0, carMovement.z * -10 * Time.deltaTime);
            car.transform.Translate(recoilMovement, Space.Self);

            float wheelRotationSpeed = fullTurn * recoilMovement.z * 10;

            frontLeftWheel.transform.Rotate(Vector3.right, wheelRotationSpeed, Space.Self);
            frontRightWheel.transform.Rotate(Vector3.right, wheelRotationSpeed, Space.Self);
            rearLeftWheel.transform.Rotate(Vector3.right, wheelRotationSpeed, Space.Self);
            rearRightWheel.transform.Rotate(Vector3.right, wheelRotationSpeed, Space.Self);
        }
        else if (stopMoving)
        {
            speedCoeff = 0;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        speedCoeff = 0;
        isDriving = false;
    }
}
