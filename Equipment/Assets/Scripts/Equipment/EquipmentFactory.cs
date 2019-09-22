using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

static class EquipmentFactory
{
    private static EntityManager eManager { get { return World.Active.EntityManager; } }
    private static EntityArchetype archetype;
    static EquipmentFactory()
    {
        archetype = World.Active.EntityManager.CreateArchetype(new ComponentType[]
        {
            ComponentType.ReadWrite<Translation>(), ComponentType.ReadWrite<Rotation>(),
            ComponentType.ReadWrite<LocalToWorld>()
        });
    }

    public static GameObject CreateEquipment(Vector3 position, EquipmentData data)
    {
        //------------- Inner Mesh ---------------
        GameObject go = new GameObject("Equipment");
        go.transform.position = position;

        go.AddComponent<SphereCollider>();
        go.AddComponent<Rigidbody>();

        // The inner mesh and material
        SetVisuals(go, data.InnerVisuals);

        //------------- Outer Mesh ---------------
        GameObject outerGo = new GameObject("Outers");
        outerGo.transform.position = go.transform.position;
        outerGo.transform.SetParent(go.transform);

        // The outer  mesh and material
        SetVisuals(outerGo, data.OuterVisuals);

        //------------- Entity ---------------
        Entity entity = World.Active.EntityManager.CreateEntity(archetype);

        // Create Entity/GameObject connection
        GameObjectEntity.AddToEntity(eManager, go, entity);
        eManager.AddComponent<CopyTransformFromGameObject>(entity);

        SetComponents(entity, data.components.ToArray(), outerGo);


        // TODO: Redo this by reformatting equipment data
        if (eManager.HasComponent<Equipment>(entity) && eManager.GetComponentData<Equipment>(entity).Type == EquipmentType.Deployable)
        {
            go.AddComponent<RemoveRigidbodyOnGrounded>();
        }

        return go;
    }

    private static void SetVisuals(GameObject go, in VisualComponent visuals)
    {
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = visuals.Material;
        renderer.shadowCastingMode = ShadowCastingMode.Off;

        go.AddComponent<MeshFilter>().sharedMesh = visuals.Mesh;
    }

    private static void SetComponents(Entity entity, IComponentData[] componentsData, GameObject go)
    {
        // set entity components
        foreach (var component in componentsData)
        {
            DynamicConvertAndSetData(entity, component, component.GetType());
        }

        // set gameobject components
        if(eManager.HasComponent<AreaOfEffect>(entity))
        {
            go.transform.localScale = eManager.GetComponentData<AreaOfEffect>(entity).Size;
        }
    }

    private static void DynamicConvertAndSetData(Entity entity, IComponentData data, Type type)
    {
        dynamic convertedData = Convert.ChangeType(data, type);
        eManager.AddComponentData(entity, convertedData);
    }
}
