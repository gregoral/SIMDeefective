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
        public unsafe static uint VectorSum4(ReadOnlySpan<char> charSpan, int offset, int length, char c, bool alignVector)
        {
            int vectorSize = Vector<ushort>.Count;
            int eltBytes = sizeof(ushort);
            var vectorAlignmentMask = vectorSize * (eltBytes << 3) - 1;

            var strlen = charSpan.Length;

            var accumulatorCount = 4;
            var charsPerBlock = vectorSize;
            var charsPerLoop = charsPerBlock * accumulatorCount;

            var blockCount = length / charsPerLoop;

            var remainingChars = length - blockCount * charsPerLoop;

            // NOTE: the second half of vector appears to be empty!! => WHY??!! ( seems to be just a debugger display issue? )

            var mask = new Vector<ushort>((byte)c);
            var accResult0 = Vector<ushort>.Zero;
            var accResult1 = Vector<ushort>.Zero;
            var accResult2 = Vector<ushort>.Zero;
            var accResult3 = Vector<ushort>.Zero;
            var i = 0;

            var result = 0U;
            var result0 = 0U;
            var result1 = 0U;
            var result2 = 0U;
            var result3 = 0U;

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
                    var vector0 = System.Runtime.CompilerServices.Unsafe.Read<Vector<ushort>>(charPtr);
                    charPtr += charsPerBlock;
                    var areEqual0 = Vector.Equals(vector0, mask);
                    accResult0 = Vector.Subtract(accResult0, areEqual0);

                    var vector1 = System.Runtime.CompilerServices.Unsafe.Read<Vector<ushort>>(charPtr);
                    charPtr += charsPerBlock;
                    var areEqual1 = Vector.Equals(vector1, mask);
                    accResult1 = Vector.Subtract(accResult1, areEqual1);

                    var vector2 = System.Runtime.CompilerServices.Unsafe.Read<Vector<ushort>>(charPtr);
                    charPtr += charsPerBlock;
                    var areEqual2 = Vector.Equals(vector2, mask);
                    accResult2 = Vector.Subtract(accResult2, areEqual2);

                    var vector3 = System.Runtime.CompilerServices.Unsafe.Read<Vector<ushort>>(charPtr);
                    charPtr += charsPerBlock;
                    var areEqual3 = Vector.Equals(vector3, mask);
                    accResult3 = Vector.Subtract(accResult3, areEqual3);

                    // prevent overflow
                    if(0 != (0x7FFF & i)) continue;

                    // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                    // result += Vector.Dot(accResult, Vector<ushort>.One);
                    result0 += SumVectorElts(accResult0);
                    result1 += SumVectorElts(accResult1);
                    result2 += SumVectorElts(accResult2);
                    result3 += SumVectorElts(accResult3);

                    accResult0 = Vector<ushort>.Zero;
                    accResult1 = Vector<ushort>.Zero;
                    accResult2 = Vector<ushort>.Zero;
                    accResult3 = Vector<ushort>.Zero;
                }
                // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                // result += Vector.Dot(accResult, Vector<ushort>.One);
                result0 += SumVectorElts(accResult0);
                result1 += SumVectorElts(accResult1);
                result2 += SumVectorElts(accResult2);
                result3 += SumVectorElts(accResult3);

                for (; charPtr < stopMark; charPtr++)
                    if (c == *charPtr) result++;
            }

            result += result0 + result1 + result2 + result3;

            return result;
        }

        internal static uint Test1vector4(string S, int offset, int length, bool alignVector)
        {
            if(5 > length) return 0;

            var counts = new uint[64 * 1024];
            var len = S.Length;

            var a = VectorSum4(S, offset, length, 'A', alignVector);
            var e = VectorSum4(S, offset, length, 'E', alignVector);
            var i = VectorSum4(S, offset, length, 'I', alignVector);
            var o = VectorSum4(S, offset, length, 'O', alignVector);
            var u = VectorSum4(S, offset, length, 'U', alignVector);

            var result = a;
            result = Math.Min(result, e);
            result = Math.Min(result, i);
            result = Math.Min(result, o);
            result = Math.Min(result, u);

            return result;

        }
    }
}
