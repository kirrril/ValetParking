using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SafetyIndicator : MonoBehaviour
{
    private float safetyScale = 1f;

    [SerializeField]
    private CarManager carManager;

    [SerializeField]
    private GameObject youreFiredPanel;

    [SerializeField]
    private GameObject youreFiredBackground;

    public GameObject car;
    public GameObject carBody;
    private GameObject carTrigger;
    private CollisionHandler collisionHandler;

    void OnEnable()
    {
        car = carManager.selectedCar;
        if (car != null)
        {
            carBody = car.transform.Find("CarBody").gameObject;
            carTrigger = car.transform.Find("CarTrigger").gameObject;
            collisionHandler = carTrigger.GetComponent<CollisionHandler>();
        }

        collisionHandler.crush += HandleBumps;
    }

    void OnDisable()
    {
         collisionHandler.crush -= HandleBumps;
    }

    private void HandleBumps()
    {
        if (safetyScale > 0)
        {
            safetyScale -= 0.1f;
            transform.GetComponent<Image>().fillAmount = safetyScale;

            if (safetyScale <= 0.3f)
            {
                transform.GetComponent<Image>().color = Color.red;
            }
        }
        else
        {
            StartCoroutine(FiredCoroutine());
        }
    }

    private IEnumerator FiredCoroutine()
    {
        youreFiredPanel.SetActive(true);
        Image backgroundImage = youreFiredBackground.GetComponent<Image>();

        for (int i = 0; i < 4; i++)
        {
            backgroundImage.color = new Color(1, 0, 0, 0.7f);
            yield return new WaitForSeconds(0.5f);
            backgroundImage.color = new Color(1, 0, 0, 0.3f);
            Handheld.Vibrate();
            yield return new WaitForSeconds(0.5f);
        }
        Application.Quit();
    }
}
