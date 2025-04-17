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
                Cube cube = hit.collider.GetComponent<Cube>();

                if(cube != null)
                {
                    cube.OnClicked();
                }
            }
        }
    }
}
