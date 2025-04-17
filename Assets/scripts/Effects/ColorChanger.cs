using UnityEngine;

public class ColorChanger : MonoBehaviour 
{
    private static int MinIndex = 0;
    
    [SerializeField] private Material[] _materials;
    
    public Material SetRandomMaterial()
    {
        return _materials[Random.Range(MinIndex, _materials.Length)];
    }
}
