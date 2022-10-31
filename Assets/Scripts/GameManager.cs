using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("GameObjects")]
    private GameObject _player;
    // private GameObject _playerTurret;
    // [SerializeField] private GameObject chopperBulletPrefab;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        // _playerTurret = GameObject.FindGameObjectWithTag("PlayerTurret");

        if (_player == null)
            Debug.Log("_player is empty!!!");
        // if (_playerTurret == null)
        //     Debug.Log("_playerTurret is empty!!!");
    }


    public static GameManager Instance
    {
        get
        {
            instance = FindObjectOfType<GameManager>();
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.name = "GameController";
                instance = go.AddComponent<GameManager>();

            }
            return instance;
        }
    }



    public GameObject player
    {
        get
        {
            return _player;
        }
    }

    // public GameObject playerTurret
    // {
    //     get
    //     {
    //
    //         return _playerTurret;
    //     }
    // }

}
