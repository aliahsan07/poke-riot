using UnityEngine;
using System.Collections;

public class BaseTower : MonoBehaviour {

    public int elixirOnDelete;
    public int hitpoint = 3;

    public void TakeDamage(int amount)
    {
        hitpoint -= amount;
        print(hitpoint);
        if (hitpoint < 0)
        {
            print("GONNA BE DELETED");
            Tile t =GameGrid.Instance.SelectGridTile((int)(transform.position.x),(int)(transform.position.z));
            Destroy(t.Tower.gameObject);
            t.Occupied = false;
        }
    }
}
