using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class CarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject driveControl;

    public GameObject selectedCar;

    [SerializeField]
    private GameObject carInfoDisplay;

    [SerializeField]
    private TMP_Text brandAndModel;

    [SerializeField]
    private TMP_Text motorPower;

    [SerializeField]
    private TMP_Text price;

    void Start()
    {
        RayGun.Instance.CarSelected += GetCarToDrive;
        RayGun.Instance.CarDeselected += DeselectCar;
    }

    private void GetCarToDrive(GameObject receivedCar)
    {
        selectedCar = receivedCar;
        HighlightSelected(receivedCar);
        DisplayCarInfos(receivedCar);
        driveControl.SetActive(true);
    }

    private void HighlightSelected(GameObject gameObject)
    {
        Transform selectionShellTransform = gameObject.transform.Find("SelectShell");
        if (selectionShellTransform != null)
        {
            GameObject selectionShell = selectionShellTransform.gameObject;
            selectionShell.SetActive(true);
        }
    }

    private void DisplayCarInfos(GameObject receivedCar)
    {
        carInfoDisplay.SetActive(true);
        CarInfos carInfos = receivedCar.GetComponent<CarInfos>();
        brandAndModel.text = $"<b>{carInfos.carBrand}</b> {carInfos.modelName} <b>{carInfos.productionYear}</b>";
        motorPower.text = $"{carInfos.motorPower} <b>hp</b>";
        price.text = $"{carInfos.carPrice} <b>€</b>";
    }

    public void DeselectCar(GameObject receivedCar)
    {
        Transform selectionShellTransform = receivedCar.transform.Find("SelectShell");
        if (selectionShellTransform != null)
        {
            GameObject selectionShell = selectionShellTransform.gameObject;
            selectionShell.SetActive(false);
        }

        driveControl.SetActive(false);

        carInfoDisplay.SetActive(false);

        selectedCar = null;
    }
}
