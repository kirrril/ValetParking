using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private GameObject[] parkingSpots = new GameObject[24];

    private bool colorSucceeded;
    private bool brandSucceeded;

    private List<Renderer> activeRenderers = new List<Renderer>();

    private List<int> redCars = new List<int>();
    private List<int> yellowCars = new List<int>();
    private List<int> whiteCars = new List<int>();
    private List<int> blueCars = new List<int>();

    private List<int> toyotaCars = new List<int>();
    private List<int> fordCars = new List<int>();
    private List<int> bmwCars = new List<int>();

    private List<int> yearsList = new List<int>();
    private List<int> powerList = new List<int>();
    private List<int> priceList = new List<int>();
    private List<int> consecutiveIncreasingDecreasing = new List<int>();


    public void PlaceCar(int placeIndex, GameObject placedCar)
    {
        parkingSpots[placeIndex] = placedCar;
        SortAll();

        Debug.Log($"Car {placedCar} is placed on the spot {placeIndex}");
    }

    public void EmptySpot(int placeIndex)
    {
        foreach (Renderer renderer in activeRenderers)
        {
            renderer.enabled = false;
        }
        activeRenderers.Clear();

        parkingSpots[placeIndex] = null;
        colorSucceeded = false;
        brandSucceeded = false;

        SortAll();
    }

    private void SortAll()
    {
        if (!brandSucceeded)
        {
            SortColor("Red");
            SortColor("Yellow");
            SortColor("White");
            SortColor("Blue");
        }

        if (!colorSucceeded)
        {
            SortBrand("Ford");
            SortBrand("BMW");
            SortBrand("Toyota");
        }

        SortYear();
        SortPower();
        SortPrice();
    }

    private List<int> GetColorList(string color)
    {
        switch (color)
        {
            case "Red":
                return redCars;
            case "Yellow":
                return yellowCars;
            case "White":
                return whiteCars;
            case "Blue":
                return blueCars;
            default:
                return new List<int>();
        }
    }

    private List<int> GetBrandList(string brand)
    {
        switch (brand)
        {
            case "Ford":
                return fordCars;
            case "BMW":
                return bmwCars;
            case "Toyota":
                return toyotaCars;
            default:
                return new List<int>();
        }
    }

    private void SortColor(string color)
    {
        List<int> colorList = GetColorList(color);

        colorList.Clear();

        int totalCarsOfColor = 3;

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null)
            {
                CarInfos carInfos = parkingSpots[i].GetComponent<CarInfos>();

                if (carInfos != null)
                {
                    string carColor = carInfos.carColor;

                    if (carColor == color)
                    {
                        colorList.Add(i);
                    }
                }
            }
        }

        if (colorList.Count == totalCarsOfColor)
        {
            colorList.Sort();
            bool isConsecutive = true;

            for (int i = 0; i < colorList.Count - 1; i++)
            {
                if (colorList[i] + 1 != colorList[i + 1])
                {
                    isConsecutive = false;
                    break;
                }
            }

            if (isConsecutive)
            {
                YouSucceededColor(color);
            }
        }
    }

    private void SortBrand(string brand)
    {
        List<int> brandList = GetBrandList(brand);

        brandList.Clear();

        int totalCarsOfBrand = 4;

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null)
            {
                CarInfos carInfos = parkingSpots[i].GetComponent<CarInfos>();

                if (carInfos != null)
                {
                    string carBrand = carInfos.carBrand;

                    if (carBrand == brand)
                    {
                        brandList.Add(i);
                    }
                }
            }
        }

        if (brandList.Count == totalCarsOfBrand)
        {
            brandList.Sort();
            bool isConsecutive = true;

            for (int i = 0; i < brandList.Count - 1; i++)
            {
                if (brandList[i] + 1 != brandList[i + 1])
                {
                    isConsecutive = false;
                    break;
                }
            }

            if (isConsecutive)
            {
                YouSucceededBrand(brand);
            }
        }
    }

    private void SortYear()
    {
        yearsList.Clear();
        consecutiveIncreasingDecreasing.Clear();

        List<(int year, int index)> yearWithIndex = new List<(int, int)>();

        int totalCars = 12;

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null)
            {
                CarInfos carInfos = parkingSpots[i].GetComponent<CarInfos>();

                if (carInfos != null)
                {
                    int carYear = carInfos.productionYear;
                    yearWithIndex.Add((carYear, i));
                    consecutiveIncreasingDecreasing.Add(i);
                }
            }
        }

        bool allCarsParked = yearWithIndex.Count == totalCars;

        if (allCarsParked)
        {
            bool allIncreasing = true;
            yearWithIndex.Sort((a, b) => a.year.CompareTo(b.year));

            for (int i = 0; i < yearWithIndex.Count - 1; i++)
            {
                if (yearWithIndex[i].year > yearWithIndex[i + 1].year)
                {
                    allIncreasing = false;
                    break;
                }
            }

            bool allDecreasing = true;
            if (!allIncreasing)
            {
                yearWithIndex.Sort((a, b) => b.year.CompareTo(a.year));
                for (int i = 0; i < yearWithIndex.Count - 1; i++)
                {
                    if (yearWithIndex[i].year < yearWithIndex[i + 1].year)
                    {
                        allDecreasing = false;
                        break;
                    }
                }
            }

            bool isConsecutive = true;
            consecutiveIncreasingDecreasing.Sort();

            for (int i = 0; i < consecutiveIncreasingDecreasing.Count - 1; i++)
            {
                if (consecutiveIncreasingDecreasing[i + 1] != consecutiveIncreasingDecreasing[i] + 1)
                {
                    isConsecutive = false;
                    break;
                }
            }

            if ((allIncreasing || allDecreasing) && isConsecutive)
            {
                foreach (var item in yearWithIndex)
                {
                    yearsList.Add(item.index);
                }
                YouSucceededParameter("year");
            }
        }
    }

    private void SortPower()
    {
        powerList.Clear();
        consecutiveIncreasingDecreasing.Clear();

        List<(int power, int index)> powerWithIndex = new List<(int, int)>();

        int totalCars = 12;

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null)
            {
                CarInfos carInfos = parkingSpots[i].GetComponent<CarInfos>();

                if (carInfos != null)
                {
                    int carPower = carInfos.motorPower;
                    powerWithIndex.Add((carPower, i));
                    consecutiveIncreasingDecreasing.Add(i);
                }
            }
        }

        bool allCarsParked = powerWithIndex.Count == totalCars;

        if (allCarsParked)
        {
            bool allIncreasing = true;
            powerWithIndex.Sort((a, b) => a.power.CompareTo(b.power));

            for (int i = 0; i < powerWithIndex.Count - 1; i++)
            {
                if (powerWithIndex[i].power > powerWithIndex[i + 1].power)
                {
                    allIncreasing = false;
                    break;
                }
            }

            bool allDecreasing = true;
            if (!allIncreasing)
            {
                powerWithIndex.Sort((a, b) => b.power.CompareTo(a.power));
                for (int i = 0; i < powerWithIndex.Count - 1; i++)
                {
                    if (powerWithIndex[i].power < powerWithIndex[i + 1].power)
                    {
                        allDecreasing = false;
                        break;
                    }
                }
            }

            bool isConsecutive = true;
            consecutiveIncreasingDecreasing.Sort();

            for (int i = 0; i < consecutiveIncreasingDecreasing.Count - 1; i++)
            {
                if (consecutiveIncreasingDecreasing[i + 1] != consecutiveIncreasingDecreasing[i] + 1)
                {
                    isConsecutive = false;
                    break;
                }
            }

            if ((allIncreasing || allDecreasing) && isConsecutive)
            {
                foreach (var item in powerWithIndex)
                {
                    powerList.Add(item.index);
                }
                YouSucceededParameter("power");
            }
        }
    }

    private void SortPrice()
    {
        priceList.Clear();
        consecutiveIncreasingDecreasing.Clear();

        List<(int price, int index)> priceWithIndex = new List<(int, int)>();

        int totalCars = 12;

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            if (parkingSpots[i] != null)
            {
                CarInfos carInfos = parkingSpots[i].GetComponent<CarInfos>();

                if (carInfos != null)
                {
                    int carPrice = carInfos.carPrice;
                    priceWithIndex.Add((carPrice, i));
                    consecutiveIncreasingDecreasing.Add(i);
                }
            }
        }

        bool allCarsParked = priceWithIndex.Count == totalCars;

        if (allCarsParked)
        {
            bool allIncreasing = true;
            priceWithIndex.Sort((a, b) => a.price.CompareTo(b.price));

            for (int i = 0; i < priceWithIndex.Count - 1; i++)
            {
                if (priceWithIndex[i].price > priceWithIndex[i + 1].price)
                {
                    allIncreasing = false;
                    break;
                }
            }

            bool allDecreasing = true;
            if (!allIncreasing)
            {
                priceWithIndex.Sort((a, b) => b.price.CompareTo(a.price));
                for (int i = 0; i < priceWithIndex.Count - 1; i++)
                {
                    if (priceWithIndex[i].price < priceWithIndex[i + 1].price)
                    {
                        allDecreasing = false;
                        break;
                    }
                }
            }

            bool isConsecutive = true;
            consecutiveIncreasingDecreasing.Sort();

            for (int i = 0; i < consecutiveIncreasingDecreasing.Count - 1; i++)
            {
                if (consecutiveIncreasingDecreasing[i + 1] != consecutiveIncreasingDecreasing[i] + 1)
                {
                    isConsecutive = false;
                    break;
                }
            }

            if ((allIncreasing || allDecreasing) && isConsecutive)
            {
                foreach (var item in priceWithIndex)
                {
                    priceList.Add(item.index);
                }
                YouSucceededParameter("price");
            }
        }
    }

    private void YouSucceededColor(string parameter)
    {

        foreach (int index in GetColorList(parameter))
        {
            string parkingPlaceNumber = index.ToString();
            Transform parkingPlace = transform.Find(parkingPlaceNumber);

            if (parkingPlace.gameObject != null)
            {
                Debug.Log($"{parkingPlace.gameObject.name}");
            }
            else
            {
                Debug.Log("Parking place isn't found :((((");
            }

            Renderer renderer = parkingPlace.GetComponentInChildren<Renderer>();

            if (renderer != null)
            {
                renderer.enabled = true;
                activeRenderers.Add(renderer);
                colorSucceeded = true;
            }
        }
    }

    private void YouSucceededBrand(string parameter)
    {
        foreach (int index in GetBrandList(parameter))
        {
            string parkingPlaceNumber = index.ToString();
            Transform parkingPlace = transform.Find(parkingPlaceNumber);

            if (parkingPlace.gameObject != null)
            {
                Debug.Log($"{parkingPlace.gameObject.name}");
            }
            else
            {
                Debug.Log("Parking place isn't found :((((");
            }

            Renderer renderer = parkingPlace.GetComponentInChildren<Renderer>();

            if (renderer != null)
            {
                renderer.enabled = true;
                activeRenderers.Add(renderer);
                brandSucceeded = true;
            }
        }
    }

    private void YouSucceededParameter(string parameter)
    {
        List<int> parameterList = GetParameterList(parameter);

        foreach (int index in parameterList)
        {
            string parkingPlaceNumber = index.ToString();
            Transform parkingPlace = transform.Find(parkingPlaceNumber);

            Renderer renderer = parkingPlace.GetComponentInChildren<Renderer>();

            if (renderer != null)
            {
                renderer.enabled = true;
                activeRenderers.Add(renderer);
            }
        }
    }

    private List<int> GetParameterList(string parameterName)
    {
        switch (parameterName)
        {
            case "year":
                return yearsList;
            case "power":
                return powerList;
            case "price":
                return priceList;
            default:
                return null;
        }
    }
}



