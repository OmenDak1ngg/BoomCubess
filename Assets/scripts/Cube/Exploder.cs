using NUnit.Framework.Internal;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Vector3 _baseScaleOfCube;
    [SerializeField] private float _maxExplotionForce = 10f;
    [SerializeField] private float _minExplotionForce = 1f;
    [SerializeField] private ParticleSystem _effect;

    private float _baseExplotionRadius = 2f;
    private float _explotionRadius;

    private float _explotionForce;

    private int _scaleReduce = 2;

    public void ExplodeCubes(Cube epicenter)
    {
        _explotionRadius = _baseExplotionRadius * (_baseScaleOfCube.x / epicenter.transform.localScale.x);
        _explotionForce = _minExplotionForce * (_baseScaleOfCube.x / epicenter.transform.localScale.x);
        float distance;
        Vector3 ExplotionDirection;
        Collider[] explodableObjects = Physics.OverlapSphere(epicenter.transform.position, _explotionRadius);

        foreach (Collider collider in explodableObjects)
        {
            bool isCube = collider.TryGetComponent<Cube>(out Cube cube);

            if (isCube)
            {
                distance = Vector3.SqrMagnitude(cube.transform.position - collider.transform.position);
                _explotionForce *= Mathf.Clamp(_explotionRadius / distance, _minExplotionForce, _maxExplotionForce);
                ExplotionDirection = (collider.transform.position - cube.transform.position).normalized;

                cube.Rigidbody.AddForce(_explotionForce * ExplotionDirection, ForceMode.Impulse);
            }
        }
    }

    public void ExplodeNewCubes(Cube[] newCubes, Cube parent)
    {
        foreach (Cube cube in newCubes)
        {
            cube.ReduceSplitChance(parent.Generation);
            cube.Rigidbody.AddForce(_minExplotionForce * Random.onUnitSphere, ForceMode.Impulse);

            cube.transform.localScale /= _scaleReduce;
        }
    }

    public void EffectExplode(Cube epicenter)
    {
        Instantiate(_effect, epicenter.transform.position, epicenter.transform.rotation);

        Destroy(epicenter);
    }
}
