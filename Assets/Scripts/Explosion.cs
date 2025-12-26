using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 5f;

    public void Explode(GameObject cube)
    {
        Vector3 center = cube.transform.position;
        
        Collider[] colliders = Physics.OverlapSphere(center, explosionRadius);

        foreach (var col in colliders)
        {
            if (col.CompareTag("Cube"))
            {
                Rigidbody rigidbody = col.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    Vector3 direction = (col.transform.position - center).normalized;
                    float distance = Vector3.Distance(col.transform.position, center);
                    float force = explosionForce * (1f - Mathf.Clamp01(distance / explosionRadius));
                    float scale = col.transform.localScale.x;
                    force *= (1f / scale);

                    rigidbody.AddForce(direction * force, ForceMode.Impulse);
                }
            }
        }

        Destroy(cube);
    }
}
