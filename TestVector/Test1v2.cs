/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

using System;
using System.Numerics;

namespace SIMDeefect
{
    partial class Program
    {
        public unsafe static uint VectorSum2(ReadOnlySpan<char> charSpan, int offset, int length, char c, bool alignVector)
        {
            if(5 > length) return 0;

            int vectorSize = Vector<byte>.Count;
            int eltBytes = sizeof(byte);
            var vectorAlignmentMask = vectorSize * (eltBytes << 3) - 1;
            
            var strlen = length;
            var byteLen = length << 1;
            var byteOffset = offset << 1;

            var accumulatorCount = 1;
            var bytesPerBlock = vectorSize;
            var bytesPerLoop = bytesPerBlock * accumulatorCount;
            var charsPerLoop = bytesPerLoop >> 1;

            var blockCount = length / charsPerLoop;

            var remainingChars = length - blockCount * charsPerLoop;

            // NOTE: the second half of vector appears to be empty!! => WHY??!! ( seems to be just a debugger display issue? )

            var mask = new Vector<byte>((byte)c);
            var accResult = Vector<byte>.Zero;
            var i = 0;
            var result = 0U;

            fixed(char* fixedPtr = charSpan)
            {
                var bytePtr = (byte*)fixedPtr + byteOffset;

                var stopMark = bytePtr + byteLen;
                var vectorStopMark = stopMark - bytesPerLoop;

                // consume unaligned elements
                if(alignVector)
                    for (; 0 < ((nint)bytePtr & vectorAlignmentMask) && bytePtr < stopMark; bytePtr++)
                        if (c == *bytePtr) result++;
                
                blockCount = (int)(vectorStopMark - bytePtr) / bytesPerLoop;

                for (i = blockCount; 0 < i; i--)
                {
                    var v = System.Runtime.CompilerServices.Unsafe.Read<Vector<byte>>(bytePtr);
                    var areEqual = Vector.Equals(v, mask);
                    accResult = Vector.Subtract(accResult, areEqual);
                    bytePtr += bytesPerLoop;
                    
                    // prevent overflow
                    if(0 != (0x7F & i)) continue;
                    
                    // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                    // result += Vector.Dot(accResult, Vector<byte>.One);
                    result += SumVectorElts(accResult);
                    accResult = Vector<byte>.Zero;
                }
                // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                // result += Vector.Dot(accResult, Vector<byte>.One);
                result += SumVectorElts(accResult);

                for (; bytePtr < stopMark; bytePtr++)
                    if (c == *bytePtr) result++;
            }

            return result;
        }

        internal static uint Test1vector2(string S, int offset, int length, bool alignVector)
        {
            //                                                               ~0,49
            var counts = new uint[64 * 1024];
            var len = S.Length;

            var a = VectorSum2(S, offset, length, 'A', alignVector);
            var e = VectorSum2(S, offset, length, 'E', alignVector);
            var i = VectorSum2(S, offset, length, 'I', alignVector);
            var o = VectorSum2(S, offset, length, 'O', alignVector);
            var u = VectorSum2(S, offset, length, 'U', alignVector);

            var result = a;
            result = Math.Min(result, e);
            result = Math.Min(result, i);
            result = Math.Min(result, o);
            result = Math.Min(result, u);

            return result;
        }
    }
}
