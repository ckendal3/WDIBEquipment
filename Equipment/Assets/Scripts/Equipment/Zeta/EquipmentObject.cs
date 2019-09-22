using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Equipment", fileName = "Equipment")]
public class EquipmentObject : ScriptableObject
{
    [Tooltip("Name of the Equipment")]
    public string Name;

    [Tooltip("Visuals")]
    public VisualComponent InnerVisuals;
    public VisualComponent OuterVisuals;

    [SerializeReference] public List<IComponentData> components;

    [Tooltip("Emitting Particle Effect")]
    public ParticleSystem ParticleSystem;

    private void OnEnable()
    {
        components = new List<IComponentData>();
        components.Add(new Equipment { Type = EquipmentType.Deployable, ID = 0 });
        components.Add(new DelayedActivation { Value = .5f });
        components.Add(new AreaOfEffect { Shape = ShapeType.Sphere, Size = new float3(5f, 5f, 5f) });
        components.Add(new LifeTime { Value = 15f });
        components.Add(new Health { Value = 15f, MaxValue = 15f });
        components.Add(new Heal { Value = 15f, Rate = 1.2f });
        components.Add(new Damage { Value = 15f, Rate = 1.2f });
    }
}
