using UnityEngine;

public class CubeSpawners : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Cube _prefab;
    [SerializeField] private ColorChanger _colorChanger;

    private float _explotionForce = 2;
    private int _minCubesCount = 2;
    private int _maxCubesCount = 6;

    private int _scaleReduce = 2;

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

        for(int i = 0; i < countOfCubes; i++)
        {
            Cube newCube = Instantiate(_prefab);
            
            newCube.ReduceSplitChance(_cube.Generation);
            newCube.GetComponent<Rigidbody>().AddForce(_explotionForce * Random.onUnitSphere, ForceMode.Impulse);
            newCube.transform.localScale /= _scaleReduce;
            newCube.GetComponent<Renderer>().material = _colorChanger.SetRandomMaterial();
        }
    }
}
