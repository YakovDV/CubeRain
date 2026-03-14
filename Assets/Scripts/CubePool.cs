using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _poolCapacity = 120;
    [SerializeField] private int _poolMaxSize = 120;

    private ObjectPool<Cube> _pool;

    public int PoolCapacity => _poolCapacity;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) =>
            {
                Destroy(cube.gameObject);
            },
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    public Cube GetCube()
    {
        return _pool.Get();
    }

    public void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.ResetStats();
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.ResetStats();
        cube.gameObject.SetActive(false);
    }
}