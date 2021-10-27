using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // player's speed
    public float speed = 5;
    private float _screenHalfWidth, _screenAndPlayerHalfWidth, _playerHalfWidth;
    public event System.Action OnPlayerDeath;
    private Collider2D _col2D;

    // Start is called before the first frame update
    void Start()
    {
        //setting up variables & objects
        _playerHalfWidth = transform.localScale.x / 2;
        _screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        _screenAndPlayerHalfWidth = _screenHalfWidth + _playerHalfWidth;
        _col2D = gameObject.GetComponent<Collider2D>();
        _col2D.isTrigger = false;
    }


    // Update is called once per frame
    void Update()
    {
        //movement controls
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * speed;
        transform.Translate(Vector2.right * (velocity * Time.deltaTime));


        // if block is offscreen, disable player's collision (to avoid gameover from collision offscreen)
        if ((IsOutLeft() || IsOutRight()) && _col2D.isTrigger)
            _col2D.isTrigger = false;
        // if box is on screen, activate collision)
        else if (!(IsOutLeft() || IsOutRight()) && !_col2D.isTrigger)
            _col2D.isTrigger = true;

        // if offscreen player is "too far off screen" in a direction or another, "teleport" it to the other side 
        // (smooth cycling of player instances to seemlessly emulate going from one border of the screen to another)
        if (IsFarOutLeft())
            transform.position = new Vector2(Vector2.zero.x + _screenHalfWidth * 2 - _playerHalfWidth, transform.position.y);
        else if (IsFarOutRight())
            transform.position = new Vector2(Vector2.zero.x  - _screenHalfWidth * 2 + _playerHalfWidth, transform.position.y);
    }
    private bool IsOutLeft()
    {
        return transform.position.x  < Vector2.zero.x -_screenAndPlayerHalfWidth;
    }

    private bool IsOutRight()
    {
        return transform.position.x  > Vector2.zero.x + _screenAndPlayerHalfWidth;
    }

    private bool IsFarOutRight()
    {
        return transform.position.x  > Vector2.zero.x + (_playerHalfWidth + _screenHalfWidth * 2);
    }

    private bool IsFarOutLeft()
    {
        return transform.position.x  < Vector2.zero.x - (_playerHalfWidth + _screenHalfWidth * 2);
    }
    
    // on collision with a collision box of an enemy, player's die (and triggers the gameover event)
    private void OnTriggerEnter2D(Collider2D enemyCollider)
    {
        if (enemyCollider.CompareTag("Enemy"))
        {
            if (OnPlayerDeath != null)
                OnPlayerDeath();
        }
    }
}