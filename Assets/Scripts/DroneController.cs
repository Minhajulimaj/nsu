using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    enum DroneState
    {
        Drone_state_Idle,
        Drone_state_start_takingoff,
        Drone_state_takingoff,
        Drone_state_movingup,
        Drone_state_flying,
        Drone_state_start_landing,
        Drone_state_landing,
        Drone_state_landed,
        Drone_state_wait_ending_stop,
    }
    DroneState state;

    Animator anime;
    Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);

    public float movementspeed = 1.0f;

    public bool IsIdle()
    {
        return (state == DroneState.Drone_state_Idle);
    }

    public void Takeoff()
    {
        state = DroneState.Drone_state_start_takingoff;
    }

    public bool IsFlying()
    {
        return (state == DroneState.Drone_state_flying);
    }

    public void Land()
    {
        state = DroneState.Drone_state_start_landing;
    }

    void Start()
    {
        anime = GetComponent<Animator>();
        //anime.SetBool("TakeOff", true);

        //state = dronestate.drone_state_Idle;
    }

    public void Move(float speedX,float speedZ)
    {
        speed.x = speedX;
        speed.z = speedZ;

        UpdateDrone();
    }

    // Update is called once per frame
    void UpdateDrone()
    {
        switch (state)
        {
            case DroneState.Drone_state_Idle:
                break;

            case DroneState.Drone_state_start_takingoff:
                anime.SetBool("TakeOff", true);
                state = DroneState.Drone_state_takingoff;
                break;

            case DroneState.Drone_state_takingoff:
                if (anime.GetBool("TakeOff") == false)
                {
                    state = DroneState.Drone_state_movingup;
                }
                break;

            case DroneState.Drone_state_movingup:
                if (anime.GetBool("MoveUp") == false)
                {
                    state = DroneState.Drone_state_flying;
                }
                break;

            case DroneState.Drone_state_flying:
                float angleZ = -30.0f * speed.x * 60.0f * Time.deltaTime;
                float angleX = -30.0f * speed.z * 60.0f * Time.deltaTime;
                Vector3 rotation = transform.localRotation.eulerAngles;
                transform.localPosition += speed * movementspeed * Time.deltaTime;
                transform.localRotation = Quaternion.Euler(angleX, rotation.y, angleZ);
                break;

            case DroneState.Drone_state_start_landing:
                anime.SetBool("MoveDown", true);
                state = DroneState.Drone_state_landing;
                break;

            case DroneState.Drone_state_landing:
                if (anime.GetBool("MoveDown") == false)
                {
                    state = DroneState.Drone_state_landed;
                }
                break;

            case DroneState.Drone_state_landed:
                anime.SetBool("Land", true);
                state = DroneState.Drone_state_wait_ending_stop;
                break;

            case DroneState.Drone_state_wait_ending_stop:
                if (anime.GetBool("Land") == false)
                {
                    state = DroneState.Drone_state_Idle;
                }
                break;
        }
       
        
    }
}
