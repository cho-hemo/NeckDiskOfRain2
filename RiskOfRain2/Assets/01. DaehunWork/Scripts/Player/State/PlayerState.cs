using UnityEngine;

public class Player_IdleState : IState
{
    private Player _player;
    public Player_IdleState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
    }

    public void UpdateState()
    {

    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }

    public void AnimationChange()
    {

    }
}

public class Player_WalkState : IState
{
    private Player _player;
    public Player_WalkState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
        _player.SetBool("IsMove", true);
    }
    public void UpdateState()
    {
        if (_player.InputMove == Vector2.zero)
        {
            _player.StateMachine.SetState(new Player_IdleState(_player));
        }
        float targetSpeed = _player.Speed;

        // if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        // // a reference to the players current horizontal velocity
        // float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        // float speedOffset = 0.1f;
        // float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        // // accelerate or decelerate to target speed
        // if (currentHorizontalSpeed < targetSpeed - speedOffset ||
        //     currentHorizontalSpeed > targetSpeed + speedOffset)
        // {
        //     // creates curved result rather than a linear one giving a more organic speed change
        //     // note T in Lerp is clamped, so we don't need to clamp our speed
        //     _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
        //         Time.deltaTime * SpeedChangeRate);

        //     // round speed to 3 decimal places
        //     _speed = Mathf.Round(_speed * 1000f) / 1000f;
        // }
        // else
        // {
        //     _speed = targetSpeed;
        // }   

        // _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        // if (_animationBlend < 0.01f) _animationBlend = 0f;

        // // normalise input direction
        // Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // // if there is a move input rotate player when the player is moving
        // if (_input.move != Vector2.zero)
        // {
        //     _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
        //                       _mainCamera.transform.eulerAngles.y;
        //     float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
        //         RotationSmoothTime);

        //     // rotate to face input direction relative to camera position
        //     transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        // }


        // Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // // move the player
        // _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
        //                  new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }
    public void AnimationChange()
    {

    }
}

public class Player_SprintState : IState
{

    private Player _player;
    public Player_SprintState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
        _player.SetBool("IsSprint", _player.IsSprint);
    }
    public void UpdateState()
    {
    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }
    public void AnimationChange()
    {

    }
}

public class Player_JumpState : IState
{

    private Player _player;
    public Player_JumpState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
        _player.SetTrigger("Jump");
    }
    public void UpdateState()
    {
    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }
    public void AnimationChange()
    {

    }
}

public class Player_DeadState : IState
{
    private Player _player;
    public Player_DeadState(Player player_)
    {
        this._player = player_;
    }

    public void OnEnter()
    {
        _player.SetBool("IsDead", _player.IsDead);
    }
    public void UpdateState()
    {
    }

    public void OnExit()
    {
    }

    public void Action()
    {
    }

    public void ChangeState()
    {
    }
    public void AnimationChange()
    {

    }
}