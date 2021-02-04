using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float parallaxSpeed = 0.18f;
    public RawImage platform, background;
    public enum GameState{Idle,Playing};
    public GameState gameState = GameState.Idle;
    public GameObject uiIdle;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Idle && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0)))
        {
            gameState = GameState.Playing;
            //Desactiva u oculta el texto si inicia el juego
            uiIdle.SetActive(false);
            player.SendMessage("UpdateState","PlayerRun");
        }else if (gameState == GameState.Playing)
        {
            Parallax();
        }
    }
    public void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed , 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4 , 0f, 1f, 1f);
    }
}
