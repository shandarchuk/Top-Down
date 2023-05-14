using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 playerChangePosition; // изменение позиции игрока
    public Vector3 cameraChangePosition; // изменение позиции камеры 
     Camera cam; // камера
    
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            print("Player change position " + playerChangePosition);
            print("Camera change position " + cameraChangePosition);

            print("Player pos before " + other.transform.position);
            print("Cam pos before " + cam.transform.position);    
            other.transform.position += playerChangePosition;
            cam.transform.position += cameraChangePosition;

            print("Player pos after " + other.transform.position);
            print("Cam pos after " + cam.transform.position); 
        }    
    }
   
}
