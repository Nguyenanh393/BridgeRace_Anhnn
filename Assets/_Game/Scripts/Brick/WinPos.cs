using _UI.Scripts;
using UnityEngine;

namespace _Game.Scripts.Brick
{
    public class WinPos : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                if (other.transform == player)
                {
                    GameManager.ChangeState(GameState.Win);
                }
                else
                {
                    GameManager.ChangeState(GameState.Lose);
                }
            }
        }
    }
}
