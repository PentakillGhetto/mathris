using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    /// <summary>
    /// Puts value between min and max inclusively
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min">Lower bound</param>
    /// <param name="max">Higher bound</param>
    /// <returns>Min if value is bigger than max, max if value is less than min, value otherwise</returns>
    public static int Wrap(int value, int min, int max) => value < min ?
    max - (min - value) % (max + min) :
         min + (value - min) % (max - min);
}
