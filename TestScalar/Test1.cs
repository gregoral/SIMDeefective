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
        internal static uint Test1(string S, int offset, int length, bool alignVector)
        {
            if(5 > length) return 0;

            var counts = new uint[26];
            var len = S.Length;

            var stopIdx = offset + length;
            for(var i = offset; i < stopIdx; i++)
                counts[S[i] - 65] += 1;

            var result = counts['A' - 65];
            result = Math.Min(result, counts['E' - 65]);
            result = Math.Min(result, counts['I' - 65]);
            result = Math.Min(result, counts['O' - 65]);
            result = Math.Min(result, counts['U' - 65]);

            return result;
        }

    }
}
