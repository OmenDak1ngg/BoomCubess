using System;
using UnityEditor.Build;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Transform _parent;

    private void Awake()
    {
        ReduceScale();
        transform.GetComponent<Renderer>().enabled = false;
    }

    [ContextMenu(nameof(ReduceScale))]
    private void ReduceScale()
    {
        _parent = transform.parent;
        transform.localScale = _parent.localScale / 2;
    }
}
