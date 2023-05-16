using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject key;


    [HideInInspector] public List<GameObject> rooms;

    private void Start() 
    {
        StartCoroutine(RandomSpawner());   
    }

    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(5f);

        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
        int Rand = Random.Range(0, rooms.Count - 2);

        Instantiate(key,rooms[Rand].transform.position, Quaternion.identity);

        //lastRoom.door.SetActive(true);
        //lastRoom.DestroyWalls();
    } 
}
