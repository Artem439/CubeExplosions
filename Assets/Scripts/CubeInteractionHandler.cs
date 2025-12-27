using UnityEngine;

[RequireComponent(typeof(Raycaster))]
[RequireComponent(typeof(SpawnerCubes))]
[RequireComponent(typeof(Explosion))]
public class CubeInteractionHandler : MonoBehaviour
{
    private Raycaster _raycaster;
    private SpawnerCubes _spawner;
    private Explosion _explosion;
    
    private float _currentSpawnChance = 1.0f;

    private void Awake()
    {
        _raycaster = GetComponent<Raycaster>();
        _spawner = GetComponent<SpawnerCubes>();
        _explosion = GetComponent<Explosion>();
    }
    
    private void OnEnable()
    {
        if (_raycaster != null)
            _raycaster.OnObjectHit += OnObjectClicked;
    }

    private void OnDisable()
    {
        if (_raycaster != null)
            _raycaster.OnObjectHit -= OnObjectClicked;
    }

    private void OnObjectClicked(GameObject obj)
    {
        if (obj.CompareTag("Cube"))
        {
            float randomValue = Random.value;

            if (randomValue <= _currentSpawnChance)
            {
                float scale = obj.transform.localScale.x;
                _spawner.SpawnCubes(obj.transform.position, scale);
                _currentSpawnChance = 1.0f;
                
                Destroy(obj);
            }
            else
            {
                float scale = obj.transform.localScale.x;
                _explosion.Explode(_spawner.SpawnCubes(obj.transform.position, scale));
                _currentSpawnChance /= 2f;
            }
        }
    }
}
