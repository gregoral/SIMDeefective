/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace SIMDeefect
{
    partial class Program
    {
        public unsafe static uint VectorSum6(ReadOnlySpan<char> charSpan, int offset, int length, bool alignVector)
        {
            if(5 > length) return 0;

            int vectorSize = Vector256<byte>.Count;
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

            var maskA = Vector256.Create((byte)'A');
            var maskE = Vector256.Create((byte)'E');
            var maskI = Vector256.Create((byte)'I');
            var maskO = Vector256.Create((byte)'O');
            var maskU = Vector256.Create((byte)'U');

            var accResultA = Vector256<byte>.Zero;
            var accResultE = Vector256<byte>.Zero;
            var accResultI = Vector256<byte>.Zero;
            var accResultO = Vector256<byte>.Zero;
            var accResultU = Vector256<byte>.Zero;
            var i = 0;

            uint resultA = 0;
            uint resultE = 0;
            uint resultI = 0;
            uint resultO = 0;
            uint resultU = 0;

            fixed(char* fixedPtr = charSpan)
            {
                var bytePtr = (byte*)fixedPtr + byteOffset;

                var stopMark = bytePtr + byteLen;
                var vectorStopMark = stopMark - bytesPerLoop;

                // consume unaligned elements
                if(alignVector)
                {
                    for (; 0 < ((nint)bytePtr & vectorAlignmentMask) && bytePtr < stopMark; bytePtr++)
                    {
                        if ('A' == *bytePtr) resultA++;
                        if ('E' == *bytePtr) resultE++;
                        if ('I' == *bytePtr) resultI++;
                        if ('O' == *bytePtr) resultO++;
                        if ('U' == *bytePtr) resultU++;
                    }
                }

                blockCount = (int)(vectorStopMark - bytePtr) / bytesPerLoop;

                for (i = blockCount; 0 < i; i--)
                {
                    var vector = Avx2.LoadVector256(bytePtr);

                    var areEqualA = Avx2.CompareEqual(vector, maskA);
                    accResultA = Avx2.Subtract(accResultA, areEqualA);

                    var areEqualE = Avx2.CompareEqual(vector, maskE);
                    accResultE = Avx2.Subtract(accResultE, areEqualE);

                    var areEqualI = Avx2.CompareEqual(vector, maskI);
                    accResultI = Avx2.Subtract(accResultI, areEqualI);

                    var areEqualO = Avx2.CompareEqual(vector, maskO);
                    accResultO = Avx2.Subtract(accResultO, areEqualO);

                    var areEqualU = Avx2.CompareEqual(vector, maskU);
                    accResultU = Avx2.Subtract(accResultU, areEqualU);

                    bytePtr += bytesPerLoop;

                    // prevent overflow
                    if(0 != (0x7F & i)) continue;

                    // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                    // result += Vector.Dot(accResult, Vector<ushort>.One);
                    resultA += SumVectorElts(accResultA);
                    resultE += SumVectorElts(accResultE);
                    resultI += SumVectorElts(accResultI);
                    resultO += SumVectorElts(accResultO);
                    resultU += SumVectorElts(accResultU);

                    accResultA = Vector256<byte>.Zero;
                    accResultE = Vector256<byte>.Zero;
                    accResultI = Vector256<byte>.Zero;
                    accResultO = Vector256<byte>.Zero;
                    accResultU = Vector256<byte>.Zero;
                }
                // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                // result += Vector.Dot(accResult, Vector<ushort>.One);
                resultA += SumVectorElts(accResultA);
                resultE += SumVectorElts(accResultE);
                resultI += SumVectorElts(accResultI);
                resultO += SumVectorElts(accResultO);
                resultU += SumVectorElts(accResultU);

                for (; bytePtr < stopMark; bytePtr++)
                {
                    if ('A' == *bytePtr) resultA++;
                    if ('E' == *bytePtr) resultE++;
                    if ('I' == *bytePtr) resultI++;
                    if ('O' == *bytePtr) resultO++;
                    if ('U' == *bytePtr) resultU++;
                }
            }

            var result = UInt32.MaxValue;
            result = Math.Min(result, resultA);
            result = Math.Min(result, resultE);
            result = Math.Min(result, resultI);
            result = Math.Min(result, resultO);
            result = Math.Min(result, resultU);

            return result;
        }

        internal static uint Test1vector6(string S, int offset, int length, bool alignVector)
        {
            //                                                               ~0,18
            var counts = new uint[64 * 1024];
            var len = S.Length;

            var result = VectorSum6(S, offset, length, alignVector);

            return result;
        }
    }
}
