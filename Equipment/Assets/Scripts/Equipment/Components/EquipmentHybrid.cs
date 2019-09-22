using Unity.Mathematics;
using UnityEngine;

public class EquipmentHybrid : MonoBehaviour
{
    [SerializeField]
    public EquipmentObject equipObject;

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
        GameObject equipment = EquipmentFactory.CreateEquipment(new float3(0f, 0f, 0f), equipObject);

        Rigidbody rb = equipment.GetComponent<Rigidbody>();
        rb.AddForce(new float3(7.5f, 2f, 7.5f), ForceMode.Impulse);
    }
}
