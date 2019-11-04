using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public  PlayerControl player;
    public EnemyManager enemyManager;

    // Start is called before the first frame update
    private void Awake()
    {
        player.SetEnemyManager(enemyManager);
        enemyManager.SetPlayer(player);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
