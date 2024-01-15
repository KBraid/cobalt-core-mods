using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBraid.BraidEili.Actions;

public class AEnemyVolleySpawnFromAllMissileBays : CardAction
{
    public required ASpawn spawn;

    public override void Begin(G g, State s, Combat c)
    {
        if (s.route is Combat)
        {
            timer = 0.0;
            spawn.multiBayVolley = true;
            List<ASpawn> list = new List<ASpawn>();
            int num = 0;
            foreach (Part part in c.otherShip.parts)
            {
                if (part.type == PType.missiles)
                {
                    spawn.fromX = num;
                    spawn.thing.x = num;
                    list.Add(Mutil.DeepCopy(spawn));
                }

                num++;
            }

            c.QueueImmediate(list);
        }
    }
}
