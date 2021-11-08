using System.Linq;
using UnityEngine;

public class RandomHelper
{
    /// <summary>
    /// generate a random integer based on the random weights
    /// </summary>
    public static int GetRandomIndex(float[] weights = null)
    {
        if (weights == null || weights.Length < 2)
            return 0;

        var r = Random.Range(0, weights.Sum());
        for (int i = 0; i < weights.Length; i++)
        {
            r -= weights[i];
            if (r <= 0)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// convert the floating factor into a factor value.
    /// e.g. 0.2 => 0.8 ~ 1.2
    /// </summary>
    /// <param name="factor"></param>
    /// <returns></returns>
    public static float FloatingFactor(float factor)
    {
        if (factor == 0)
            return 1;

        return 1 + Random.Range(0, factor * 2) - factor;
    }
}
