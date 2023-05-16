using UnityEngine;
using System.IO;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class Data
{
    public int health;
    public Vector3 position;
    public Vector3 camPosition; 
    public List<RoomData> roomsData;
}

[System.Serializable]
public class RoomData
{
    public string name;
    public Vector3 position;
}

public class DataManager : MonoBehaviour
{
    public Data data;

    // Метод сохранения данных игрока
    public void SaveData(int currentLives, Vector3 currentPosition)
    {   
        Camera cam = Camera.main.GetComponent<Camera>();
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Room");
        
        // Создание нового объекта Data
        data = new Data();
        // Присвоение переданных значений
        data.health = currentLives;
        data.position = currentPosition;
        data.camPosition = cam.transform.position;
        data.roomsData = new List<RoomData>();
        foreach(GameObject obj in objectsWithTag)
        {   
            RoomData roomData = new RoomData();
            roomData.name = obj.name.Replace("(Clone)","");
            roomData.position = obj.transform.position;            
            data.roomsData.Add(roomData);        
        }

        // Преобразование объекта Data в JSON-строку
        string json = JsonUtility.ToJson(data);

        // Получение пути к файлу сохранения
        string savePath = GetSavePath();

        // Запись JSON-строки в файл
        File.WriteAllText(savePath, json);

        Debug.Log("Данные игрока сохранены. " + savePath);

    }

    // Метод загрузки данных игрока
    public void LoadData()
    {
        // Получение пути к файлу сохранения
        string savePath = GetSavePath();

        if (File.Exists(savePath))
        {
            // Чтение JSON-строки из файла
            string json = File.ReadAllText(savePath);

            // Преобразование JSON-строки в объект Data
            data = JsonUtility.FromJson<Data>(json);
            Debug.Log("Данные игрока загружены.");
            Camera cam = Camera.main.GetComponent<Camera>();
            cam.transform.position = data.camPosition;
                        
            foreach(RoomData roomData in data.roomsData)
            {
                string name = roomData.name;  
                Vector3 position = roomData.position; 
                
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Rooms/"+name+".prefab");
                print(prefab.name);
                
                Instantiate(prefab, position, prefab.transform.rotation);      
            }
        }
        else
        {
            Debug.Log("Файл сохранения не найден. Созданы новые данные игрока.");

            // Если файл сохранения не найден, создаем новый объект Data со значениями по умолчанию
            data = new Data();
            data.health = 3; // Значение по умолчанию для количества жизней
            data.position = Vector3.zero; // Значение по умолчанию для позиции
        }
        // Передача данных игрока в DataStorage
        DataStorage.Instance.health = data.health;
        DataStorage.Instance.position = data.position;
    }

    private string GetSavePath()
    {
        // Получение пути к папке сохранения в зависимости от платформы
        string saveFolder = Application.persistentDataPath;
        // Соединение пути к папке сохранения и имени файла
        string savePath = Path.Combine(saveFolder, "data.json");

        return savePath;
    }
}