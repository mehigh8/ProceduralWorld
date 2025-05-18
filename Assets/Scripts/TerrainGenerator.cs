using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Terrain")]
    public Terrain terrain;
    [Tooltip("width x depth x height")]
    public Vector3Int size;
    public float scale;
    public int octaves;
    public float amplitude;
    public float frequency;
    public float lacunarity;
    public float persistence;
    [Header("Plants")]
    public GameObject plantPrefab;
    public int plantCount;

    void Start()
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        SpawnPlants(plantCount);
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = size.x + 1;
        terrainData.size = size;
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[size.x, size.z];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.z; y++)
            {
                heights[x, y] = CalculateFractalNoise(x, y);
            }
        }

        return heights;
    }

    private float CalculateFractalNoise(int x, int y)
    {
        float noiseHeight = 0f;
        float amplitude = this.amplitude;
        float frequency = this.frequency;

        for (int i = 0; i < octaves; i++)
        {
            float xCoord = (float)x / size.x * scale * frequency;
            float yCoord = (float)y / size.z * scale * frequency;

            float perlinValue = Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1;
            noiseHeight += perlinValue * amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;
        }

        return (noiseHeight + 1) / 2; // Normalize to 0 - 1
    }

    private void SpawnPlants(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 startRayPoint = new Vector3(Random.Range(1f, size.x - 1), size.y * 2, Random.Range(1f, size.z - 1));
            if (Physics.Raycast(startRayPoint, Vector3.down, out RaycastHit hit, float.MaxValue))
                Instantiate(plantPrefab, hit.point, Quaternion.identity, transform);
        }
    }
}
