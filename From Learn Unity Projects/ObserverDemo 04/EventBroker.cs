using System;

public class EventBroker
{
    public static event Action ProjectileOutOfBounds;

    public static void CallProjectileOutOfBounds()
    {
        if (ProjectileOutOfBounds != null)
            ProjectileOutOfBounds();
    }
}
