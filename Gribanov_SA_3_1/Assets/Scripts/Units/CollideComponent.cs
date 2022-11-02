
using UnityEngine;

namespace Arkanoid.Units
{
    public class CollideComponent : MonoBehaviour
    {
        [SerializeField, Tooltip("Переменная, указывающая на то, является ли объект мячиком.")]
        private bool isBall = false;
        private void OnCollisionEnter(Collision collision)
        {
            //шарик должен отражаться
            if (isBall)
            {
                var direction = Managers.GameManager.Self.World.Direction;
                direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
                Managers.GameManager.Self.World.Direction = direction;
            }
            else
            {
                Managers.GameManager.Self.World.DestroyBlock(gameObject);
            }
        }

    }
}
