using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public static DataStorage Instance {get; private set;}

    // Данные игрока, которые нужно передать между сценами
    public int health;
    public Vector3 position;
    public Vector3 camPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
