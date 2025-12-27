using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private float _maxSplitChance = 1.0f;
    
    private ColorChanger _colorChanger;

    public float CurrentSplitChance { get; private set; } = 1.0f;
    
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    public void Initialize(Vector3 scale, float splitChance)
    {
        transform.localScale = scale;
        CurrentSplitChance = Mathf.Clamp(splitChance, 0, _maxSplitChance);
        
        _colorChanger.Change();
    }
}