using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private int _requiredSplitChance;
    private int _minSplitChance = 1;
    private int _maxSplitChance = 100;
    private int _spitChanceDecayFactor = 2;
    private int _generation = 0;
    private Explotion _explotion;

    public event Action Clicked;

    public int Generation => _generation;

    private void Awake()
    {
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

        _explotion.Explode();
    }

    public void ReduceSplitChance(int generation)
    {
        _generation = generation;
        _requiredSplitChance = _maxSplitChance / (int)Math.Pow(_spitChanceDecayFactor, generation);
    }
}
