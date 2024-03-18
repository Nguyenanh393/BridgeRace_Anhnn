using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Manager;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Brick
{
    public class BrickController : Brick
    {
        [SerializeField] private int numberBrick;
        [SerializeField] private int numRow;
        [SerializeField] private int numCol;
        [SerializeField] private int numPlayer;
        [SerializeField] private Transform startFloor;
        [SerializeField] private Vector3 startPostion;
        [SerializeField] private Vector3 movePosition;

        [SerializeField] private Transform[] floors;

        private List<Brick>[] listBricks;
        private ColourType currentCharacterColour;
        private static int currentFloor = 0;
        private List<Vector3> listPosition;
        // public static int CurrentFloor
        // {
        //     get => currentFloor;
        //     set => currentFloor = value;
        // }

        void Awake()
        {
            //Brick brick1 = SimplePool.Spawn<Brick>(PoolType.Brick_2, new Vector3(0,0, 1), Quaternion.identity);
            SpawnBrick();
        }

        private void SpawnBrick()
        {
            CreateBrick(numberBrick, numRow, numCol, numPlayer);
        }

        private void Start()
        { 
            InvokeRepeating("ResetFloor", 15, 15);
        }

        private void CreateBrick(int numberBrick, int numRow, int numCol, int numPlayer)
        {
            //float numberBricks = Random.Range(numberBrick, numberBrick + 5);
            listBricks = new List<Brick>[numPlayer];
            for (int i = 0; i < numPlayer; i++)
            {
                listBricks[i] = new List<Brick>();
            }

            startPostion = floors[currentFloor].transform.position;
            for (int i = 0; i < numRow; i++)
            {
                for (int j = 0; j < numCol; j++)
                {
                    Vector3 position = new Vector3(startPostion.x + j + movePosition.x,
                        startPostion.y + movePosition.y,
                        startPostion.z + i + movePosition.z);
                    
                    int colour = Random.Range(1, numPlayer + 1);
                    Brick brick = SimplePool.Spawn<Brick>((PoolType)colour + 1, position, Quaternion.identity);
                    listBricks[colour - 1].Add(brick);
                }
            }
        }

        public void NextFloor(int colour, int floorIndex)
        {
            // if (floorIndex <= currentFloor)
            // {
            //     return;
            // }

            SimplePool.Collect((PoolType)colour + 1);
            listBricks = null;
            SetStartFloor(floorIndex);
            SpawnBrick();
        }

        private void SetStartFloor(int floorIndex)
        {
            currentFloor = floorIndex;
            startFloor = floors[floorIndex];
        }


        public List<Brick> GetBricks(ColourType colourType)
        {
            return listBricks[(int)colourType];
        }

        private void ResetFloor()
        {
            for (int i = 0; i < listBricks.Length; i++)
            {
                SimplePool.Collect((PoolType) i + 1);
            }
        
            listBricks = null;
            SpawnBrick();
        }
        
        
        private void RespawnListBrickColour(int colour)
        {
            SimplePool.Collect((PoolType)colour + 1);
            listBricks[colour - 1] = new List<Brick>();
            startPostion = floors[currentFloor].transform.position;
            for (int i = 0; i < numRow; i++)
            {
                for (int j = 0; j < numCol; j++)
                {
                    Vector3 position = new Vector3(startPostion.x + j + movePosition.x,
                        startPostion.y + movePosition.y,
                        startPostion.z + i + movePosition.z);
                    
                    Brick brick = SimplePool.Spawn<Brick>((PoolType)colour + 1, position, Quaternion.identity);
                    listBricks[colour - 1].Add(brick);
                }
            }
        }
    }
}
