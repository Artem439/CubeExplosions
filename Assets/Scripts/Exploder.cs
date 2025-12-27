using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 5f;

    public void Explode(IEnumerable<Rigidbody> rigidbodies, Vector3 center)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
            if (rigidbody != null)
                rigidbody.AddExplosionForce(_explosionForce, center, _explosionRadius, 1);
    }
}
