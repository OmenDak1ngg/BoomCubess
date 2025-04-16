using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

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

        Destroy(gameObject);
    }

    public void ReduceSplitChance(int generation)
    {
        _generation = generation;
        _requiredSplitChance = _maxSplitChance / (int)Math.Pow(_spitChanceDecayFactor, generation);
    }
}
