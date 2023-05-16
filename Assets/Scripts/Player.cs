using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Controls")]
    public ControlType controlType; // тип управления
    public Joystick joystick; // компонент джойстик
    public float speed; // скорость
    public enum ControlType{PC, Android} // варианты управления

    [Header("Health")]
    public int health; // здоровье
    public Text healthDisplay; // текст здоровья

    [Header("Shield")]
    public GameObject shield; // щит
    public Shield shieldTimer; // таймер щита

    [Header("Key")]
    public GameObject keyIcon; // иконка ключа
    public GameObject wallEffect; // эффект стены


    private Animator animator; // компонент аниматор
    private Rigidbody2D rigidbody2d;
    private Vector2 moveInput; // направление движения
    private Vector2 moveVelocity; // итоговоая скорость игрока
    private bool facingRight = true; // направление лица
    private bool keyButtonPushed; // признак нажатия на иконку ключа

    //сохранение загрузка
    private DataManager dataManager;
    private float saveInterval = 6f; // Интервал сохранения в секундах
    private float timer = 0f;

    private DataStorage dataStorage;
    //сохранение загрузка


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ChangeHealth(0);

        //сохранение загрузка
        dataManager = FindObjectOfType<DataManager>();
        LoadGame();
        //сохранение загрузка
    }

    private void LoadGame()
    {
        // Получение ссылки на компонент dataStorage
        DataStorage dataStorage = DataStorage.Instance;

        health = dataStorage.health;
        transform.position = dataStorage.position;

        Camera cam = Camera.main.GetComponent<Camera>();
        cam.transform.position += transform.position;
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


        //сохранение
        {
        // Увеличение таймера
        timer += Time.deltaTime;

        // Если прошло достаточно времени для сохранения
        if (timer >= saveInterval)
        {
            // Вызов метода сохранения данных игрока
            dataManager.SaveData(health, transform.position);

            // Сброс таймера
            timer = 0f;
        }
        //сохранение
    }

    }

    //разворот
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
    
    public void ChangeHealth(int healthValue)
    {
        if(!shield.activeInHierarchy || shield.activeInHierarchy && healthValue > 0)
        {
            health += healthValue;
            healthDisplay.text = "HP: " + health;
        }
     }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Potion"))
        {
            ChangeHealth(5);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Shield"))
        {
            if(!shield.activeInHierarchy)
            {
                shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isCoolDown = true;       
            }
            else
            {
                shieldTimer.ResetTimer();
            }
            Destroy(other.gameObject);
        }  
        else if(other.CompareTag("Key"))
        {
            keyIcon.SetActive(true);
            Destroy(other.gameObject);
        }  
    }

    public void onKeyButtonDown()
    {
        keyButtonPushed = !keyButtonPushed;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
    if (other.CompareTag("Door") && keyButtonPushed && keyIcon.activeInHierarchy)
    {
        //Instantiate(wallEffect, other.transform.position, Quaternion.identity);
        keyIcon.SetActive(false);
        other.gameObject.SetActive(false);
        keyButtonPushed = false;
    }    
    }

     
}
