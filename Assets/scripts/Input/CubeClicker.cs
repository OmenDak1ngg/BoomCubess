using UnityEngine;

public class CubeClicker : MonoBehaviour
{
    private static int LeftMouseButton = 0;
    private Cube[] _newCubes;

    private Camera _camera;
    private CubeSpawner _cubeSpawner;
    private Exploder _exploder;

    private void Start()
    {
        _exploder = FindFirstObjectByType<Exploder>();
        _cubeSpawner = FindFirstObjectByType<CubeSpawner>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                bool isCube = hit.collider.TryGetComponent<Cube>(out Cube cube);

                if(isCube)
                {
                    if (cube.CanSplit())
                    {
                        _newCubes = _cubeSpawner.SpawnAndGetCubes();
                        _exploder.ExplodeNewCubes(_newCubes, cube);
                    }
                    else
                    {
                        _exploder.ExplodeCubes(cube);
                    }

                    _exploder.EffectExplode(cube);
                }
            }
        }
    }
}
