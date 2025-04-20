using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;

    private int _requiredSplitChance;
    private int _minSplitChance = 1;
    private int _maxSplitChance = 100;
    private int _spitChanceDecayFactor = 2;
    private Explotion _explotion;

    private int _generation = 0;
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event Action Clicked;

    public Exploder Exploder => _exploder;
    public Rigidbody Rigidbody => _rigidbody;
    public Renderer Renderer => _renderer;
    public int Generation => _generation;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody  = GetComponent<Rigidbody>();
        _explotion = GetComponentInChildren<Explotion>();
       _requiredSplitChance = _maxSplitChance;
    }

    public void OnClicked()
    {
        _generation++;
        bool isSplitted = UnityEngine.Random.Range(_minSplitChance, _maxSplitChance) <= _requiredSplitChance;

        if (isSplitted)
        {
            Clicked?.Invoke();

        }
        else
        {
            _exploder.ExplodeCubes();
        }

        _explotion.Explode();
    }

    public void ReduceSplitChance(int generation)
    {
        _generation = generation;
        _requiredSplitChance = _maxSplitChance / (int)Math.Pow(_spitChanceDecayFactor, generation);
    }
}
