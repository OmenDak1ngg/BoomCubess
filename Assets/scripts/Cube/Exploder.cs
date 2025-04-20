using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Cube _parent;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private Vector3 _baseScaleOfCube;
    [SerializeField] private float _maxExplotionForce = 10f;
    [SerializeField] private float _minExplotionForce = 1f;

    private float _baseExplotionRadius = 2f;
    private float _explotionRadius;

    private float _explotionForce;

    private int _scaleReduce = 2;

    public void ExplodeCubes()
    {
        _explotionRadius = _baseExplotionRadius * (_baseScaleOfCube.x / _parent.transform.localScale.x);
        _explotionForce = _minExplotionForce * (_baseScaleOfCube.x / _parent.transform.localScale.x);
        float distance;
        Vector3 ExplotionDirection;
        Collider[] explodableObjects = Physics.OverlapSphere(_parent.transform.position, _explotionRadius);

        foreach (Collider collider in explodableObjects)
        {
            if (collider.TryGetComponent(out Cube cube))
            {
                distance = Vector3.Distance(_parent.transform.position, collider.transform.position);
                _explotionForce *= Mathf.Clamp(_explotionRadius / distance, _minExplotionForce, _maxExplotionForce);
                ExplotionDirection = (collider.transform.position - _parent.transform.position).normalized;

                cube.Rigidbody.AddForce(_explotionForce * ExplotionDirection, ForceMode.Impulse);
            }
        }
    }

    public void ExplodeNewCubes(Cube[] newCubes)
    {
        foreach (Cube cube in newCubes)
        {
            cube.ReduceSplitChance(_parent.Generation);
            cube.Rigidbody.AddForce(_minExplotionForce * Random.onUnitSphere, ForceMode.Impulse);

            cube.transform.localScale /= _scaleReduce;
            cube.Renderer.material = _colorChanger.SetRandomMaterial();
        }
    }
}
