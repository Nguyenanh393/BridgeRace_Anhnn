using UnityEngine;

namespace _Game.Scripts.StateMachine
{
    public class PatrolState : IState<Enemy>
    {
        
        public void OnEnter(Enemy enemy)
        {
            Debug.Log("Patrol");
             
            
        }

        public void OnExecute(Enemy enemy)
        {
            enemy.SetAnim(AnimType.Run);
            enemy.Move();
        }

        public void OnExit(Enemy enemy)
        {
        
        }

    }
}
