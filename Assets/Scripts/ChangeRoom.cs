using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 playerChangePosition; // изменение позиции игрока
    public Vector3 cameraChangePosition; // изменение позиции камеры 
    public Camera cam; // камера
    
    void Start()
    {
        //cam = Camera.main.GetComponent<Camera>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            print("Before " + cam.transform.position);
            other.transform.position += playerChangePosition;
            cam.transform.position += cameraChangePosition;
            print("Before " + cam.transform.position);
        }    
    }
   
}
