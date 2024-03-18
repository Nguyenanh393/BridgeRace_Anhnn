using UnityEngine;

namespace _Game.Scripts.Brick
{
    public class Floor : MonoBehaviour
    {
        [SerializeField] private BrickController brickController;
        [SerializeField] private int floorIndex;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                brickController.NextFloor((int) other.GetComponent<Character.Character>().CharacterColour, floorIndex);
            }
        }
    }
}
