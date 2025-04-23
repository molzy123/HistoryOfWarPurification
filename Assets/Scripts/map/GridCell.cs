using UnityEngine;

namespace DefaultNamespace.map
{
    public class GridCell: MonoBehaviour
    {

        public void setPosition(int x, int z)
        {
            transform.position = new Vector3(x,0,z);
        }
        
        
    }
}