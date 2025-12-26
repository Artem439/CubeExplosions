using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Raycaster))]
public class SpawnerCubes : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 5f;

    public void SpawnCubes(Vector3 center, float parentScale)
    {
        List<GameObject> spawnedCubes = new List<GameObject>();
        
        int newCubesCount = Random.Range(2, 7);
        float newScale = parentScale / 2f;

        for (int i = 0; i < newCubesCount; i++)
        {
            CreateNewCube(center, newScale);
        }

        ApplyExplosionForce(spawnedCubes, center, newScale);
    }

    private void CreateNewCube(Vector3 center, float scale)
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized * 1f;

        GameObject newCube = Instantiate(cubePrefab, center + randomOffset, Quaternion.identity);
        newCube.transform.localScale = Vector3.one * scale;

        newCube.tag = "Cube";
        
        Renderer renderer = newCube.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Random.ColorHSV();
        }

        Rigidbody rb = newCube.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = newCube.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
    }

    private void ApplyExplosionForce(List<GameObject> cubes, Vector3 center, float scale)
    {
        foreach (GameObject cube in cubes)
        {
            Rigidbody rb = cube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (cube.transform.position - center).normalized;
                float distance = Vector3.Distance(cube.transform.position, center);
                float force = explosionForce * (1f - Mathf.Clamp01(distance / explosionRadius));
                force *= 1f / Mathf.Max(scale, 0.1f);

                rb.AddForce(direction * force, ForceMode.Impulse);
            }
        }
    }
}
