namespace Enemies
{
    public class WaspBasic : Wasp
    {
        private void Update()
        {
            if (!Home.Instance) return;

            Move(Home.Instance.transform.position);
        }
    }
}
