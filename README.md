# SoftEngine

A simple 3D software engine written in C# from scratch that renders to the console.  

"Software" means that the engine fully relies on the CPU, bypassing GPU hardware acceleration, similar to a "retro" rendering style. 

![](https://github.com/8fclss/SoftEngine/blob/main/cube.gif)

## Current State
This project is currently about:
- **3D Vertex Transformations**: Custom 4x4 matrix implementation for rotation, translation, and world-space mapping.
- **Triangle Rasterization**: A barycentric-based system for filling 3D faces.
- **Back-face Culling**: Optimization using Dot and Cross products to avoid rendering faces that are pointing away from the camera.
- **Z-Buffering**: Depth management to ensure correct occlusion when rendering overlapping triangles.
- **Gouraud Shading**: Per-vertex normals are accumulated from adjacent faces and normalized. Lighting intensity is calculated per-vertex and interpolated smoothly across each triangle using barycentric coordinates.
- **Buffer Management**: Character buffer for console output.

## How it Works
The engine follows a standard 3D rendering pipeline, adapted for ASCII-based rendering:

1. **Vertex Transformation**: Each vertex starts in its own local coordinate system. We use 4x4 matrices to rotate and translate these vertices into a single "world space."
2. **Back-face Culling**: Before drawing, the engine calculates the surface normal of each triangle face. If the face is looking away from the camera (determined by the dot product of the normal and the view direction) it is discarded. This prevents seeing through the models and reduces processing.
3. **Perspective Projection**: To create the illusion of depth, the X and Y coordinates are divided by their distance from the camera (the Z value). This makes objects further away look smaller on the screen. (bigger Z => smaller 1/Z and vice versa)
4. **Triangle Rasterization**: Since the console is a grid of characters, we use **barycentric coordinates** to determine if a "pixel" is inside a triangle and interpolate depth and lighting.
5. **Z-Buffering and Shading**: For every pixel, the engine compares its depth (Z) against a depth buffer to ensure front-most surfaces are rendered. Per-vertex normals are computed by accumulating and normalizing adjacent face normals. Lighting intensity is calculated per-vertex via dot product with a static light source, then smoothly interpolated across each triangle using barycentric coordinates (Gouraud shading), and mapped to ASCII characters: ` .:-=+*#%@`.
6. **Buffer Management**: To avoid flickering, all characters are written into a single character buffer first. Once the frame is complete, the entire buffer is sent to the console at once.

## Project Structure
```text
SoftEngine/
├── Core/
│   └── Engine.cs       # Main update and render loop
├── Display/
│   └── Screen.cs       # Console buffer, Z-buffer and triangle drawing
├── Mathematics/
│   ├── Matrix4.cs      # Custom 4x4 matrix implementation
│   └── Vector3.cs      # Custom 3D vector implementation
└── Models/
    ├── Mesh.cs         # Base mesh class (Vertices, Edges, Faces)
    └── Cube.cs         # Cube primitive definition
```

## Run
Requirements: .NET SDK

```bash
dotnet run --project SoftEngine/SoftEngine.csproj
```
