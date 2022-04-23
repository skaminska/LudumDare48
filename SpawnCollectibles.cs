using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SpawnCollectibles : MonoBehaviour
{
    public List<GameObject> collectibles = new List<GameObject>();
    public Tilemap tilemap;
    public List<Vector3> tileWorldLocations;
    void Start()
    {
        tileWorldLocations = new List<Vector3>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            if (tilemap.HasTile(localPlace))
            {
                tileWorldLocations.Add(place);
            }
        }

        for(int i = 0; i < 120; i++)
        {
            int xPos;
            int yPos;
            do
            {
                xPos = Random.Range(-11, 125);
                yPos = Random.Range(-5, -170);
            }
            while (tileWorldLocations.Contains(new Vector3(xPos, yPos, 0)));
            
            Instantiate(pickToSpawn(yPos), transform.position = new Vector3(xPos, yPos, 0), Quaternion.identity);
        }
    }

    GameObject pickToSpawn(int yPos)
    {
        int yPosMin = -170;
        int tmp = Random.Range(0, 100);
        if(yPos > (yPosMin / 3))
        {
            if (tmp < 60)
                return collectibles[0];
            else
                return collectibles[1];
        }
        if(yPos > (yPosMin / 1.5))
        {
            if (tmp < 50)
                return collectibles[0];
            else if (tmp < 90)
                return collectibles[1];
            else
                return collectibles[2];
        }
        else
        {
            if (tmp < 30)
                return collectibles[0];
            else if (tmp < 70)
                return collectibles[1];
            else
                return collectibles[2];
        }
    }
}
