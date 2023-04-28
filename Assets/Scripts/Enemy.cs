using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int healt; // здоровье
    public float speed; // скорость
    private Animator animator; 

    private float timeBetwenAtack; // время перезарядки
    public float startTimeBetwenAtack; // количество секунду между ударами

    private Player player; // игрок

    public int damage; //урон

    public GameObject effect; // эффект частиц при смерти

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    public void TakeDamage(int damage)
    {

        if(healt <= 0 )
        {
            Destroy(gameObject);
            Instantiate(effect,transform.position,Quaternion.identity);
            
        }
        healt -= damage;
    }

    void Update()
    {
        animator.SetBool("isRunning", true);  

        // разворачиваем врага в сторону игрока
        if(player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);   
        }

        // двигаем врага в направлении игроку 
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            if(timeBetwenAtack <= 0 )
            {
                animator.SetTrigger("atack");
            }
            else
            {
                timeBetwenAtack -= Time.deltaTime;   
            }
        }    
    }

    // к функции обращаемся из анимации атаки...
    public void OnEnemyAtack()
    {
        //спавним эфект частицы
        Instantiate(effect,player.transform.position,Quaternion.identity);
        player.ChangeHealth(-damage);
        
        timeBetwenAtack = startTimeBetwenAtack;    
    }




}
