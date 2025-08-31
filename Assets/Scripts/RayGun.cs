using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayGun : MonoBehaviour
{
    public static RayGun Instance { get; private set; }

    // [SerializeField]
    // private Objects Cars;
    private GameObject selectedCar;
    public event Action<GameObject> CarSelected;
    public event Action<GameObject> CarDeselected;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Target"))
            {
                if (selectedCar != null) CarDeselected?.Invoke(selectedCar);
                selectedCar = hit.transform.gameObject;
                CarSelected?.Invoke(selectedCar);
            }
            else
            {
                CarDeselected?.Invoke(selectedCar);
            }
        }
    }
}
