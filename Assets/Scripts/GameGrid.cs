using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class Tile
{
    public bool Occupied { get; set; }
    public Vector2 Position { set; get; }
    public BaseTower Tower { set; get; }

}


public class GameGrid : MonoBehaviour {



    public static GameGrid Instance { set; get; }
    private const int AMN_TILES_X = 10;
    private const int AMN_TILES_Y = 5;
    private const float TILE_SIZE = 1.0f;
    private const int ENEMY_START_X = 11;

    public Tile[,] Grid { set; get; }
    public GameObject[] TowerPrefab;
    public GameObject[] EnemyPrefab;
    public List<BaseEnemy> activeEnemies = new List<BaseEnemy>(); 
    private bool isDeletingTower;
    private bool isSelectingTower; 
    private int selectedTowerIndex;
    private void Start()
    {
        Instance = this;
        Grid = new Tile[AMN_TILES_X, AMN_TILES_Y];

        for (int i= 0;i < AMN_TILES_X;i++)
        {
            for(int j=0;j<AMN_TILES_Y;j++)
            {
                Grid[i, j] = new Tile() { Occupied = false, Position = new Vector2(i, j), Tower = null };
            }
        }
    }


    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            spawnEnemy(0, 2);
        }


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            int x = 0;
            int y = 0;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 30.0f, LayerMask.GetMask("GameGrid")))
            {

                x = (int)hit.point.x;
                y = (int)hit.point.z;

                if (isDeletingTower)
                {
                    Tile t = SelectGridTile(x, y);
                    if (t.Occupied)
                    {
                        // delete that tower
                        GameManager.Instance.Elixir += t.Tower.elixirOnDelete;
                        GameManager.Instance.UpdateElixirText();

                        Destroy(t.Tower.gameObject);
                        t.Occupied = false;
                    }
                    isDeletingTower = false;
                }


                if (isSelectingTower) 
                {
                    Tile t = SelectGridTile(x, y);
                    if (!t.Occupied)
                    {

                        GameObject Go = Instantiate(TowerPrefab[selectedTowerIndex]) as GameObject;
                        Go.transform.position = (Vector3.right * x) + (Vector3.forward * y) + (0.5f * Vector3.right) + (0.5f * Vector3.forward);

                        t.Occupied = true;
                        t.Tower = Go.GetComponent<BaseTower>();

                        // deduct elixir
                        int cost = int.Parse(GameManager.Instance.PokemonContainer.transform.GetChild(selectedTowerIndex).GetComponentInChildren<Text>().text);
                        SelectTower(selectedTowerIndex);
                        GameManager.Instance.Elixir -= cost;
                        GameManager.Instance.UpdateElixirText();
                        isSelectingTower = false;
                        selectedTowerIndex = -1;
                    }
                    else
                    {
                        isSelectingTower = false;
                        selectedTowerIndex = -1;
                    }
                }

            }
        }

        // draw debugs
        
        for (int i = 0; i < AMN_TILES_X+1; i++)
        {
            Vector3 startPos = Vector3.right * i;
            //Debug.DrawLine(startPos,startPos + Vector3.forward * AMN_TILES_Y);
        }
        for (int j=0;j<AMN_TILES_Y+1;j++)
        {
            Vector3 startPos = Vector3.forward * j;
            //Debug.DrawLine(startPos, startPos + Vector3.right * AMN_TILES_X);
        }

    }

    public Tile SelectGridTile(int x,int y)
    {
        return Grid[x, y];
    }

    public void SelectTower(int index)
    {
        int cost = int.Parse(GameManager.Instance.PokemonContainer.transform.GetChild(index).GetComponentInChildren<Text>().text);
        if (cost <= GameManager.Instance.Elixir)
        {
            // do we have enough elixir
            isSelectingTower = true;
            selectedTowerIndex = index;
        }
        else
        {
            Debug.Log("Insuffiecient Elixir");
        }
    }


    public void SelectDelete()
    {
        isDeletingTower = true;
    }

    public void spawnEnemy(int prefabIndex, int lane)
    {
        GameObject go = Instantiate(EnemyPrefab[prefabIndex]) as GameObject;
        go.transform.position = new Vector3(ENEMY_START_X, 0.5f, 1 * lane + 0.5f);

        BaseEnemy e = go.GetComponent<BaseEnemy>();
        e.Position = new Vector2(ENEMY_START_X, lane);
        activeEnemies.Add(e);
         
    }

    public void deleteEnemy(BaseEnemy e)
    {
        activeEnemies.Remove(e);
        Destroy(e.gameObject);

        // Create Particle Effect and Sound Effect
    }

}
