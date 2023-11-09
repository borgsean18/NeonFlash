using System.Collections;

namespace Combat
{
    public interface IMele
    {
        public void Attack();

        public IEnumerator CoolDown();
    }
}
