using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    public float offset; // начальное отклонение направления оружия
    public GameObject bullet; // пуля
    public Transform shotPoint; // позиция вылета пули 

    private float timeBetwenShot; // время перезарядки
    public float startTimeBetwenShot; // количество секунду между выстрелами
    
    private Animator cameraAnimator; // аниматор камеры


    void Start()
    {
        cameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,rotZ + offset);

        if(timeBetwenShot <= 0)
        {
            //если нажата клавиша мышки то создаем пулю в позиции вылета
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet,shotPoint.position, transform.rotation);
                // тригер для запуска анимации трясти камеру
                cameraAnimator.SetTrigger("shake");
                timeBetwenShot = startTimeBetwenShot;
            }
        }
        else
        {
            timeBetwenShot -= Time.deltaTime;
        }

    }
}
