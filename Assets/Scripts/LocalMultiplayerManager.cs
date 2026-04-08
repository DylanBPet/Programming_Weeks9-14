using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerManager : MonoBehaviour
{

    public List<Sprite> playerSprites;
    public List<PlayerInput> players;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoin(PlayerInput player)
    {
        players.Add(player);
        
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        sr.sprite = playerSprites[player.playerIndex];

        LocalMultiplayerController controller = player.GetComponent<LocalMultiplayerController>();
        controller.manager = this;
    }

    public void playerAttacking(PlayerInput attackingPlayer)
    {

        for (int i = 0; i < players.Count; i++)
        {
            //continue says "if this is true, stop code here, go back and increase [i] then come back"
            if (attackingPlayer == players[i]) continue;

            if (Vector2.Distance(attackingPlayer.transform.position, players[i].transform.position) < 0.5)
            {
                Debug.Log("Player " + attackingPlayer.playerIndex + ": Hit Player " + players[i].playerIndex);
            }
        }

    }
}
