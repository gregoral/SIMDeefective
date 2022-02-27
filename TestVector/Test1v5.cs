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
        public unsafe static uint VectorSum5(ReadOnlySpan<char> charSpan, int offset, int length, bool alignVector)
        {
            if(5 > length) return 0;

            int vectorSize = Vector<ushort>.Count;
            int eltBytes = sizeof(ushort);
            var vectorAlignmentMask = vectorSize * (eltBytes << 3) - 1;

            var accumulatorCount = 1;
            var charsPerBlock = vectorSize;
            var charsPerLoop = charsPerBlock * accumulatorCount;

            // NOTE: the second half of vector appears to be empty!! => WHY??!! ( seems to be just a debugger display issue? )

            var maskA = new Vector<ushort>((ushort)'A');
            var maskE = new Vector<ushort>((ushort)'E');
            var maskI = new Vector<ushort>((ushort)'I');
            var maskO = new Vector<ushort>((ushort)'O');
            var maskU = new Vector<ushort>((ushort)'U');

            var accResultA = Vector<ushort>.Zero;
            var accResultE = Vector<ushort>.Zero;
            var accResultI = Vector<ushort>.Zero;
            var accResultO = Vector<ushort>.Zero;
            var accResultU = Vector<ushort>.Zero;

            uint resultA = 0;
            uint resultE = 0;
            uint resultI = 0;
            uint resultO = 0;
            uint resultU = 0;

            fixed(char* fixedPtr = charSpan)
            {
                var charPtr = fixedPtr + offset;

                var stopMark = charPtr + length;
                var vectorStopMark = stopMark - charsPerLoop;

                // consume unaligned elements
                if(alignVector)
                {
                    for (; 0 < ((nint)charPtr & vectorAlignmentMask) && charPtr < stopMark; charPtr++)
                    {
                        if ('A' == *charPtr) resultA++;
                        if ('E' == *charPtr) resultE++;
                        if ('I' == *charPtr) resultI++;
                        if ('O' == *charPtr) resultO++;
                        if ('U' == *charPtr) resultU++;
                    }
                }

                var blockCount = (int)(vectorStopMark - charPtr) / charsPerLoop;

                for (var i = blockCount; 0 < i; i--)
                {
                    var vector = System.Runtime.CompilerServices.Unsafe.Read<Vector<ushort>>(charPtr);

                    var areEqualA = Vector.Equals(vector, maskA);
                    accResultA = Vector.Subtract(accResultA, areEqualA);

                    var areEqualE = Vector.Equals(vector, maskE);
                    accResultE = Vector.Subtract(accResultE, areEqualE);

                    var areEqualI = Vector.Equals(vector, maskI);
                    accResultI = Vector.Subtract(accResultI, areEqualI);

                    var areEqualO = Vector.Equals(vector, maskO);
                    accResultO = Vector.Subtract(accResultO, areEqualO);

                    var areEqualU = Vector.Equals(vector, maskU);
                    accResultU = Vector.Subtract(accResultU, areEqualU);

                    charPtr += charsPerBlock;

                    // prevent overflow
                    if(0 != (0x7FFF & i)) continue;

                    // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                    // result += Vector.Dot(accResult, Vector<ushort>.One);
                    resultA += SumVectorElts(accResultA);
                    resultE += SumVectorElts(accResultE);
                    resultI += SumVectorElts(accResultI);
                    resultO += SumVectorElts(accResultO);
                    resultU += SumVectorElts(accResultU);

                    accResultA = Vector<ushort>.Zero;
                    accResultE = Vector<ushort>.Zero;
                    accResultI = Vector<ushort>.Zero;
                    accResultO = Vector<ushort>.Zero;
                    accResultU = Vector<ushort>.Zero;
                }
                // NOTE: vector.dot can produce an overflow if the sum > Vector<ushort>.Max
                // result += Vector.Dot(accResult, Vector<ushort>.One);
                resultA += SumVectorElts(accResultA);
                resultE += SumVectorElts(accResultE);
                resultI += SumVectorElts(accResultI);
                resultO += SumVectorElts(accResultO);
                resultU += SumVectorElts(accResultU);

                for (; charPtr < stopMark; charPtr++)
                {
                    if ('A' == *charPtr) resultA++;
                    if ('E' == *charPtr) resultE++;
                    if ('I' == *charPtr) resultI++;
                    if ('O' == *charPtr) resultO++;
                    if ('U' == *charPtr) resultU++;
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

        internal static uint Test1vector5(string S, int offset, int length, bool alignVector)
        {
            //                                                               ~0,14
            var counts = new uint[64 * 1024];
            var len = S.Length;

            var result = VectorSum5(S, offset, length, alignVector);

            return result;
        }
    }
}
