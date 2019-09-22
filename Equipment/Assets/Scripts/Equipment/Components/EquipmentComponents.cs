using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct FallOff : IComponentData
{
    // TODO: Implement Falloff Component
}


[Serializable]
public struct ForceApplier : IComponentData
{
    public float Force;
    public float Rate;
}

[Serializable]
public struct ForceApplyNormalsDirection : IComponentData
{
    public float Force;
    public float Rate;
}


[Serializable]
public struct Damage : IComponentData
{
    public float Value;
    public float Rate;
}

[Serializable]
public struct Heal : IComponentData
{
    public float Value;
    public float Rate;
}

[Serializable]
public struct Health : IComponentData
{
    public float Value;
    public float MaxValue;

    public bool IsAlive()
    {
        return Value > 0;
    }

    public static implicit operator bool(Health health)
    {
        return health.Value > 0;
    }
}

[Serializable]
public struct LifeTime : IComponentData
{
    public float Value;
}

[Serializable]
public struct DelayedActivation : IComponentData
{
    public float Value;
}


[Serializable]
public struct Equipment : IComponentData
{
    public EquipmentType Type;
    public int ID;
}

public enum EquipmentType
{
    PowerUps,
    Deployable,
    Throwable,
    Abilities
}

// TODO: Make visuals a struct
[Serializable]
public class VisualComponent
{
    public Mesh Mesh;
    public Material Material;
}

[Serializable]
public struct AreaOfEffect : IComponentData
{
    public ShapeType Shape;
    public float3 Size;
}

public enum ShapeType
{
    Sphere, 
    Box, 
    Capsule, 
    Cylinder
}