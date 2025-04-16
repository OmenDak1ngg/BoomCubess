using UnityEngine;

public class ColorChanger : MonoBehaviour 
{
    [SerializeField] private Material[] _materials;
    
    private int _minIndex = 0;
    
    public  Material SetRandomMaterial()
    {
        return _materials[Random.Range(_minIndex, _materials.Length)];
    }
}
