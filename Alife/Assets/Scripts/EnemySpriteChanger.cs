﻿using System.Collections.Generic;
using Common;
using UnityEngine;

public class EnemySpriteChanger : BaseBehaviour
{

    Dictionary<Team, Sprite> teamSprites;

    SpriteRenderer spriteRenderer;

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        InnerBus.ChangeTeam.AddListener(ChangeSprite);
    }

    public override void Start()
    {
        spriteRenderer = transform.FindChild("Sprite").GetComponent<SpriteRenderer>();

        teamSprites = new Dictionary<Team, Sprite>()
        {
            {Team.GOOD, Resources.Load<Sprite>("Sprites/Friendly")},
            {Team.EVIL, Resources.Load<Sprite>("Sprites/Enemy")}
        };
    }

    void OnDestroy()
    {
        InnerBus.ChangeTeam.RemoveListener(ChangeSprite);
    }

    void ChangeSprite(Team team)
    {
        spriteRenderer.sprite = teamSprites[team];
    }
}