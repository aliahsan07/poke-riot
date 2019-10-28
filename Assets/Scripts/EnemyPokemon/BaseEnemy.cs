using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class BaseEnemy : MonoBehaviour {

    public Vector2 Position { set; get; }
    public float attackCooldown = 1.5f;
    public int damage = 1;
    private float lastAttack;

    private Vector2 desiredPosition;
    private bool isBlocked;
    

    private void Update()
    {
        if (!isBlocked)
        {
            transform.position += -Vector3.right * Time.deltaTime;
            if ((Position.x - transform.position.x) > 0.5f)
            {
                UpdateGridPos();
            }
        }

        else
        {
            // attacking logic
            //Debug.Log((int)desiredPosition.x - 1);
            Attack();
            isBlocked = GameGrid.Instance.Grid[(int)desiredPosition.x, (int)desiredPosition.y].Occupied;
        }
        
    }

    private void UpdateGridPos()
    {
        Vector2 desiredPosition = new Vector2((int)transform.position.x,(int)transform.position.z);

        if (desiredPosition.x < 1)
        {
            //GameGrid.Instance
            SceneManager.LoadScene(3);
            print("The enemy Pokemon have raided your home! You lose");
            GameManager.Instance.removeLife();
            
            Destroy(gameObject);
            return;
        }
        //Debug.Log(desiredPosition);
        if (GameGrid.Instance.Grid[(int)desiredPosition.x-1,(int)desiredPosition.y].Occupied)
        {
            
            isBlocked = true;
            lastAttack = Time.time;
        }
        else
        {
            Position = desiredPosition;
        }
    }

    private void Attack()
    {
        if (Time.time -lastAttack > attackCooldown)
        {
            
            for (int i=0;i<555;i++)
                print("gonna attack");
            GameGrid.Instance.SelectGridTile((int)desiredPosition.x-1, (int)desiredPosition.y).Tower.TakeDamage(damage);
            lastAttack = Time.time;
        }
    }

}
