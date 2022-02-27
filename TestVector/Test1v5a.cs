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
        public unsafe static uint VectorSum5a(ReadOnlySpan<char> charSpan, int offset, int length, bool alignVector)
        {
            if(5 > length) return 0;

            int vectorSize = Vector<ushort>.Count;
            int eltBytes = sizeof(ushort);
            var vectorAlignmentMask = vectorSize * (eltBytes << 3) - 1;

            var accumulatorCount = 2;
            var charsPerBlock = vectorSize;
            var charsPerLoop = charsPerBlock * accumulatorCount;

            var blockCount = length / charsPerLoop;

            var remainingChars = length - blockCount * charsPerLoop;

            // NOTE: the second half of vector appears to be empty!! => WHY??!! ( seems to be just a debugger display issue? )

            var maskA = new Vector<ushort>((byte)'A');
            var maskE = new Vector<ushort>((byte)'E');
            var maskI = new Vector<ushort>((byte)'I');
            var maskO = new Vector<ushort>((byte)'O');
            var maskU = new Vector<ushort>((byte)'U');

            var accResultA = Vector<ushort>.Zero;
            var accResultE = Vector<ushort>.Zero;
            var accResultI = Vector<ushort>.Zero;
            var accResultO = Vector<ushort>.Zero;
            var accResultU = Vector<ushort>.Zero;

            var areEqualA = Vector<ushort>.Zero;
            var areEqualE = Vector<ushort>.Zero;
            var areEqualI = Vector<ushort>.Zero;
            var areEqualO = Vector<ushort>.Zero;
            var areEqualU = Vector<ushort>.Zero;

            var i = 0;

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

                blockCount = (int)(vectorStopMark - charPtr) / charsPerLoop;

                for (i = blockCount; 0 < i; i--)
                {
                    var vector0 = System.Runtime.CompilerServices.Unsafe.Read<Vector<ushort>>(charPtr);
                    charPtr += charsPerBlock;
                    var vector1 = System.Runtime.CompilerServices.Unsafe.Read<Vector<ushort>>(charPtr);
                    charPtr += charsPerBlock;

                    // vector0  ------------------------------------------------------------------------
                    
                    areEqualA = Vector.Equals(vector0, maskA);
                    accResultA = Vector.Subtract(accResultA, areEqualA);

                    areEqualE = Vector.Equals(vector0, maskE);
                    accResultE = Vector.Subtract(accResultE, areEqualE);

                    areEqualI = Vector.Equals(vector0, maskI);
                    accResultI = Vector.Subtract(accResultI, areEqualI);

                    areEqualO = Vector.Equals(vector0, maskO);
                    accResultO = Vector.Subtract(accResultO, areEqualO);

                    areEqualU = Vector.Equals(vector0, maskU);
                    accResultU = Vector.Subtract(accResultU, areEqualU);

                    // vector1  ------------------------------------------------------------------------
                    
                    areEqualA = Vector.Equals(vector1, maskA);
                    accResultA = Vector.Subtract(accResultA, areEqualA);

                    areEqualE = Vector.Equals(vector1, maskE);
                    accResultE = Vector.Subtract(accResultE, areEqualE);

                    areEqualI = Vector.Equals(vector1, maskI);
                    accResultI = Vector.Subtract(accResultI, areEqualI);

                    areEqualO = Vector.Equals(vector1, maskO);
                    accResultO = Vector.Subtract(accResultO, areEqualO);

                    areEqualU = Vector.Equals(vector1, maskU);
                    accResultU = Vector.Subtract(accResultU, areEqualU);


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
        internal static uint Test1vector5a(string S, int offset, int length, bool alignVector)
        {
            //                                                               ~0,14
            var counts = new uint[64 * 1024];
            var len = S.Length;

            var result = VectorSum5a(S, offset, length, alignVector);

            return result;
        }
    }
}
