using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    private float _explotionRadius = 3;
    private float _explotionForce = 2;

    private int _requiredSplitChance;
    private int _minSplitChance = 1;
    private int _maxSplitChance = 100;
    private int _spitChanceDecayFactor = 2;

    private int _generation = 0;

    public int Generation => _generation;
    public event Action Clicked;

    private void Awake()
    {
       _requiredSplitChance = _maxSplitChance;
    }

    private void OnMouseDown()
    {
        _generation++;
        bool isSplitted = UnityEngine.Random.Range(_minSplitChance, _maxSplitChance) <= _requiredSplitChance;

        if (isSplitted)
        {
            Clicked?.Invoke();
        }

        Explode();
    }

    private void Explode()
    {
        Instantiate(_effect, transform.position, transform.rotation);

        foreach(var cube in GetExplodableObjects())
        {
            cube.AddForce(_explotionForce * UnityEngine.Random.onUnitSphere, ForceMode.Impulse);
        }

        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explotionRadius);

        List<Rigidbody> cubes = new();

        foreach(Collider hit in hits)
        {
            if(hit.attachedRigidbody != null) 
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }

    public void ReduceSplitChance(int generation)
    {
        _generation = generation;
        _requiredSplitChance = _maxSplitChance / (int)Math.Pow(_spitChanceDecayFactor, generation);
        Debug.Log("Chance" + _requiredSplitChance);
    }
}
