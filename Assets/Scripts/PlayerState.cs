using UnityEngine;

public abstract class PlayerState
{
    protected Movement player;

    public PlayerState(Movement player)
    {
        this.player = player;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}

