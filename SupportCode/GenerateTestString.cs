/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

namespace SIMDeefect
{
    partial class Program
    {
        internal static string GenerateTestString(int minAeiouLen = 0, int maxAeiouLen = 1000)
        {
            var rnd = new System.Random();
            var aeiouLen = rnd.Next(minAeiouLen, maxAeiouLen);
            var sb = new System.Text.StringBuilder(aeiouLen + 100);

            while(sb.Length < aeiouLen)
            {
                var ch = (char)(65 + rnd.Next(0, 26));
                sb.Append(ch);
            }

            var aeiou = sb.ToString();
            return aeiou;
        }
    }
}
