using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private Cube _prefab;

    private int _minCubesCount = 2;
    private int _maxCubesCount = 6;

    public Cube[] SpawnAndGetCubes()
    {
        int countOfCubes = Random.Range(_minCubesCount, _maxCubesCount);
        Cube[] newCubes = new Cube[countOfCubes];

        for (int i = 0; i < countOfCubes; i++)
        {
            newCubes[i] = Instantiate(_prefab);
            newCubes[i].Renderer.material = _colorChanger.SetRandomMaterial();
        }

        return newCubes;
    }
}
