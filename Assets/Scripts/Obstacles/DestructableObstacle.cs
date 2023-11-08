using Combat;

namespace Obstacles
{
    public class DestructableObstacle : Destructable
    {
        public override void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
