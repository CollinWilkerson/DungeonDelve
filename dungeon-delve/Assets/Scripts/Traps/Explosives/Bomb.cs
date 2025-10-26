using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask playerLayer;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(Physics.CheckSphere(transform.position, explosionRadius, playerLayer))
        {
            FindAnyObjectByType<TrapBase>().Fail();
        }
        Destroy(gameObject);
    }
}
