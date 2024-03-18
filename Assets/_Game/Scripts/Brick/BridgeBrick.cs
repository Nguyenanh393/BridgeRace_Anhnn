using UnityEngine;

namespace _Game.Scripts.Brick
{
    public class BridgeBrick : MonoBehaviour
    {
        [SerializeField] protected ColourType colourType;
        [SerializeField] private Renderer rend;
        [SerializeField] private ColourManager colourManager;
        public ColourType ColourType => colourType;
        public void ChangeColour(ColourType colour)
        {
            colourType = colour;
            rend.material = colourManager.GetMaterial(colour);
        }
    }
}
