/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

using System;
using System.Numerics;

namespace SIMDeefective
{
    partial class Program
    {
        public unsafe static uint VectorSum3(ReadOnlySpan<char> charSpan, int offset, int length, char c, bool alignVector)
        {
            int vectorSize = Vector<ushort>.Count;
            int eltBytes = sizeof(ushort);
            var vectorAlignmentMask = vectorSize * (eltBytes << 3) - 1;
            
            var strlen = charSpan.Length;

            var accumulatorCount = 1;
            var charsPerBlock = vectorSize;
            var charsPerLoop = charsPerBlock * accumulatorCount;

            var blockCount = length / charsPerLoop;

            var remainingChars = length - blockCount * charsPerLoop;

            // NOTE: the second half of vector appears to be empty!! => WHY??!! ( seems to be just a debugger display issue? )

            var mask = new Vector<ushort>((byte)c);
            var accResult = Vector<ushort>.Zero;
            var i = 0;
            var result = 0U;

            fixed(char* fixedPtr = charSpan)
            {
                var charPtr = fixedPtr + offset;

                var stopMark = charPtr + length;
                var vectorStopMark = stopMark - charsPerLoop;

                // consume unaligned elements
                if(alignVector)
                    for (; 0 < ((nint)charPtr & vectorAlignmentMask) && charPtr < stopMark; charPtr++)
                        if (c == *charPtr) result++;
                
                blockCount = (int)(vectorStopMark - charPtr) / charsPerLoop;

                for (i = blockCount; 0 < i; i--)
                {
                    var vector = System.Runtime.CompilerServices.Unsafe.Read<Vector<ushort>>(charPtr);
                    var areEqual = Vector.Equals(vector, mask);
                    accResult = Vector.Subtract(accResult, areEqual);
                    charPtr += charsPerBlock;

                    // prevent overflow
                    if(0 != (0x7FFF & i)) continue;

                    // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                    // result += Vector.Dot(accResult, Vector<ushort>.One);
                    result += SumVectorElts(accResult);
                    accResult = Vector<ushort>.Zero;
                }
                // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                // result += Vector.Dot(accResult, Vector<ushort>.One);
                result += SumVectorElts(accResult);

                for (; charPtr < stopMark; charPtr++)
                    if (c == *charPtr) result++;
            }


            return result;
        }

        internal static uint Test1vector3(string S, int offset, int length, bool alignVector)
        {
            if(5 > length) return 0;

            var counts = new uint[64 * 1024];
            var len = S.Length;

            var a = VectorSum3(S, offset, length, 'A', alignVector);
            var e = VectorSum3(S, offset, length, 'E', alignVector);
            var i = VectorSum3(S, offset, length, 'I', alignVector);
            var o = VectorSum3(S, offset, length, 'O', alignVector);
            var u = VectorSum3(S, offset, length, 'U', alignVector);

            var result = a;
            result = Math.Min(result, e);
            result = Math.Min(result, i);
            result = Math.Min(result, o);
            result = Math.Min(result, u);

            return result;
        }
    }
}
