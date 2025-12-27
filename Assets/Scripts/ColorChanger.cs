using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class ColorChanger : MonoBehaviour
{
    private GameObject _cubePrefab;
    
    public void ColorChange()
    {
        Renderer component = _cubePrefab.GetComponent<Renderer>();
        if (component != null)
        {
            component.material.color = Random.ColorHSV();
        }
    }

    private void Awake()
    {
        _cubePrefab = GetComponent<GameObject>();
    }
}
