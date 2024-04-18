using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLookat : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _npcSprite;

    [SerializeField] private GameObject _npc;
    [SerializeField] private GameObject _player;

    private void Update()
    {
        float xMagnitude = _player.transform.position.x - _npc.transform.position.x;

        if (xMagnitude > 0)
        {
            _npcSprite.flipX = true;
        }
        else
        {
            _npcSprite.flipX = false;
        }
    }
}
