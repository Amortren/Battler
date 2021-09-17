using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(100, 100);
    //private Unit[,] grid;
    //private Enemy[,] grid2;
    [SerializeField]
    private Unit unit;
    private Camera mainCamera;
    [SerializeField]
    private Enemy unit2;





    private void Awake()
    {
        //grid = new Unit[GridSize.x, GridSize.y];
        //grid2 = new Enemy[GridSize.x, GridSize.y];
        mainCamera = Camera.main;
        
        
    }

   

    void Update()
    {
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 worldPosition = ray.GetPoint(position);

            int x = Mathf.RoundToInt(worldPosition.x);
            int y = Mathf.RoundToInt(worldPosition.z);

            bool available = true;

            if (x < 0 || x > GridSize.x) available = false;
            if (y < 0 || y > GridSize.y) available = false;


            //if (available && IsPlaceTaken(x, y)) available = false;

            unit.transform.position = new Vector3(x, 0, y);
            unit2.transform.position = new Vector3(x, 0, y);
            


            if (available && Input.GetMouseButtonDown(0))
            {
                PlaceUnit(x, y);
            }
            if (available && Input.GetMouseButtonDown(1))
            {
                PlaceEnemies(x, y);
            }


        }
    }
   
    private void PlaceUnit(int placeX, int placeY)
    {
        Instantiate(unit);

        unit.transform.position = new Vector3(placeX, 0, placeY);

    }
    //private bool IsPlaceTaken(int placeX, int placeY)
    //{
    //    for (int x = 0; x < GridSize.x; x++)
    //    {
    //        for (int y = 0; y < u.y; y++)
    //        {
    //            if (grid[placeX + x, placeY + y] != null) return true;
    //        }
    //    }

    //    return false;
    //}
    private void PlaceEnemies(int placeX, int placeY)
    {
        Instantiate(unit2);

        unit2.transform.position = new Vector3(placeX, 0, placeY);

    }
}
