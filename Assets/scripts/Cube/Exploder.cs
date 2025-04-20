using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Cube _parent;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private Vector3 _baseScaleOfCube;

    private float _baseExplotionRadius = 2f;
    private float _explotionRadius;

    private float _minExplotionForce = 1f;
    private float _maxExplotionForce = 100f;
    private float _baseExplotionForce = 2f;
    private float _explotionForce;

    private int _scaleReduce = 2;

    public void ExplodeCubes(Cube[] newCubes)
    {
        _explotionRadius = _baseExplotionRadius * (_baseScaleOfCube.x / _parent.transform.localScale.x);
        _explotionForce = _baseExplotionForce * (_baseScaleOfCube.x / _parent.transform.localScale.x);
        float distance;

        Collider[] explodableObjects = Physics.OverlapSphere(_parent.transform.position, _explotionRadius);


        foreach (Collider collider in explodableObjects)
        {
            if (collider.TryGetComponent(out Cube cube))
            {
                cube.ReduceSplitChance(_parent.Generation);

                distance = Vector3.Distance(_parent.transform.position, collider.transform.position);
                _explotionForce *= Mathf.Clamp(_explotionRadius / distance, _minExplotionForce, _maxExplotionForce);

                cube.Rigidbody.AddForce(_explotionForce * Random.onUnitSphere, ForceMode.Impulse);

                cube.transform.localScale /= _scaleReduce;
                cube.Renderer.material = _colorChanger.SetRandomMaterial();
            }
        }

        ExplodeNewCubes(newCubes);
    }

    public void ExplodeNewCubes(Cube[] newCubes)
    {
        foreach (Cube cube in newCubes)
        {
            cube.ReduceSplitChance(_parent.Generation);
            cube.Rigidbody.AddForce(_baseExplotionForce * Random.onUnitSphere, ForceMode.Impulse);
            cube.transform.localScale /= _scaleReduce;
            cube.Renderer.material = _colorChanger.SetRandomMaterial();
        }
    }
}
