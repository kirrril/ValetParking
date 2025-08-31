using System;
using TMPro;
using UnityEngine;

public class SpotHandler : MonoBehaviour
{
    private Detector detector;
    int placeIndex;

    void OnEnable()
    {

        detector = GetComponentInParent<Detector>();
        placeIndex = Convert.ToInt32(gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parktronic"))
        {
            GameObject placedCar = other.transform.parent.gameObject;
            detector.PlaceCar(placeIndex, placedCar);

            Debug.Log($"Car placed on place {gameObject.name}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Parktronic"))
        {
            detector.EmptySpot(placeIndex);
            Debug.Log($"Car removed from place {gameObject.name}");
        }
    }
}
