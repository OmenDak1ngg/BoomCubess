using UnityEngine;
using UnityEngine.UIElements;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Cube _parent;
    [SerializeField] private ColorChanger _colorChanger;
    
    private int _explotionForce;
    private int _scaleReduce = 2;

    public void ExplodeCube(Cube[] explodableObjects)
    {
        foreach (Cube cube in explodableObjects)
        {
            cube.ReduceSplitChance(_parent.Generation);
            cube.Rigidbody.AddForce(_explotionForce * Random.onUnitSphere, ForceMode.Impulse);
            cube.transform.localScale /= _scaleReduce;
            cube.Renderer.material = _colorChanger.SetRandomMaterial();
        }
    }
}
