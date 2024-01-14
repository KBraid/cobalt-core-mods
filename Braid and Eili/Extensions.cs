using System;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;

// Code from Eddie mod
internal static class Extensions
{
    static int recursionLevel = 0;

    public static int GetCurrentCostNoRecursion(this Card card, State s)
    {
        int result = 0;
        recursionLevel++;
        if (recursionLevel < 2)
            result = card.GetCurrentCost(s);
        else
            result = card.GetData(s).cost;
        recursionLevel = 0;
        return result;
    }
    public static void TurnCardToEnergyAttack(State s, Combat c, Card? card, CardAction action, Upgrade upgrade)
    {
        if (card == null)
            return;

        int multiplier;
        if (upgrade == Upgrade.B)
        {
            multiplier = 4;
            c.Queue(new ADestroyCard()
            {
                uuid = card.uuid
            });
        }
        else
        {
            multiplier = 2;
            c.Queue(new AExhaustOtherCard
            {
                uuid = card.uuid
            });
        }
        action.timer = 0.2;

        int cost = card.GetCurrentCostNoRecursion(s);
        c.Queue(new AAttack()
        {
            damage = card.GetDmg(s, multiplier * cost)
        });
    }

    public static void QueueImmediate<T>(this List<T> source, T item)
    {
        source.Insert(0, item);
    }

    public static void Queue<T>(this List<T> source, T item)
    {
        source.Add(item);
    }
    public static T GetModulo<T>(this T[] arr, int index)
    {
        return arr[Mutil.Mod(index, arr.Length)];
    }

    public static T Random<T>(this List<T> list, Rand rng)
    {
        return list[rng.NextInt() % list.Count];
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        Random rnd = new Random();
        return source.OrderBy((T item) => rnd.Next());
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Rand rng)
    {
        Rand rng2 = rng;
        return source.OrderBy((T item) => rng2.NextInt());
    }
}