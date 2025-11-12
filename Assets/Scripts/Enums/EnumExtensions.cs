using System;

namespace Enums
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Chance[%] for a given rarity
        /// </summary>
        /// <param name="rarity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static double Chance(this Rarity rarity)
        {
            return rarity switch
            {
                Rarity.Natural => 10 / 100d,
                Rarity.Supernatural => 4 / 100d,
                Rarity.Ethereal => 2 / 100d,
                Rarity.Imaginary => 0.04d / 100d,
                Rarity.Unfathomable => 0.01d / 100d,
                _ => throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null)
            };
        }
    }
}

