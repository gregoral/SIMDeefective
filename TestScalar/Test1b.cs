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
        internal static uint Test1b(string S, int offset, int length, bool alignVector)
        {
            //                                                               ~0,96
            var counts = new uint[64 * 1024];
            var len = S.Length;

            var span = S.AsSpan();
            for(var i = offset + length - 1; offset <= i; i--) counts[span[i]]++;

            var result = counts['A'];
            result = Math.Min(result, counts['E']);
            result = Math.Min(result, counts['I']);
            result = Math.Min(result, counts['O']);
            result = Math.Min(result, counts['U']);

            return result;
        }


    }
}
