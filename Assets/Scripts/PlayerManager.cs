using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player player;
    private Player[] _players;
    private float _screenHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        _players = new Player[2];
        _screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        CreatePlayers();
    }

    void CreatePlayers()
    {
        //creating 3 players to allow seemlessly "teleporting" from one border of the screen to another. Also offsets their spawn positions (1 onscreen, 2 offscreen)
        for (int i = 0; i < 2; i++)
        {
            player = Instantiate(player);
            switch (i)
            {
                case 0:
                    player.transform.position =
                        new Vector2(0 - _screenHalfWidth * 2, -3.5f);
                    break;
                case 1:
                    player.transform.position = new Vector2(0, -3.5f);
                    break;
                case 2:
                    player.transform.position =
                        new Vector2(0 + _screenHalfWidth * 2, -3.5f);
                    break;
            }

            _players[i] = player;
        }
    }
}