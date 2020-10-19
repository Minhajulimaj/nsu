using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public DroneController droneController;

    public Button flybutton;
    public Button landbutton;

    //public GameObject controls;

    struct DroneAnimationControl
    {
        public bool moving;
        public bool interpoAsc;
        public bool interpoDesc;
        public float axis;
        public float direction;
    }

    DroneAnimationControl moving_left;
    DroneAnimationControl moving_back;
    // Start is called before the first frame update
    void Start()
    {
        flybutton.onClick.AddListener(EventOnClickFlyButton);
        landbutton.onClick.AddListener(EventOnClickLandButton);
    }

    // Update is called once per frame
    void Update()
    {
        //float speedX = Input.GetAxis("Horizontal");
        //float speedZ = Input.GetAxis("Vertical");

        UpdateControle(ref moving_left);
        UpdateControle(ref moving_back);

        droneController.Move(moving_left.axis * moving_left.direction, moving_back.axis *moving_back.direction);
    }

    void UpdateControle(ref DroneAnimationControl control)
    {
        if(control.moving || control.interpoAsc || control.interpoDesc)
        {
            if (control.interpoAsc)
            {
                control.axis += 0.05f;
                if(control.axis >= 1.0f)
                {
                    control.axis = 1.0f;
                    control.interpoAsc = false;
                    control.interpoDesc = true;
                }
            }
            else if (!control.moving)
            {
                control.axis -= 0.05f;
                if(control.axis <= 0.0f)
                {
                    control.axis = 0.0f;
                    control.interpoDesc = false;
                }
            }
        }
    }

    void EventOnClickFlyButton()
    {
        if (droneController.IsIdle())
        {
            droneController.Takeoff();
            
        }
    }
    void EventOnClickLandButton()
    {
        if (droneController.IsFlying())
        {
            droneController.Land();

        }
    }

    public void EventOnLeftButtonPressed()
    {
        moving_left.moving = true;
        moving_left.interpoAsc = true;
        moving_left.direction = -1.0f;
    }

    public void EventOnLeftButtonReleased()
    {
        moving_left.moving = false;
    }

    public void EventOnRightButtonPressed()
    {
        moving_left.moving = true;
        moving_left.interpoAsc = true;
        moving_left.direction = 1.0f;
    }

    public void EventOnRightButtonReleased()
    {
        moving_left.moving = false;
    }

    public void EventOnBackButtonPressed()
    {
        moving_back.moving = true;
        moving_back.interpoAsc = true;
        moving_back.direction = -1.0f;
    }

    public void EventOnBackButtonReleased()
    {
        moving_back.moving = false;
    }

    public void EventOnForwordButtonPressed()
    {
        moving_back.moving = true;
        moving_back.interpoAsc = true;
        moving_back.direction = 1.0f;
    }

    public void EventOnForwordButtonReleased()
    {
        moving_back.moving = false;
    }
}
