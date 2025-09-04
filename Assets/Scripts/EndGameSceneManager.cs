using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndGameSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playIcon;
    private Image playIconImage;

    void Awake()
    {
        DOTween.Init();
    }

    void Start()
    {
        playIconImage = playIcon.GetComponent<Image>();
    }

    void Update()
    {
        // playIconImage.DOColor(Color.yellow, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}
