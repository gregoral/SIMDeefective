/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

using System;

namespace SIMDeefective
{
    partial class Program
    {
        internal static unsafe uint Test1e(string S, int offset, int length, bool alignVector)
        {
            if(5 > length) return 0;

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

                for(var i = length >> 3; 0 < i; i--)
                {
                    counts[*(charPtr0)]++;
                    charPtr0 += 4;
                    counts[*(charPtr1)]++;
                    charPtr1 += 4;
                    counts[*(charPtr2)]++;
                    charPtr2 += 4;
                    counts[*(charPtr3)]++;
                    charPtr3 += 4;

                    counts[*(charPtr0)]++;
                    charPtr0 += 4;
                    counts[*(charPtr1)]++;
                    charPtr1 += 4;
                    counts[*(charPtr2)]++;
                    charPtr2 += 4;
                    counts[*(charPtr3)]++;
                    charPtr3 += 4;
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
