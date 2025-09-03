using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class ContentSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject parkingMarkingsPrefab;

    [SerializeField]
    private GameObject parkingSpotsPrefab;

    [SerializeField]
    private GameObject parkingPlacesPrefab;

    [SerializeField]
    private Cars cars;

    [SerializeField]
    private GameObject indicators;
    private GameObject parkingMarkings;
    private GameObject parkingSpots;
    private GameObject parkingPlaces;
    private List<Transform> availableParkingPlaces = new List<Transform>();
    [SerializeField]
    private Transform groundPlaneStage;
    [SerializeField]
    private PlaneFinderBehaviour planeFinder;


    public void InstantiateParkingLot(HitTestResult hitTestResult)
    {
        if (hitTestResult == null) return;

        if (parkingMarkings != null || parkingSpots != null) return;

        if (planeFinder != null)
        {
            planeFinder.enabled = false;
        }

        parkingMarkings = Instantiate(parkingMarkingsPrefab, hitTestResult.Position, hitTestResult.Rotation, groundPlaneStage);
        parkingSpots = Instantiate(parkingSpotsPrefab, hitTestResult.Position, hitTestResult.Rotation, groundPlaneStage);
        parkingPlaces = Instantiate(parkingPlacesPrefab, hitTestResult.Position, hitTestResult.Rotation, groundPlaneStage);

        if (parkingSpots != null)
        {
            for (int i = 1; i <= 24; i++)
            {
                availableParkingPlaces.Add(parkingSpots.transform.Find($"Spot{i}"));
            }
        }

        if (availableParkingPlaces.Count == 24)
        {
            InstantiateCars();
        }
    }

    private void InstantiateCars()
    {
        for (int i = 0; i < cars.carsList.Count; i++)
        {
            if (availableParkingPlaces.Count > 0)
            {
                int randomPlaceIndex = Random.Range(0, availableParkingPlaces.Count);
                Transform randomPlace = availableParkingPlaces[randomPlaceIndex];

                GameObject car = Instantiate(cars.carsList[i].prefab3D, randomPlace.position, randomPlace.rotation, groundPlaneStage);

                CarInfos carInfos = car.GetComponent<CarInfos>();
                carInfos.carBrand = cars.carsList[i].carBrand;
                carInfos.modelName = cars.carsList[i].modelName;
                carInfos.productionYear = cars.carsList[i].productionYear;
                carInfos.motorPower = cars.carsList[i].motorPower;
                carInfos.carPrice = cars.carsList[i].carPrice;
                carInfos.carColor = cars.carsList[i].carColor;

                availableParkingPlaces.RemoveAt(randomPlaceIndex);
            }
        }

        indicators.SetActive(true);
    }
}
