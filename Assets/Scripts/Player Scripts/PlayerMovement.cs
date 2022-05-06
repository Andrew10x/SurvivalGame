
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private State state;
    private Context context;
    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;
    public static CharacterController character_Controller;

    public float jump_Force = 10f;
    public static float vertical_Velocity;

    void Awake() {
        character_Controller = GetComponent<CharacterController>();
    }

    void Update () {
        MoveThePlayer();
	}

    void MoveThePlayer() {

        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
                                     Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();

        character_Controller.Move(move_Direction);


    }

    void ApplyGravity() {

        vertical_Velocity -= gravity * Time.deltaTime;

        state = new Jump(jump_Force, vertical_Velocity);
        context = new Context(state);
        context.Request();

        move_Direction.y = vertical_Velocity * Time.deltaTime;

    }

}

public class Context
{
    State state;

    public Context(State state)
    {
        this.state = state;
    }



    public void Request()
    {
        state.doWork();
    }
}


abstract public class State
{
    public abstract void doWork();
}
class Jump: State
{
    private float jump_Force = 10f;
    private float vertical_Velocity;
    public Jump(float jump_Force, float vertical_Velocity)
    {
        this.jump_Force = jump_Force;
        this.vertical_Velocity = vertical_Velocity;
    }

    public override void doWork()
    {
        if (PlayerMovement.character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_Force;
        }

        PlayerMovement.vertical_Velocity = vertical_Velocity;
    }
}



































