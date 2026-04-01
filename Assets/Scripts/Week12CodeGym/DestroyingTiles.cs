using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class DestroyingTiles : MonoBehaviour
{
    private Vector2 mousePos;
    private Vector3Int cellPos;

    public List<Tilemap> tilemaps;

    public GameObject highlightIndicator;

    public ParticleSystem particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        cellPos = tilemaps[0].WorldToCell(mousePos);

        //have a highlight to tell where you will dig
        Vector3 pos = tilemaps[0].GetCellCenterWorld(cellPos);
        highlightIndicator.transform.position = pos;

    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            for (int i = 0; i < tilemaps.Count; i++)
            {
                if (tilemaps[i].GetTile(cellPos) != null)
                {
                    tilemaps[i].SetTile(cellPos, null);
                    return;
                }
            }            
        }
        particles.transform.position = tilemaps[0].GetCellCenterWorld(cellPos);
        particles.Emit(20);
    }

}
