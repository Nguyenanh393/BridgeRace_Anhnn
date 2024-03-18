using _Game.Scripts.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Brick
{
    public class Brick : GameUnit
    {
        [SerializeField] protected ColourType colourType;
        [SerializeField] private Renderer rend;
        [SerializeField] private ColourManager colourManager;
        public ColourType ColourType => colourType;
        
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Character") && collision.GetComponent<Character.Character>().CheckColour(colourType))
            {
                collision.GetComponent<Character.Character>().AddBrick();
                OnDespawn();
            }
        }

        internal void OnDespawn()
        {
            SimplePool.Despawn(this);
        }

        public void ChangeColour(ColourType colour)
        {
            colourType = colour;
            rend.material = colourManager.GetMaterial(colour);
        }
    }
}
