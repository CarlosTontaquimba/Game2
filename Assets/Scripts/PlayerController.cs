using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject game, enemyGenerator;
    private AudioSource audioPlayer;
    public AudioClip jumpClip;
    public AudioClip dieClip;
    private Animator animator;
    //Reprdcir sonido mientras se toca el suelo
    private float startY;
    //****************
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        //Almacenamos a posicion inicial del personaje cunado toca el suelo
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // 
        bool isGrounded = transform.position.y == startY; 
        //*********************
        bool gamePlaying = game.GetComponent<GameController>().gameState == GameState.Playing;
        if (gamePlaying && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("up")) && isGrounded)
        {
            UpdateState("PlayerJump");
            //Audio cubndo salta 
            audioPlayer.clip = jumpClip;
            audioPlayer.Play();
        }
    }
    public void UpdateState(string state = null)
    {
        if(state != null)
        {
            animator.Play(state);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            UpdateState("PlayerDie");
            game.GetComponent<GameController>().gameState = GameState.Ended;
            enemyGenerator.SendMessage("CancelGenerator", true);
            game.SendMessage("ResetTimeScale",1f);
            //Audio cuando muere
            game.GetComponent<AudioSource>().Stop();
            audioPlayer.clip = dieClip;
            audioPlayer.Play();
        }else if (other.gameObject.tag == "Point")
        {
            game.SendMessage("IncreasePoints");
        }
    }
     void GameReady()
    {
        game.GetComponent<GameController>().gameState = GameState.Ready;
    }
}
