using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorChanger))]
public class SpawnerCubes : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    
    private ColorChanger _colorChanger;

    public List<Rigidbody> SpawnCubes(Vector3 center, float parentScale)
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();
        Rigidbody rb;
        GameObject cube;
        
        int newCubesCount = Random.Range(2, 7);
        float newScale = parentScale / 2f;

        for (int i = 0; i < newCubesCount; i++)
        {
            cube = CreateNewCube(center, newScale);
            
            if (cube.TryGetComponent(out rb ))
                rigidbodies.Add(rb);
        }
        
        return rigidbodies;
    }

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private GameObject CreateNewCube(Vector3 center, float scale)
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized * 1f;

        GameObject newCube = Instantiate(_cubePrefab, center + randomOffset, Quaternion.identity);
        newCube.transform.localScale = Vector3.one * scale;
        
        _colorChanger.ColorChange();
        
        Rigidbody rb = newCube.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = newCube.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
        
        return newCube;
    }
}
