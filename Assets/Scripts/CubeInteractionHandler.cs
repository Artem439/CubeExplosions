using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Raycaster))]
[RequireComponent(typeof(CubeSpawner))]
[RequireComponent(typeof(Exploder))]
public class CubeInteractionHandler : MonoBehaviour
{
    private Raycaster _raycaster;
    private CubeSpawner _cubeSpawner;
    private Exploder _exploder;

    private void Awake()
    {
        _raycaster = GetComponent<Raycaster>();
        _cubeSpawner = GetComponent<CubeSpawner>();
        _exploder = GetComponent<Exploder>();
    }
    
    private void OnEnable()
    {
        _raycaster.CubeHit += HandleHit;
    }

    private void OnDisable()
    {
        _raycaster.CubeHit -= HandleHit;
    }

    private void HandleHit(Cube cube)
    {
        if (Random.value <= cube.CurrentSplitChance)
        {
            IEnumerable<Cube> cubes = _cubeSpawner.Spawn(cube);
            _exploder.Explode(cubes.Select(cube => cube.Rigidbody), cube.transform.position);
        }
        
        Destroy(cube.gameObject);
    }
}
