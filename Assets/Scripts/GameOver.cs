using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text secondsSurvivedUI;
    private bool gameOver;
    private Player[] _players;
    
    // Start is called before the first frame update
    void Start()
    {
        _players = FindObjectsOfType<Player>();
        foreach (var player in _players)
        {
            player.OnPlayerDeath += OnGameOver;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(0);
                
    }

//show gameover screen if player is hit by a block
    void OnGameOver()
    {

        gameOverScreen.SetActive(true);
        secondsSurvivedUI.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
        gameOver = true;
        foreach (var player in _players)
        {
            Destroy(player.gameObject);
        }
    }
}
