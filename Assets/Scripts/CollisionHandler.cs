using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action crush;
    public event Action triggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            crush?.Invoke();
            Handheld.Vibrate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            triggerExit?.Invoke();
        }
    }
}
