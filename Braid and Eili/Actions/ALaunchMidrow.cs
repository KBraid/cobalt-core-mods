namespace KBraid.BraidEili.Actions;

public class ALaunchMidrow : ASpawn
{
    public override void Begin(G g, State s, Combat c)
    {
        if (thing is Asteroid)
        {
            c.QueueImmediate(new AEnemyVolleySpawnFromAllMissileBays()
            {
                spawn = new ASpawn()
                {
                    thing = new Asteroid(),
                    fromPlayer = false
                }
            });
        }
        else if (thing is SpaceMine)
        {
            c.QueueImmediate(new AEnemyVolleySpawnFromAllMissileBays()
            {
                spawn = new ASpawn()
                {
                    thing = new SpaceMine(),
                    fromPlayer = false
                }
            });
        }
        else if (thing is Missile)
        {
            c.QueueImmediate(new AEnemyVolleySpawnFromAllMissileBays()
            {
                spawn = new ASpawn()
                {
                    thing = new Missile()
                    {
                        targetPlayer = true,
                    },
                    fromPlayer = false
                }
            });
        }
        c.QueueImmediate(new ASpawn()
        {
            thing = thing,
            fromPlayer = true
        });
    }
}