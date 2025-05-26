using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungeonGenerator : MonoBehaviour
{
    [Header("Config")]
    public int minRoomWidth;
    public int minRoomHeight;
    public int dungeonWidth;
    public int dungeonHeight;
    public int offset;
    [Header("Painter")]
    public GameObject floorTile;
    public GameObject wallTile;
    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject trapPrefab;
    public GameObject chestPrefab;
    public GameObject enemyPrefab;
    public GameObject portalPrefab;

    private List<BoundsInt> roomsList;

    void Awake()
    {
        roomsList = BinarySpacePartitioning.BSP(new BoundsInt(new Vector3Int(-dungeonWidth / 2, -dungeonHeight / 2, 0), new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomsList);

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        PaintDungeon(floor);

        // spawn player
        Instantiate(playerPrefab, roomsList[0].center, Quaternion.identity);

        for (int i = 1; i < roomsList.Count - 1; i++)
        {
            int choice = Random.Range(0, 4);
            switch (choice)
            {
                case 0:
                    GameObject trap = Instantiate(trapPrefab, roomsList[i].center, Quaternion.identity, transform);
                    break;
                case 1:
                    GameObject gold = Instantiate(chestPrefab, roomsList[i].center, Quaternion.identity, transform);
                    gold.GetComponent<EntityInformation>().LoadGold(Random.Range(100, 500));
                    break;
                case 2:
                    GameObject item = Instantiate(chestPrefab, roomsList[i].center, Quaternion.identity, transform);
                    item.GetComponent<EntityInformation>().LoadItem(NPCItemGenerator.GetInstance().GenerateItem());
                    break;
                case 3:
                    GameObject enemy = Instantiate(enemyPrefab, roomsList[i].center, Quaternion.identity, transform);
                    enemy.GetComponent<EntityInformation>().LoadEnemy(NPCItemGenerator.GetInstance().GenerateNPC());
                    break;
            }
        }

        Instantiate(portalPrefab, roomsList[roomsList.Count - 1].center, Quaternion.identity, transform);
    }

    private void PaintDungeon(HashSet<Vector2Int> floor)
    {
        foreach (Vector2Int tile in floor)
        {
            Instantiate(floorTile, new Vector3(tile.x, tile.y, 0), Quaternion.identity, transform);
            if (!floor.Contains(tile + new Vector2Int(-1, 0)))
                Instantiate(wallTile, new Vector3(tile.x - 1, tile.y, 0), Quaternion.identity, transform);
            if (!floor.Contains(tile + new Vector2Int(1, 0)))
                Instantiate(wallTile, new Vector3(tile.x + 1, tile.y, 0), Quaternion.identity, transform);
            if (!floor.Contains(tile + new Vector2Int(0, -1)))
                Instantiate(wallTile, new Vector3(tile.x, tile.y - 1, 0), Quaternion.identity, transform);
            if (!floor.Contains(tile + new Vector2Int(0, 1)))
                Instantiate(wallTile, new Vector3(tile.x, tile.y + 1, 0), Quaternion.identity, transform);
        }
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
