# Terrain Stitching Feature

This document describes the optional terrain stitching feature for HybridTerrainEntity to prevent gaps between adjacent terrain tiles.

## Overview

The terrain stitching feature automatically matches the edge heights of adjacent HybridTerrainEntity instances to create seamless terrain transitions. This eliminates visible gaps that can occur when placing multiple terrain entities next to each other.

## Features

- **Optional Configuration**: Stitching can be enabled or disabled per terrain entity
- **Automatic Edge Detection**: Automatically detects adjacent terrain entities
- **Edge Matching**: Averages height values along shared edges
- **All Directions**: Supports stitching on all four edges (left, right, front, back)
- **Non-Destructive**: Original height data is preserved when stitching is disabled

## Usage

### Basic Usage

```csharp
// Create terrain with stitching enabled
HybridTerrainEntity terrain = HybridTerrainEntity.Create(
    length: 10f, 
    width: 10f, 
    height: 5f,
    heights: heightArray,
    layers: layerArray,
    layerMasks: layerMaskDict,
    id: Guid.NewGuid(),
    enableStitching: true  // Enable stitching
);

// Apply stitching with adjacent terrains
terrain.StitchWithAdjacentTerrains();
```

### Runtime Configuration

```csharp
// Check if stitching is enabled
bool isStitchingEnabled = terrain.GetStitching();

// Enable or disable stitching
terrain.SetStitching(true);

// Find adjacent terrains
List<HybridTerrainEntity> adjacentTerrains = terrain.FindAdjacentTerrains();

// Apply stitching manually
terrain.StitchWithAdjacentTerrains();
```

### Example Scenario

```csharp
// Create a 2x2 grid of terrains with stitching
for (int x = 0; x < 2; x++)
{
    for (int z = 0; z < 2; z++)
    {
        HybridTerrainEntity terrain = HybridTerrainEntity.Create(
            10f, 10f, 5f, heights, layers, layerMasks, 
            Guid.NewGuid(), true);
        
        terrain.SetPosition(new Vector3(x * 10f, 0, z * 10f), false);
    }
}

// Apply stitching to all terrains
HybridTerrainEntity[] allTerrains = FindObjectsOfType<HybridTerrainEntity>();
foreach (var terrain in allTerrains)
{
    terrain.StitchWithAdjacentTerrains();
}
```

## API Reference

### Properties and Methods

- **`GetStitching()`**: Returns whether stitching is enabled
- **`SetStitching(bool enabled)`**: Enable or disable stitching
- **`FindAdjacentTerrains(float maxDistance = 0.1f)`**: Find nearby terrain entities
- **`StitchWithAdjacentTerrains()`**: Apply stitching with all adjacent terrains

### Create Method Overloads

```csharp
// Create without stitching (backward compatible)
HybridTerrainEntity.Create(length, width, height, heights, layers, layerMasks, id)

// Create with stitching option
HybridTerrainEntity.Create(length, width, height, heights, layers, layerMasks, id, enableStitching)
```

## How It Works

1. **Detection**: The system searches for HybridTerrainEntity instances within a specified distance
2. **Positioning**: Determines relative positions to identify which edges should be stitched
3. **Edge Matching**: Averages height values along shared edges between terrains
4. **Application**: Updates the terrain's height data and applies changes to Unity's TerrainData

## Best Practices

1. **Position First**: Set terrain positions before applying stitching
2. **Batch Operations**: Apply stitching to all terrains after creating a grid
3. **Consistent Heights**: Ensure adjacent terrains have compatible height ranges
4. **Performance**: Use stitching sparingly for large numbers of terrains

## Limitations

- Stitching works best with terrains of similar sizes
- Height data resolution should be compatible between adjacent terrains
- Stitching is applied to base heights only, not to digger/voxel modifications

## Example Component

See `TerrainStitchingExample.cs` for a complete example of creating a grid of stitched terrains.

## Testing

The feature includes comprehensive tests in `EntityTests.cs` under the `EntityTests_HybridTerrainEntity_Stitching` test method.