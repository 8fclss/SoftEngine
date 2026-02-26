# SoftEngine

A simple 3D software engine written in C# from scratch that renders to the console.  

"Software" means that the engine fully relies on CPU, bypassing GPU hardware acceleration similar to "retro" rendering style. 

## Current State
This project is currently about:
- 3D vertex transformations using a custom 4x4 Matrix implementation.
- Wireframe rendering using Bresenham's line algorithm.
- A basic Mesh system (currently demonstrating a rotating Cube).

## How it Works
The engine follows a standard 3D rendering pipeline, adapted for an ASCII based rendering:

1. **Vertex Transformation**: Each vertex starts in its own local coordinate system. We use 4x4 matrices instead of standard 3x3 rotation matrix because it allows us to rotate and translate these vertices into a single "world space". However the full potential of 4x4 matrix is still not being used as we handle perspective projection manually.
2. **Perspective Projection**: To create the illusion of depth, the X and Y coordinates are divided by their distance from the camera (the Z value). This makes objects further away look smaller on the screen. (bigger Z => smaller 1/Z and vice versa)
3. **Rasterization**: Console is just a grid of characters, therefore to make the engine smart and help him decide which pixels to fill with characters, we use Bresenham's line algorithm.
4. **Buffer Management**: To avoid flickering, all characters are written into a single array (the buffer) first, and then the entire buffer is sent to the console at once, instead of "printing" each character one by one as in the earliest versions of the project.

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
    ├── Mesh.cs         # Base mesh class
    └── Cube.cs         # Cube primitive definition
```

## Run
Requirements: .NET SDK

```bash
dotnet run --project SoftEngine/SoftEngine.csproj
```
