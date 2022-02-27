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
        internal static unsafe uint Test1f(string S, int offset, int length, bool alignVector)
        {
            //                                                               ~0,80
            var counts = new uint[64 * 1024];
            var len = S.Length;

            var span = S.AsSpan();
            fixed(char* fixedPtr = span)
            {
                var charPtr = fixedPtr + offset;
                var stopMark = charPtr + length;

                var charPtr0 = charPtr + 0;
                var charPtr1 = charPtr + 1;
                var charPtr2 = charPtr + 2;
                var charPtr3 = charPtr + 3;
                var charPtr4 = charPtr + 4;
                var charPtr5 = charPtr + 5;
                var charPtr6 = charPtr + 6;
                var charPtr7 = charPtr + 7;

                for(var i = length >> 4; 0 < i; i--)
                {
                    counts[*(charPtr0)]++;
                    charPtr0 += 8;
                    counts[*(charPtr1)]++;
                    charPtr1 += 8;
                    counts[*(charPtr2)]++;
                    charPtr2 += 8;
                    counts[*(charPtr3)]++;
                    charPtr3 += 8;
                    counts[*(charPtr4)]++;
                    charPtr4 += 8;
                    counts[*(charPtr5)]++;
                    charPtr5 += 8;
                    counts[*(charPtr6)]++;
                    charPtr6 += 8;
                    counts[*(charPtr7)]++;
                    charPtr7 += 8;

                    counts[*(charPtr0)]++;
                    charPtr0 += 8;
                    counts[*(charPtr1)]++;
                    charPtr1 += 8;
                    counts[*(charPtr2)]++;
                    charPtr2 += 8;
                    counts[*(charPtr3)]++;
                    charPtr3 += 8;
                    counts[*(charPtr4)]++;
                    charPtr4 += 8;
                    counts[*(charPtr5)]++;
                    charPtr5 += 8;
                    counts[*(charPtr6)]++;
                    charPtr6 += 8;
                    counts[*(charPtr7)]++;
                    charPtr7 += 8;
                }
                while(charPtr0 < stopMark) counts[*(charPtr0++)]++;
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
