using UnityEngine;

public class ArcherController : BulletShooter
{
    public override bool CanShoot(GameObject obj, Damageable dmg)
    {
        return dmg.armorLevel == ArmorLevel.Light && obj.CompareTag(Tags.Enemy);
    }

    public override GameObject GetBulletPrefab()
    {
        return Prefabs.GetPrefab(Prefabs.BulletType.Arrow);
    }
}
