using UnityEngine;
using UnityEngine.UI;

public class XPIndicator : MonoBehaviour
{
    private Detector detector;

    private bool yellowIsSorted;
    private bool redIsSorted;
    private bool whiteIsSorted;
    private bool blueIsSorted;
    private bool fordIsSorted;
    private bool toyotaIsSorted;
    private bool bmwIsSorted;
    private bool priceIsSorted;
    private bool yearIsSorted;
    private bool powerIsSorted;

    private Image xpScale;

    [SerializeField]
    private GameObject youWonPanel;

    void Start()
    {
        xpScale = transform.GetComponent<Image>();
        xpScale.fillAmount = 0f;

        GameObject parkingPlaces = GameObject.FindWithTag("Detector");
        detector = parkingPlaces.GetComponent<Detector>();

        detector.YellowSorted += () => IncreaseYellowXP();
        detector.RedSorted += () => IncreaseRedXP();
        detector.WhiteSorted += () => IncreaseWhiteXP();
        detector.BlueSorted += () => IncreaseBlueXP();
        detector.FordSorted += () => IncreaseFordXP();
        detector.ToyotaSorted += () => IncreaseToyotaXP();
        detector.BMWSorted += () => IncreaseBmwXP();
        detector.PriceSorted += () => IncreasePriceXP();
        detector.YearSorted += () => IncreaseYearXP();
        detector.PowerSorted += () => IncreasePowerXP();
    }

    private void IncreaseYellowXP()
    {
        if (!yellowIsSorted)
        {
            yellowIsSorted = true;
            xpScale.fillAmount += 0.05f;
            CheckIfVictory();
        }
    }

    private void IncreaseRedXP()
    {
        if (!redIsSorted)
        {
            redIsSorted = true;
            xpScale.fillAmount += 0.05f;
            CheckIfVictory();
        }
    }

    private void IncreaseWhiteXP()
    {
        if (!whiteIsSorted)
        {
            whiteIsSorted = true;
            xpScale.fillAmount += 0.05f;
            CheckIfVictory();
        }
    }

    private void IncreaseBlueXP()
    {
        if (!blueIsSorted)
        {
            blueIsSorted = true;
            xpScale.fillAmount += 0.05f;
            CheckIfVictory();
        }
    }

    private void IncreaseFordXP()
    {
        if (!fordIsSorted)
        {
            fordIsSorted = true;
            xpScale.fillAmount += 0.07f;
            CheckIfVictory();
        }
    }

    private void IncreaseToyotaXP()
    {
        if (!toyotaIsSorted)
        {
            toyotaIsSorted = true;
            xpScale.fillAmount += 0.07f;
            CheckIfVictory();
        }
    }

    private void IncreaseBmwXP()
    {
        if (!bmwIsSorted)
        {
            bmwIsSorted = true;
            xpScale.fillAmount += 0.07f;
            CheckIfVictory();
        }
    }

    private void IncreasePriceXP()
    {
        if (!priceIsSorted)
        {
            priceIsSorted = true;
            xpScale.fillAmount += 0.2f;
            CheckIfVictory();
        }
    }

    private void IncreaseYearXP()
    {
        if (!yearIsSorted)
        {
            yearIsSorted = true;
            xpScale.fillAmount += 0.2f;
            CheckIfVictory();
        }
    }

    private void IncreasePowerXP()
    {
        if (!powerIsSorted)
        {
            powerIsSorted = true;
            xpScale.fillAmount += 0.2f;
            CheckIfVictory();
        }
    }

    private void CheckIfVictory()
    {
        if (xpScale.fillAmount >= 1f)
        {
            youWonPanel.SetActive(true);
        }
    }
}
