/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

using System;

namespace SIMDeefect
{
    partial class Program
    {
        internal static unsafe uint Test1d(string S, int offset, int length, bool alignVector)
        {
            //                                                               ~0,80
            var counts = new uint[64 * 1024];
            var len = S.Length;

            var span = S.AsSpan();
            fixed(char* fixedPtr = span)
            {
                var charPtr = fixedPtr + offset;
                var stopMark = charPtr + length;

                for(var i = length >> 3; 0 < i; i--)
                {
                    counts[*(charPtr++)]++;
                    counts[*(charPtr++)]++;
                    counts[*(charPtr++)]++;
                    counts[*(charPtr++)]++;
                    counts[*(charPtr++)]++;
                    counts[*(charPtr++)]++;
                    counts[*(charPtr++)]++;
                    counts[*(charPtr++)]++;
                }
                while(charPtr < stopMark) counts[*(charPtr++)]++;
            }

            var result = counts['A'];
            result = Math.Min(result, counts['E']);
            result = Math.Min(result, counts['I']);
            result = Math.Min(result, counts['O']);
            result = Math.Min(result, counts['U']);

            return result;
        }
    }
}
