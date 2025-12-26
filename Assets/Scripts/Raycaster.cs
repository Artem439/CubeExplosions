using System;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class Raycaster : MonoBehaviour
{
    public Action<GameObject> OnObjectHit;
    
    private InputReader _inputReader;
    
    [SerializeField] private Camera _camera;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }
    
    private void OnEnable()
    {
        _inputReader.MouseButtonClicked += ReyCasting;
    }

    private void OnDisable()
    {
        _inputReader.MouseButtonClicked -= ReyCasting;
    }

    private void ReyCasting()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            OnObjectHit?.Invoke(hitObject);
        }
    }
}
