using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerControl : MonoBehaviour
{
    float inputMoveX, inputMoveY;
    Player playerInput;

    // Start is called before the first frame update
    private void Awake()
    {
        playerInput = ReInput.players.GetPlayer(0);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputMoveX = playerInput.GetAxis("Move Horizontal");
        inputMoveY = playerInput.GetAxis("Move Vertical");
    }

    private void FixedUpdate()
    {
        
    }



}
