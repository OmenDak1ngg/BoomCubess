using UnityEngine;

public class CubeClicker : MonoBehaviour
{
    private static int LeftMouseButton = 0;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                bool isCube = hit.collider.TryGetComponent<Cube>(out Cube cube);

                if(isCube)
                {
                    cube.HandleClickAndSplit();
                }
            }
        }
    }
}
