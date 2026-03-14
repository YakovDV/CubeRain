using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _platforms;

    public event Action<Cube> HitPlatform;

    public void Register(Cube cube)
    {
        cube.FirstCollisionHappened += HandleCollision;
    }

    public void Unregister(Cube cube)
    {
        cube.FirstCollisionHappened -= HandleCollision;
    }

    private void HandleCollision(Cube cube, Collision collision)
    {
        if ((_platforms.value & (1 << collision.gameObject.layer)) == 0 || cube.HasFirstCollision == true)
        {
            return;
        }

        cube.ChangeCollisionStatus();

        HitPlatform?.Invoke(cube);
    }
}