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
        public unsafe static uint VectorSum1(uint[] uintArray, int offset, int length, char c, bool alignVector)
        {
            int vectorSize = Vector<uint>.Count;
            int eltBytes = sizeof(uint);
            var vectorAlignmentMask = vectorSize * (eltBytes << 3) - 1;

            var strlen = length;
            var uintLen = length;
            var uintOffset = offset;

            var accumulatorCount = 1;
            var bytesPerBlock = vectorSize * eltBytes;
            var bytesPerLoop = bytesPerBlock * accumulatorCount;
            var uintsPerLoop = bytesPerLoop / eltBytes;

            var blockCount = length / uintsPerLoop;

            var mask = new Vector<uint>(c);
            var accResult = new Vector<uint>();
            
            var i = 0;
            //var stopMark = uintOffset + uintLen;

            var result = 0U;

            fixed(uint* fixedPtr = uintArray)
            {
                var uintPtr = (uint*)fixedPtr + uintOffset;

                var stopMark = uintPtr + uintLen;
                var vectorStopMark = stopMark - bytesPerLoop;

                // consume unaligned elements
                if(alignVector)
                    for (; 0 < ((nint)uintPtr & vectorAlignmentMask) && uintPtr < stopMark; uintPtr++)
                        if (c == *uintPtr) result++;

                blockCount = (int)(vectorStopMark - uintPtr) / bytesPerLoop;

                for (i = blockCount; 0 < i; i--)
                {
                    var v = System.Runtime.CompilerServices.Unsafe.Read<Vector<uint>>(uintPtr);
                    var areEqual = Vector.Equals(v, mask);
                    accResult = Vector.Subtract(accResult, areEqual);
                    uintPtr += uintsPerLoop;

                    // prevent overflow
                    if(0 != (0x7FFFFFFF & i)) continue;
                    
                    // NOTE: vector.dot can produce an overflow if the sum > Vector<uint>.Max
                    // result += Vector.Dot(accResult, Vector<uint>.One);
                    result += SumVectorElts(accResult);
                    accResult = Vector<uint>.Zero;
                }
                // NOTE: vector.dot can produce an overflow if the sum > Vector<uint>.Max
                // result += Vector.Dot(accResult, Vector<uint>.One);
                result += SumVectorElts(accResult);

                for (; uintPtr < stopMark; uintPtr++)
                    if (c == *uintPtr) result++;
            }

            return result;
        }

        internal static uint Test1vector1(string S, int offset, int length, bool alignVector)
        {
            if(5 > length) return 0;

            var counts = new uint[64 * 1024];
            var len = S.Length;

            var charArray = S.ToCharArray();
            var uintArray = new uint[len];
            charArray.CopyTo(uintArray, 0);

            var a = VectorSum1(uintArray, offset, length, 'A', alignVector);
            var e = VectorSum1(uintArray, offset, length, 'E', alignVector);
            var i = VectorSum1(uintArray, offset, length, 'I', alignVector);
            var o = VectorSum1(uintArray, offset, length, 'O', alignVector);
            var u = VectorSum1(uintArray, offset, length, 'U', alignVector);

            var result = a;
            result = Math.Min(result, e);
            result = Math.Min(result, i);
            result = Math.Min(result, o);
            result = Math.Min(result, u);

            return result;
        }
    }
}
