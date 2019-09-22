using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[SerializeField]
public struct EquipmentData
{
    [Tooltip("Name of the Equipment")]
    public string Name;

    [Tooltip("Visuals")]
    public VisualComponent Visuals;

    [Tooltip("List of components")]
    [SerializeReference] public List<IComponentData> Components;

    [Tooltip("Emitting Particle Effect")]
    public ParticleSystem ParticleSystem;
}
