using System;
using System.Collections.Generic;
using _Game.Scripts.Brick;
using _UI.Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace _Game.Scripts.Character
{
    public class Character : GameUnit
    {
        [SerializeField] private Animator anim;
        [SerializeField] private Transform brickHolder;
        [SerializeField] private ColourType characterColour;
        [FormerlySerializedAs("colourSO")] [SerializeField] private ColourManager colourManager;
        [SerializeField] private Renderer rend;
        [SerializeField] private GameObject characterBrickPrefab;
        [SerializeField] private LayerMask stairLayer;

        private Stack<CharacterBrick> characterBricks = new Stack<CharacterBrick>();
        private string currentAnimType;
        private Vector3 position;
        private bool onBridge = false;
        private float height;
        private bool isGoingDown = false;
        private bool canClimbBridge = false;
        public bool CanClimbBridge
        {
            get => canClimbBridge;
            set => canClimbBridge = value;
        }
        public bool IsGoingDown
        {
            get => isGoingDown;
            set => isGoingDown = value;
        }

        public int CharacterBricksCount => characterBricks.Count;
        public ColourType CharacterColour => characterColour;

        public bool IsOnBridge
        {
            get => onBridge;
            set => onBridge = value;
        }

        protected void Start()
        {
            if (!GameManager.IsState(GameState.GamePlay))
            {
                return;
            }
            OnInit();
            SetAnim(AnimType.Idle);
        }

        protected void Update()
        {
            //CheckCanClimbBridge();
        }

        protected void OnInit()
        {
            SetAnim(AnimType.Idle);
            
            rend.material = colourManager.GetMaterial(characterColour);
            characterBricks.Clear();
        }

        protected internal void SetAnim(string animType)
        {
            if (currentAnimType == animType)
            {
                return;
            } 
        
            anim.ResetTrigger(animType);
            currentAnimType = animType;
            anim.SetTrigger(animType);
        }

        protected internal void AddBrick()
        {
            if(characterBricks.Count == 0)
            {
                position = Vector3.zero;
            }
            else
            {
                position = characterBricks.Peek().TF.localPosition + Vector3.up * 0.1f * 2f;
            }
            //canClimbBridge = true;
            CharacterBrick characterBrick = SimplePool.Spawn<CharacterBrick>(PoolType.CharacterBrick, position, Quaternion.identity, brickHolder);
            characterBricks.Push(characterBrick);
            characterBrick.ChangeColour(characterColour);
            Debug.Log(characterBricks.Count);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bridge"))
            {
                //onBridge = true;

                if (characterBricks.Count > 0)
                {
                    if(isGoingDown) return;
                    if(!CheckColour(other.GetComponent<BridgeBrick>().ColourType))
                    {
                        RemoveBrick();
                        other.GetComponent<BridgeBrick>().ChangeColour(characterColour);
                    }
                    
                }
            }
        }
        
        // private void OnTriggerExit(Collider other)
        // {
        //     if (other.CompareTag("Bridge"))
        //     {
        //         onBridge = false;
        //     }
        // }

        protected internal void RemoveBrick()
        {
            if(characterBricks.Count == 0)
            {
                return;
            }
            SimplePool.Despawn(characterBricks.Pop());
        }
    
        protected internal bool CheckColour(ColourType colourType)
        {
            if(characterColour == colourType)
            {
                return true;
            }

            return false;
        }
        
        
        protected void MoveOnBridge()
        {
            if (Physics.Raycast(TF.position, Vector3.down, out var hit, 2f, stairLayer))
            {
                onBridge = true;
                height = hit.point.y + Vector3.up.y * 0.5f;
                TF.position = new Vector3(TF.position.x, height, TF.position.z);
                
                Debug.DrawRay(TF.position + Vector3.forward, Vector3.down * 2f, Color.red);
            }
            else
            {
                onBridge = false;
            }
            
        }
        
    }
}
