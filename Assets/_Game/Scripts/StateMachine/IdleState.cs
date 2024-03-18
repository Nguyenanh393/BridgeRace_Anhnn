using System;
using System.Runtime.CompilerServices;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace _Game.Scripts.StateMachine
{
    public class IdleState : IState<Enemy>
    {
        float randomTime;
        float timer;
        public void OnEnter(Enemy enemy)
        {
            enemy.SetAnim(AnimType.Idle);
        }
        public void OnExecute(Enemy enemy)
        {
            timer += Time.deltaTime;
            if (timer >= randomTime)
            {
                enemy.ChangeState(new PatrolState());
            }
        }

        public void OnExit(Enemy enemy)
        {
        
        }

    }
}
