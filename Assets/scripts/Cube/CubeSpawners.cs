using UnityEngine;

public class CubeSpawners : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Cube _prefab;
    [SerializeField] private Exploder _exploder;

    private int _minCubesCount = 2;
    private int _maxCubesCount = 6;

    private void OnEnable()
    {
        _cube.Clicked += SpawnCubes;
    }

    private void OnDisable()
    {
        _cube.Clicked -= SpawnCubes;
    }

    private void SpawnCubes()
    {
        int countOfCubes = Random.Range(_minCubesCount, _maxCubesCount);

        Cube[] newCubes = new Cube[countOfCubes];

        for(int i = 0; i < countOfCubes; i++)
        {
            newCubes[i] = Instantiate(_prefab);
        }
        
        _exploder.ExplodeCube(newCubes);
    }
}
