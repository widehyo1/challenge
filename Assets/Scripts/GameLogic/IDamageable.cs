using System.Numerics;

public interface IDamageable
{
    public void OnDamage(int damage, Vector2 hitPoint, Vector2 hitNormal);
}
