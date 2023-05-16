using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction; 
    public enum Direction
    {
    Top,
    Bottom,
    Left,
    Right,
    None    
    }

    private RoomVariants variants;
    private int rand;
    private bool spawned;
    private float waitTime = 3f;

    private void Start() 
    {
        // определяем варианты по тегу
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); 
        // уничтожаем точку 
        Destroy(gameObject, waitTime);
        // спавним комнату с задержкой
        Invoke("Spawn", 0.2f);

    }

    public void Spawn()
    {
        if(!spawned)
        {
            if(direction == Direction.Top)
            {
                // получаем рандомную комнату из массива комнат top
                rand = Random.Range(0,variants.topRooms.Length);
                // создаем комнату в точке спавна
                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);    
            }
            else if(direction == Direction.Bottom)
            {
                // получаем рандомную комнату из массива комнат bottom
                rand = Random.Range(0,variants.bottomRooms.Length);
                // создаем комнату в точке спавна
                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);    
            }

            else if(direction == Direction.Left)
            {
                // получаем рандомную комнату из массива комнат left
                rand = Random.Range(0,variants.leftRooms.Length);
                // создаем комнату в точке спавна
                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);    
            }

            else if(direction == Direction.Right)
            {
                // получаем рандомную комнату из массива комнат right
                rand = Random.Range(0,variants.rightRooms.Length);
                // создаем комнату в точке спавна
                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);    
            }

            spawned = true;
        }    
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        // уничтожаем объект если уже есть точка спавна
        if(other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }        
    }

}
