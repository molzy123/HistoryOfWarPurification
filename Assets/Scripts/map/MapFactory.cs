using Factory;
using UnityEngine;

namespace DefaultNamespace.map
{
    public class MapFactory : ResourcesFactory
    {
        public GridCell createGridCell()
        {
            GameObject floor = loadGameObject("Map/Floor");
            if (floor == null)
            {
                return null;
            }
            return floor.GetComponent<GridCell>();
        }
        
        public GridCell[,] createGridCells(int width, int height, GameObject parent)
        {
            GridCell[,] gridCells = new GridCell[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GridCell gridCell = createGridCell();
                    if (gridCell != null)
                    {
                        gridCell.transform.parent = parent.transform;
                        gridCell.setPosition(i, j);
                        gridCells[i, j] = gridCell;
                    }
                }
            }
            return gridCells;
        }
    }
}