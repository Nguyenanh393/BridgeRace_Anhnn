using System;
using _Game.Scripts.Brick;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class Player : Character
    {
        [SerializeField] private float speed;
        [SerializeField] private VariableJoystick variableJoystick;
        [SerializeField] private Rigidbody rb;
        
        private bool isMoving = false;
        
        
        // Start is called before the first frame update
        
        // Update is called once per frame

        public void Update()
        {
            base.Update();
            Move();
            MoveOnBridge();
            if (isMoving)
            {
                SetAnim(AnimType.Run);
            }
            else
            {
                SetAnim(AnimType.Idle);
            }
        }

        private void Move()
        {
            if (Physics.Raycast(TF.position + Vector3.forward, Vector3.down * 2f, out RaycastHit hit, 1f))
            {
                // check colour of the brick
                if (hit.collider.CompareTag("Bridge"))
                {
                    Debug.Log(hit.collider.GetComponent<BridgeBrick>().ColourType);
                    if (CheckColour(hit.collider.GetComponent<BridgeBrick>().ColourType))
                    {
                        CanClimbBridge = true;
                    }
                    else
                    {
                        CanClimbBridge = false;
                    }
                }
            }
            if (IsOnBridge)
            {
                if (CharacterBricksCount > 0 || CanClimbBridge)
                {
                    MoveOnBridge();
                }
                else
                {
                    if (variableJoystick.Vertical > 0)
                    {
                        IsGoingDown = false;
                        return;
                    }
                }
            }
            isMoving = variableJoystick.Direction != Vector2.zero;
            IsGoingDown = variableJoystick.Vertical < 0;
            //Debug.DrawRay(TF.position, TF.forward , Color.red);
            if (variableJoystick.Direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(variableJoystick.Direction.x, variableJoystick.Direction.y) * Mathf.Rad2Deg;
                TF.rotation = Quaternion.Euler(0, angle, 0);
                TF.position += TF.forward * (speed * Time.deltaTime);
            
            }
        }
    }
}
