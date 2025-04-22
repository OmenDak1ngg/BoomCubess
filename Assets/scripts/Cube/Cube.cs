using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] public Exploder Exploder { get; private set; }

    private int _requiredSplitChance;
    private int _minSplitChance = 1;
    private int _maxSplitChance = 100;
    private int _spitChanceDecayFactor = 2;
    private Explotion _explotion;

    private int _generation = 0;

    public event Action Clicked;

    public Rigidbody Rigidbody { get; private set; }
    public Renderer Renderer { get; private set; }
    public int Generation => _generation;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
        _explotion = GetComponentInChildren<Explotion>();
       _requiredSplitChance = _maxSplitChance;
    }

    public void HandleClickAndSplit()
    {
        _generation++;
        bool canSplit = UnityEngine.Random.Range(_minSplitChance, _maxSplitChance) <= _requiredSplitChance;

        if (canSplit)
        {
            Clicked?.Invoke();

        }
        else
        {
            Exploder.ExplodeCubes();
        }

        _explotion.Explode();
    }

    public void ReduceSplitChance(int generation)
    {
        _generation = generation;
        _requiredSplitChance = _maxSplitChance / (int)Math.Pow(_spitChanceDecayFactor, generation);
    }
}
