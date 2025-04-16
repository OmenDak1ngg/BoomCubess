using System.Collections.Generic;
using UnityEngine;

public class CubeSpawners : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnpoints;
    [SerializeField] private Cube _cube;
    [SerializeField] private Cube _prefab;
    [SerializeField] private Material[] _materials;

    private int _minCubesCount = 2;
    private int _maxCubesCount = 6;

    private int _scaleReduce = 2;
    private List<SpawnPoint> avalaibleSpawnPoints;

    private void Start()
    {
        avalaibleSpawnPoints = new List<SpawnPoint>(_spawnpoints);
    }

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
        int minIndex = 0;
        SpawnPoint randomSpawnPoint;

        for(int i = 0; i < countOfCubes; i++)
        {
            Cube newCube = Instantiate(_prefab);
            
            randomSpawnPoint = avalaibleSpawnPoints[Random.Range(minIndex, _spawnpoints.Length)];
            avalaibleSpawnPoints.Remove(randomSpawnPoint);
            
            newCube.ReduceSplitChance();
            newCube.transform.localPosition = randomSpawnPoint.transform.localPosition;
            newCube.transform.localScale /= _scaleReduce;
            newCube.GetComponent<Renderer>().material = _materials[Random.Range(minIndex, _materials.Length)];
        }
    }
}
