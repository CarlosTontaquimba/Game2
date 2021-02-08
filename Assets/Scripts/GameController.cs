using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState{Idle,Playing, Ended, Ready};

public class GameController : MonoBehaviour
{
    public float parallaxSpeed = 0.35f;
    public RawImage platform, background;
    public GameState gameState = GameState.Idle;
    public GameObject uiIdle;
    public GameObject player;
    public GameObject enemyGenerator;
    private AudioSource musicPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        bool userAction = Input.GetMouseButtonDown(0) || Input.GetKeyDown("up");
        if (gameState == GameState.Idle && userAction)
        {
            gameState = GameState.Playing;
            //Desactiva u oculta el texto si inicia el juego
            uiIdle.SetActive(false);
            player.SendMessage("UpdateState","PlayerRun");
            enemyGenerator.SendMessage("StartGenerator");
            musicPlayer.Play();
        }else if (gameState == GameState.Playing)
        {
            Parallax();
            //Juego preparado para reiniciarse
        }else if (gameState == GameState.Ready)
        {
            //Si presionamos la tecla arriba 
            if (userAction)
            {
                RestartGame();
            }
        }
    }
    public void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed , 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4 , 0f, 1f, 1f);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Principal");
    }
}
