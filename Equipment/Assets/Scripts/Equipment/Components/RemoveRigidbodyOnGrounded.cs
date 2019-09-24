using UnityEngine;

public class RemoveRigidbodyOnGrounded : MonoBehaviour
{
    private Rigidbody rb;
    private Ray ray;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if(rb == null)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        ray = new Ray(transform.position, Vector3.down);

        // TODO: Get Collision layer (preferably ground/environmental)
        if(Physics.Raycast(ray, 0.55f, LayerMask.NameToLayer("Everything")))
        {
            Destroy(rb);
            Destroy(this);
        }
    }
}