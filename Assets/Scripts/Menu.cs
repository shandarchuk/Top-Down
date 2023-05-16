using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public bool pauseGame;
    public GameObject pauseGameMenu;

    public void Continue()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1;
        pauseGame = false;
    }

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0;
        pauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void LoadGame()
    {
        // Получение ссылки на компонент DataManager
        DataManager DataManager = FindObjectOfType<DataManager>();

        // Вызов метода загрузки данных игрока
        DataManager.LoadData();
        SceneManager.LoadScene("GameScene");    
    }

    public void NewGame()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
