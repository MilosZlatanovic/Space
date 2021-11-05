using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }   
    public void GameIsOver()
    {
        _isGameOver = true;
    }
}
