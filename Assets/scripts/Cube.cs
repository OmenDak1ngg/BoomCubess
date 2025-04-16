using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private int _requiredSplitChance;
    
    private int _minSplitChance = 1;
    private int _maxSplitChance = 100;
    private int _spitChanceDecayFactor = 2;

    public event Action Clicked;

    private void Start()
    {
       _requiredSplitChance = _maxSplitChance;
    }
    public void ReduceSplitChance()
    {
        _requiredSplitChance /= _spitChanceDecayFactor;
    }

    private void OnMouseDown()
    {
        bool isSplitted = UnityEngine.Random.Range(_minSplitChance, _maxSplitChance) <= _requiredSplitChance;

        if (isSplitted)
        {
            Clicked?.Invoke();
        }
        
        Debug.Log("DESTROYED");
        Destroy(gameObject);
    }
}
