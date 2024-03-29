﻿using System.Collections;
using Common;
using MyInput;
using UnityEngine;

public class EnemyController : BaseBehaviour
{
    IInput input;
    Team _team = Team.EVIL;

    public Team team
    {
        get
        {
            return _team;
        }
        set
        {
            _team = value;
            InnerBus.ChangeTeam.Invoke(_team);
        }
    }

    #region Unity Hooks

    public override void Awake()
    {
        base.Awake();
        InnerBus.Global.Convert.AddListener(GlobalConvert);
    }

    // Use this for initialization
    public override void Start()
    {
        input = GetComponent<IInput>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAt2D(input.lookAt);
    }

    void OnDestroy()
    {
        InnerBus.Global.OnEnemyDestroyed.Invoke();
        InnerBus.Global.Convert.RemoveListener(GlobalConvert);
    }

    #endregion

    #region Internal API
       
    #endregion

    #region Event Handlers
    /// <summary>
    /// Something needs to switch sides. May or may not be us.
    /// </summary>
    void GlobalConvert(GameObject target)
    {
        if (target == this.gameObject)
        {
            team = Team.GOOD;
        }
        else if (this.team == Team.GOOD)
        {
            //there can only be one ally at a time:
            team = Team.EVIL;
        }
    }
    #endregion
}