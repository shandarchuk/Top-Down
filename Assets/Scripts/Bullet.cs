 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float speed; // скорость
    public float lifeTime; // время жизни
    public float distance; // дальность полета
    public int damage; // урон
    public LayerMask whatIsSolid; // слой цель

    public GameObject effect; // префаб эфекта частиц
    [SerializeField] bool enemyBullet;

    private void Start()
    {
        Invoke("DestroyBullet",lifeTime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            // если слой енеми значит враг
            if(hitInfo.collider.CompareTag("Enemy"))
            {      
                // наносим урон врагу
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage); 
            }

            if(hitInfo.collider.CompareTag("Player") && enemyBullet)
            {      
                // наносим урон врагу
                hitInfo.collider.GetComponent<Player>().ChangeHealth(-damage); 
            }
            DestroyBullet();   
            
        } 
        transform.Translate(Vector2.up * speed * Time.deltaTime);    
    }


    void DestroyBullet()
    {
        //спавним эфект частицы
        Instantiate(effect,transform.position,Quaternion.identity);
        // уничтожаем объект
        Destroy(gameObject);
    }

}
