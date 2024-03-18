using System;
using System.Collections.Generic;
using _Game.Scripts.Brick;
using _UI.Scripts;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Game.Scripts.StateMachine
{
    public class Enemy : Character.Character
    {
        private IState<Enemy> currentState;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Vector3 destination;
        [SerializeField] private Transform[] targets;
        [SerializeField] private BrickController brickController;
        
        private int destinationIndex;
        private List<Brick.Brick> bricks = new List<Brick.Brick>();
        private void Start()
        {
            base.Start();
            ChangeState(new IdleState());
            bricks = brickController.GetBricks(CharacterColour-1);
            destinationIndex = 0;
        }

        void Update()
        {
            if (!GameManager.IsState(GameState.GamePlay))
            {
                return;
            }
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
            //Move();
        }

        public void ChangeState(IState<Enemy> state)
        {
            if (currentState != null)
            {
                currentState.OnExit(this);
            }

            currentState = state;

            if (currentState != null)
            {
                currentState.OnEnter(this);
            }
        }

        public void SetDestination(Vector3 destination)
        {
            this.destination = destination;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(destination);
        }

        public void StopMoving()
        {
            Debug.Log("Stop moving");
            navMeshAgent.enabled = false;
        }

        private Vector3 TargetBrickPosition()
        {
            int randomIndex = Random.Range(0, bricks.Count);
            Debug.Log("ran" + randomIndex);
            Vector3? randomDirection = bricks[randomIndex].transform.position;
            //randomDirection += transform.position;
            return randomDirection.Value;
            
            // Vector3 direction = bricks[0].transform.position;
            // return direction;
        }
        
        
        private Vector3 TargetMovePosition()
        {
            //Debug.Log(destinationIndex);
            Vector3? randomDirection = targets[destinationIndex].position;
            //randomDirection += transform.position;
            return randomDirection.Value;
        }
        
        
        private void MoveToTarget()
        {
            if (IsOnBridge || CharacterBricksCount == 0)
            {
                if (!CanClimbBridge)
                {
                    Debug.Log("Can't climb bridge");
                    SetDestination(targets[0].position);
                }
            }
            else
            {
                if (CharacterBricksCount> Random.Range(3, 6))
                {
                    SetDestination(TargetMovePosition());
                }
                else
                {
                    SetDestination(TargetBrickPosition());
                }
            }
            
            
        }

        public void Move()
        {
            MoveOnBridge();
            if (Physics.Raycast(TF.position + Vector3.forward, Vector3.down * 2f, out RaycastHit hit, 1f))
            {
                // check colour of the brick
                if (hit.collider.CompareTag("Bridge"))
                {
                    //Debug.Log(hit.collider.GetComponent<BridgeBrick>().ColourType);
                    if (CheckColour(hit.collider.GetComponent<BridgeBrick>().ColourType))
                    {
                        CanClimbBridge = true;
                    }
                    else
                    {
                        CanClimbBridge = false;
                    }
                }

                if (hit.collider.CompareTag("Floor"))
                {
                    CanClimbBridge = true;
                }
            }
            // if (IsOnBridge)
            // {
            //     if (CharacterBricksCount > 0)
            //     {
            //         MoveOnBridge();
            //     }
            //     else
            //     {
            //         if (navMeshAgent.enabled)
            //         {
            //             if (navMeshAgent.velocity.y < 0)
            //             {
            //                 IsGoingDown = true;
            //             }
            //         }
            //     }
            // }
            
            // if (CharacterBricksCount == 0)
            // {
            //         // Debug.Log("Can't climb bridge");
            //         // //StopMoving();
            //         // SetDestination(TargetBrickPosition());
            //         // if (navMeshAgent.enabled)
            //         // {
            //         //     if (navMeshAgent.velocity.y < 0)
            //         //     {
            //         //         IsGoingDown = true;
            //         //     }
            //         // }
            //         //StopMoving();
            //         SetDestination(TargetBrickPosition());
            // }
            // else
            // {
            Debug.Log(IsOnBridge);
            if (!IsOnBridge)
            {
                Debug.Log("Move to target");
                if (CharacterBricksCount >= Random.Range(3, 6))
                {
                    Debug.Log("Move to target");
                    SetDestination(TargetMovePosition()); 
                }
                else
                {
                    SetDestination(TargetBrickPosition());
                }
                
            }
            else
            {
                if (CharacterBricksCount == 0)
                {
                    SetDestination(TargetBrickPosition());
                }
                else
                {
                    SetDestination(TargetMovePosition());
                }
            }
            
        }
        
    }
}
