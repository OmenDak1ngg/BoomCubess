using NUnit.Framework.Internal;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Vector3 _baseScaleOfCube;
    [SerializeField] private float _maxExplotionForce = 10f;
    [SerializeField] private float _baseExplotionRadius = 5f;
    [SerializeField] private ParticleSystem _effect;

    private float _explotionRadius;
    private float _minExplotionForce = 0f;
    private float _explotionForce;

    private int _scaleReduce = 2;

    public void BlastNearbyCubes(Cube epicenter)
    {
        _explotionRadius = _baseExplotionRadius * (_baseScaleOfCube.x / epicenter.transform.localScale.x);
        _explotionForce = (_baseScaleOfCube.x / epicenter.transform.localScale.x);
        float sqrDistance;
        float sqrRadius;

        Vector3 ExplotionDirection;
        Collider[] explodableObjects = Physics.OverlapSphere(epicenter.transform.position, _explotionRadius);

        foreach (Collider collider in explodableObjects)
        {
            bool isCube = collider.TryGetComponent<Cube>(out Cube cube);

            if (isCube)
            {
                sqrDistance = Vector3.SqrMagnitude(cube.transform.position - epicenter.transform.position);

                sqrRadius = _explotionRadius * _explotionRadius;

                float currenForce = Mathf.Clamp(_explotionRadius / sqrDistance, _minExplotionForce, _maxExplotionForce);
                ExplotionDirection = (cube.transform.position - epicenter.transform.position).normalized;

                cube.Rigidbody.AddForce(currenForce * ExplotionDirection, ForceMode.Impulse);
            }
        }
    }

    public void BlastNewCubes(Cube[] newCubes, Cube parent)
    {
        foreach (Cube cube in newCubes)
        {
            cube.ReduceSplitChance(parent.Generation);
            cube.Rigidbody.AddForce(_minExplotionForce * Random.onUnitSphere, ForceMode.Impulse);

            cube.transform.localScale /= _scaleReduce;
        }
    }

    public void Explode(Cube epicenter)
    {
        Instantiate(_effect, epicenter.transform.position, epicenter.transform.rotation);

        Destroy(epicenter.gameObject);
    }
}
