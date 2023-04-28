using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public ControlType controlType; // тип управления

    public enum ControlType{PC, Android} // варианты управления
    public Animator animator; // компонент аниматор
    public Joystick joystick; // компонент джойстик
    public float speed; // скорость
    private Rigidbody2D rigidbody2d;
    private Vector2 moveInput; // направление движения
    private Vector2 moveVelocity; // итоговоая скорость игрока

    private bool facingRight = true; // направление лица

    public int health; // здоровье
    

    void Start()
    {

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(controlType == ControlType.PC)
        {
            // считываем направление движения с клавиатуры
            moveInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")); 
        }
        else if(controlType == ControlType.Android)
        {
            // считываем направление движения с джойстика
            moveInput = new Vector2(joystick.Horizontal,joystick.Vertical); 
        }
        // побежал
        moveVelocity = moveInput.normalized * speed; 

        // установка параметра в аниматор 
        if(moveInput.x == 0)
        {
            animator.SetBool("isRunning",false); 
        }
        else
        {
            animator.SetBool("isRunning",true);
        } 

        if(!facingRight && moveInput.x > 0)
        {
            Flip();
        } 
        else if(facingRight && moveInput.x < 0)
        {
            Flip();
        }
    }

    void FixedUpdate() 
    {
        // двигаем нашего игрока
        rigidbody2d.MovePosition(rigidbody2d.position + moveVelocity * Time.fixedDeltaTime);     
    }

    //разворот лица
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
    
    public void ChangeHealth(int healthValue)
    {
        health -= healthValue;
     }



     
}
