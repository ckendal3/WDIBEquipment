using Unity.Mathematics;
using UnityEngine;

public class EquipmentHybrid : MonoBehaviour
{
    [SerializeField]
    public EquipmentData data;

    public void Start()
    {

    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateEquipment();
        }
    }

    private void CreateEquipment()
    {
        GameObject equipment = EquipmentFactory.CreateEquipment(new float3(0f, 0f, 0f), data);

        Rigidbody rb = equipment.GetComponent<Rigidbody>();
        rb.AddForce(new float3(7.5f, 2f, 7.5f), ForceMode.Impulse);
    }
}
