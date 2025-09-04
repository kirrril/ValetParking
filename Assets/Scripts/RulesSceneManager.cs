using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesSceneManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DisplayRules());
    }

    private IEnumerator DisplayRules()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("GameScene");
    }
}
