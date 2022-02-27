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
        internal static unsafe uint Test1x(string S, int offset, int length, bool alignVector)
        {
            /** /
            //                                                               ~0,95
            var counts = new uint[256];
            /*/
            //                                                               ~0,90
            var counts = new uint[64 * 1024];
            /**/
            var len = S.Length;

            //                                                              ~24,40
            // Parallel.ForEach(S, c => counts[c]++ );
            //                                                               ~1,20
            // var span = S.AsSpan();
            // Span<char> stackSpan = stackalloc char[len];
            // span.CopyTo(stackSpan);
            // for(var i = len - 1; 0 <= i; i--) counts[stackSpan[i]]++;

            //                                                               ~0,95
            // for(var i = len - 1; 0 <= i; i--) counts[S[i]]++;
            //                                                               ~0,97
            // var span = S.AsSpan();
            // for(var i = len - 1; 0 <= i; i--) counts[span[i]]++;
            //                                                               ~1,09
            // foreach(var c in S.AsSpan()) counts[(short)c]++;

            // ( requires UNSAFE! )                                          ~1,83
            // fixed (char* p = S)
            //     for(var i = len - 1; 0 <= i; i--) counts[*p]++;

            /**/
            //                                                               ~0,90
            foreach(var c in S.AsSpan(offset, length)) counts[c]++;
            //foreach(var c in S.AsSpan()) counts[(uint)c]++; // eq IL code
            /*/
            var span = S.AsSpan();
            fixed(char* p = span)
            {
                var x = p + len;
                while(x <p) counts[*p]++;
            }
            //                                                               ~0,92

            /**/

            var result = counts['A'];
            result = Math.Min(result, counts['E']);
            result = Math.Min(result, counts['I']);
            result = Math.Min(result, counts['O']);
            result = Math.Min(result, counts['U']);

            return result;
        }
    }
}
