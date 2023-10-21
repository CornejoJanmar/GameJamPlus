using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class plantingMechanics : MonoBehaviour
{
    [SerializeField] private int spawnID = -1;

    [SerializeField] private List<GameObject> plantsPrefab;
    [SerializeField] private List<Image> plantsUI;

    [SerializeField] private Transform spawnRoot;

    [SerializeField] private Tilemap spawnTile;
    private Color originalColor;

    void Update()
    {
        if (canSpawn())
        {
            detectSpawnpoint();
        }
    }

    bool canSpawn()
    {
        if (spawnID == -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void detectSpawnpoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellPos = spawnTile.WorldToCell(mousePos);
            var cellPosCentered = spawnTile.GetCellCenterWorld(cellPos);
            if (spawnTile.GetColliderType(cellPos) == Tile.ColliderType.Sprite)
            {
                spawnPlant(cellPosCentered);
                spawnTile.SetColliderType(cellPos, Tile.ColliderType.None);
            }
        }
    }

    private void spawnPlant(Vector3 position)
    {
        GameObject plant = Instantiate(plantsPrefab[spawnID], spawnRoot);
        plant.transform.position = position;
        deSelectPlant();
    }


    public void selectPlant(int id)
    {
        deSelectPlant();
        spawnID = id;
        // foreach (var p in plantsUI)
        //  {
        //      originalColor = p.color;
        //  }

        // plantsUI[spawnID].color = Color.black;
    }

    public void deSelectPlant()
    {
        spawnID = -1;
        // foreach(var p in plantsUI)
        // {
        //    p.color = originalColor;
        // }
    }

}
