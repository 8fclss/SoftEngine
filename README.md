# SoftEngine

A simple 3D software engine written in C# from scratch that renders to the console.  

"Software" means that the engine fully relies on the CPU, bypassing GPU hardware acceleration, similar to a "retro" rendering style. 

## Current State
This project is currently about:
- **3D Vertex Transformations**: Custom 4x4 matrix implementation for rotation, translation, and world-space mapping.
- **Face-Based Rendering**: Moving beyond simple edges to a triangle-based system.
- **Back-face Culling**: Optimization using Dot and Cross products to avoid rendering faces that are pointing away from the camera.
- **Buffer Management**: Double-buffered console output.

## How it Works
The engine follows a standard 3D rendering pipeline, adapted for ASCII-based rendering:

1. **Vertex Transformation**: Each vertex starts in its own local coordinate system. We use 4x4 matrices to rotate and translate these vertices into a single "world space."
2. **Back-face Culling**: Before drawing, the engine calculates the surface normal of each triangle face. If the face is looking away from the camera (determined by the dot product of the normal and the view direction) it is discarded. This prevents seeing through the models and reduces processing.
3. **Perspective Projection**: To create the illusion of depth, the X and Y coordinates are divided by their distance from the camera (the Z value). This makes objects further away look smaller on the screen. (bigger Z => smaller 1/Z and vice versa)
4. **Rasterization**: Since the console is a grid of characters, we use **Bresenham's line algorithm** to decide which "pixels" to fill with characters to represent the edges of the visible triangles.
5. **Buffer Management**: To avoid flickering, all characters are written into a single character buffer first. Once the frame is complete, the entire buffer is sent to the console at once.

## Project Structure
```text
SoftEngine/
├── Core/
│   └── Engine.cs       # Main update and render loop
├── Display/
│   └── Screen.cs       # Console buffer and line drawing
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
