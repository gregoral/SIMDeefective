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
        internal static unsafe uint Test1x(string S, int offset, int length, bool alignVector)
        {
            if(5 > length) return 0;

            var counts = new uint[64 * 1024];
            var len = S.Length;

            foreach(var c in S.AsSpan(offset, length)) counts[c]++;

            var result = counts['A'];
            result = Math.Min(result, counts['E']);
            result = Math.Min(result, counts['I']);
            result = Math.Min(result, counts['O']);
            result = Math.Min(result, counts['U']);

            return result;
        }
    }
}
