using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, IStateMachine
{
    public IState CurrentState { get; set; }
    public Transform cameraObject;
    public float speed = 5f;
    public float boostMultiplier = 2f;

    public Vector3 inputDirection;

    private void Start()
    {
        ChangeState(new PlayerNormalState(this));
    }

    private void Update()
    {
        HandleInput();
        CurrentState?.Tick(Time.deltaTime);
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector3(horizontal, 0, vertical);
    }

    public void Move(float speedMultiplier = 1f)
    {
        if (inputDirection == Vector3.zero) return;

        Vector3 direction = cameraObject.forward * inputDirection.z + cameraObject.right * inputDirection.x;
        direction = new Vector3(direction.x, 0, direction.z).normalized;

        transform.position += direction * (speed * speedMultiplier) * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
    }
}


// -------------------- ESTADO NORMAL --------------------
public class PlayerNormalState : IState
{
    private PlayerStateMachine Player;

    public PlayerNormalState(PlayerStateMachine player)
    {
        Player = player;
    }

    public void Enter()
    {
        Debug.Log("Estado: Normal");
    }

    public void Tick(float deltaTime)
    {
        Player.Move();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Player.ChangeState(new PlayerDashState(Player));
        }
    }

    public void Exit() { }
}


// -------------------- ESTADO DASH --------------------
public class PlayerDashState : IState
{
    private PlayerStateMachine Player;
    private float dashDuration = 0.3f;
    private float timer;

    public PlayerDashState(PlayerStateMachine player)
    {
        Player = player;
        timer = dashDuration;
    }

    public void Enter()
    {
        Debug.Log("Estado: Dash");
    }

    public void Tick(float deltaTime)
    {
        timer -= deltaTime;
        Player.Move(Player.boostMultiplier);

        if (timer <= 0)
        {
            Player.ChangeState(new PlayerNormalState(Player));
        }
    }

    public void Exit() { }
}


// -------------------- ESTADO RALENTIZADO --------------------
public class PlayerSlowedState : IState
{
    private PlayerStateMachine Player;
    private float duration;
    private float timer;
    private float slowMultiplier;

    public PlayerSlowedState(PlayerStateMachine player, float duration, float slowMultiplier)
    {
        Player = player;
        this.duration = duration;
        this.slowMultiplier = slowMultiplier;
        timer = duration;
    }

    public void Enter()
    {
        Debug.Log("Estado: Ralentizado");
    }

    public void Tick(float deltaTime)
    {
        timer -= deltaTime;

        if (Player != null)
        {
            Player.Move(slowMultiplier);
        }

        if (timer <= 0)
        {
            Player.ChangeState(new PlayerNormalState(Player));
        }
    }

    public void Exit()
    {
        Debug.Log("Fin del efecto de ralentizado");
    }
}
