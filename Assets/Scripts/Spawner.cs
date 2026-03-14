using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnCenter = new(0f, 5f, 0f);
    [SerializeField] private Vector3 _spawnScale = new(8f, 1f, 8f);
    [SerializeField] private float _delay = 0.5f;

    [SerializeField] private float _minObjectLife = 2;
    [SerializeField] private float _maxObjectLife = 5;

    [SerializeField] private CubePool _cubePool;

    private void Start()
    {
        StartCoroutine(SpawnCubes(_cubePool.PoolCapacity, _delay));
    }

    private void ActivateCube()
    {
        Cube cube = _cubePool.GetCube();
        cube.ResetStats();

        Vector3 spawnPoint = CalculateSpawnPoint();

        cube.transform.position = spawnPoint;
        cube.transform.rotation = Quaternion.identity;

        cube.FirstPlatformCollision += StartLifetime;
    }

    private Vector3 CalculateSpawnPoint()
    {
        float spawnPointX = Random.Range(-_spawnScale.x / 2, _spawnScale.x / 2);
        float spawnPointY = _spawnCenter.y;
        float spawnPointZ = Random.Range(-_spawnScale.z / 2, _spawnScale.z / 2);

        Vector3 spawnPoint = new(spawnPointX, spawnPointY, spawnPointZ);

        return spawnPoint;
    }

    private void StartLifetime(Cube cube)
    {
        float lifetime = Random.Range(_minObjectLife, _maxObjectLife);

        StartCoroutine(WaitLifetime(lifetime, cube));
    }

    private IEnumerator WaitLifetime(float seconds, Cube cube)
    {
        yield return new WaitForSecondsRealtime(seconds);

        cube.FirstPlatformCollision -= StartLifetime;
        cube.ResetStats();
        _cubePool.ReleaseCube(cube);
        ActivateCube();
    }

    private IEnumerator SpawnCubes(int count, float delay)
    {
            for (int i = 0; i < count; i++)
            {
                ActivateCube();
                yield return new WaitForSecondsRealtime(delay);
            }
    }
}