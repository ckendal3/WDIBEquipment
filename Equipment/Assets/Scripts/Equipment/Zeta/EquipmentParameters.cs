using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameters/Equipment", fileName = "EquipmentParameters")]
public class EquipmentParameters : ScriptableObject
{
    [Header("Universal Mesh and Materials")]
    public Mesh StationaryMesh;
    public Material StationaryMaterial;

    public Mesh DeployableMesh;
    public Material DeployableMaterial;

    [Header("Equipment Hit Layer")]
    public LayerMask layerMask;

    [Header("List of Equipment")]
    public List<EquipmentObject> Equipments;
}
