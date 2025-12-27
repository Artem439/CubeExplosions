using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    
    [SerializeField] private int _maxCount = 6;
    [SerializeField] private int _minCount = 2;
    
    [SerializeField] private float _scaleDivider = 2f;
    [SerializeField] private float _splitChanceDivider = 2f;

    private void OnValidate()
    {
        if (_maxCount < 0 && _maxCount <= _minCount)
            _maxCount = _minCount + 1;
        
        if (_minCount < 0)
            _minCount = 1;
        
        if (_scaleDivider < 0)
            _scaleDivider = 1;
        
        if (_splitChanceDivider < 0)
            _splitChanceDivider = 1;
    }
    
    public IEnumerable<Cube> Spawn(Cube parent)
    {
        List<Cube> cubes = new ();

        int count = Random.Range(_minCount,  _maxCount + 1);

        Vector3 newScale = parent.transform.localScale / _scaleDivider;
        float newSplitChance = parent.CurrentSplitChance / _splitChanceDivider;

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, parent.transform.position, Quaternion.identity);
            
            newCube.Initialize(newScale, newSplitChance);
            
            cubes.Add(newCube);
        }
        
        return cubes;
    }
}
