using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cars", menuName = "Scriptable Objects/Cars")]
public class Cars : ScriptableObject
{
    public List<Car> carsList;
}

[System.Serializable]
public class Car
{
    public GameObject prefab3D;
    public string carBrand;
    public string modelName;
    public int productionYear;
    public int motorPower;
    public int carPrice;
    public string carColor;
}
