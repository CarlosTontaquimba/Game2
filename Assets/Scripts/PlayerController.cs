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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool gamePlaying = game.GetComponent<GameController>().gameState == GameState.Playing;
        if (gamePlaying && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("up")))
        {
            UpdateState("PlayerJump");
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
            //Audio
            game.GetComponent<AudioSource>().Stop();
            audioPlayer.clip = dieClip;
            audioPlayer.Play();
        }
    }
     void GameReady()
    {
        game.GetComponent<GameController>().gameState = GameState.Ready;
    }
}
