using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 10f;

    public void Explode(IEnumerable<Rigidbody> rigidbodies)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            Vector3 center = rb.transform.position;
            
            if (rb != null)
            {
                Vector3 direction = (transform.position - center).normalized;
                rb.AddForce(direction * _explosionForce, ForceMode.Impulse);
            }
        }
    }
}
