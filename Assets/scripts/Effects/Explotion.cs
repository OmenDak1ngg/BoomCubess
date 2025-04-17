using UnityEngine;

public class Explotion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private Transform _explodableObjec;

    private void Awake()
    {
        _explodableObjec = transform.parent;
    }

    public void Explode()
    {
        Instantiate(_effect, _explodableObjec.position, _explodableObjec.rotation);

        Destroy(_explodableObjec.gameObject);
    }
}
