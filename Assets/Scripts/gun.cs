using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    public GunType gunType;
    public float offset; // начальное отклонение направления оружия
    public GameObject bullet; // пуля
    public Joystick joystick; 
    public Transform shotPoint; // позиция вылета пули 

    private float timeBetwenShot; // время перезарядки
    private float rotationZ;
    private Vector3 difference;
    public float startTimeBetwenShot; // количество секунду между выстрелами
    
    private Animator cameraAnimator; // аниматор камеры

    private Player player; //

    public enum GunType{Default, Enemy}

    void Start()
    {
        cameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.controlType == Player.ControlType.PC && gunType == GunType.Default)
            joystick.gameObject.SetActive(false);
    }

    void Update()
    {   
        if (gunType == GunType.Default)
        {    
            if (player.controlType == Player.ControlType.PC)
            {
                difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            }

            else if (player.controlType == Player.ControlType.Android)
            {

                rotationZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            }

        }

        else if (gunType == GunType.Enemy)
        {
            difference = player.transform.position - transform.position;
            rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;    
        }


        transform.rotation = Quaternion.Euler(0f,0f,rotationZ + offset);

        if(timeBetwenShot <= 0)
        {
            //если нажата клавиша мышки то создаем пулю в позиции вылета
            if (Input.GetMouseButton(0) && player.controlType == Player.ControlType.PC || gunType == GunType.Enemy)
            {
                Shoot();    
            }
            else if (player.controlType == Player.ControlType.Android)
            {
                if (joystick.Horizontal !=0 || joystick.Vertical != 0)
                    Shoot();    
            }
        }
        else
        {
            timeBetwenShot -= Time.deltaTime;
        }

    }

    public void Shoot()
    {
        Instantiate(bullet,shotPoint.position, shotPoint.rotation);
        // тригер для запуска анимации трясти камеру
        cameraAnimator.SetTrigger("shake");
        timeBetwenShot = startTimeBetwenShot;
    }
}
