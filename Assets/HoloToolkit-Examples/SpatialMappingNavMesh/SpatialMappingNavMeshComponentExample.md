# SpatialMappingNavMeshComponent Example
This example shows how to use the built-in Unity NavMeshComponents for agent navigation of spatial maps. It shows an example of a mapped play space which allows agent navigation around it (marked by yellow cylinders). 

[NavMesh Hololens In-Editor Example](https://youtu.be/VQVxRAq7U2k)
[NavMesh On-Hololens Example](https://youtu.be/d8A6UAviLyQ)
#### NavMeshSourceTag:
Use this component for performing navigation on objects that are not part of the Spatial Mapping mesh.

#### NavMeshComponents: 

Here we use four components for the navigation system:
* __NavMeshSurface__ – for building and enabling a NavMesh surface for one agent type.
* __NavMeshModifier__ – affects the NavMesh generation of NavMesh area types, based on the transform hierarchy.
* __NavMeshModifierVolume__ – affects the NavMesh generation of NavMesh area types, based on volume.
* __NavMeshLink__ – connects same or different NavMesh surfaces for one agent type.

These components comprise the high level controls for building and using NavMeshes at runtime as well as edit time.

Detailed information can be found in the [Documentation](Documentation) section or in the [NavMesh building components](https://docs.unity3d.com/Manual/NavMesh-BuildingComponents.html) section of the Unity Manual.


#### Design Considerations

1. If you want spatial mapping to work wherever the user travels, attach the components to the camera and the specified bounds will move with the user. The NavMeshComponents will continue to expand the navigational mesh as the user moves.