using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action HitPlatform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null)
        {
            HitPlatform?.Invoke();
        }
    }
}