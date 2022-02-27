/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

using System;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace SIMDeefect
{
    partial class Program
    {
        public static uint MinArrayElt(UInt32[] uintArray)
        {
            var min = UInt32.MaxValue;
            var arrayLength = uintArray.Length;

            for (var i = arrayLength - 1; 0 <= i; i--)
                min = Math.Min(min, uintArray[i]);

            return min;
        }

        public static uint MinVectorElt(Vector<ushort> ushortVector)
        {
            var min = ushort.MaxValue;
            var vectorLength = Vector<ushort>.Count;

            for (var i = vectorLength - 1; 0 <= i; i--)
                min = Math.Min(min, ushortVector[i]);

            return min;
        }
        public static uint MaxArrayElt(UInt32[] uintArray)
        {
            var max = UInt32.MinValue;
            var arrayLength = uintArray.Length;

            for (var i = arrayLength - 1; 0 <= i; i--)
                max = Math.Max(max, uintArray[i]);

            return max;
        }

        public static uint MaxVectorElt(Vector<ushort> ushortVector)
        {
            var max = ushort.MinValue;
            var vectorLength = Vector<ushort>.Count;

            for (var i = vectorLength - 1; 0 <= i; i--)
                max = Math.Max(max, ushortVector[i]);

            return max;
        }

        public static uint SumArrayElts(UInt32[] uintArray)
        {
            var sum = 0U;
            var arrayLength = uintArray.Length;

            for (var i = arrayLength - 1; 0 <= i; i--)
                sum += uintArray[i];

            return sum;
        }

        public static uint SumVectorElts(Vector<byte> byteVector)
        {
            var sum = 0U;
            var vectorLength = Vector<byte>.Count;

            for (var i = vectorLength - 1; 0 <= i; i--)
                sum += byteVector[i];

            return sum;
        }

        public static uint SumVectorElts(Vector<ushort> ushortVector)
        {
            var sum = 0U;
            var vectorLength = Vector<ushort>.Count;

            for (var i = vectorLength - 1; 0 <= i; i--)
                sum += ushortVector[i];

            return sum;
        }

        public static uint SumVectorElts(Vector<uint> uintVector)
        {
            var sum = 0U;
            var vectorLength = Vector<uint>.Count;

            for (var i = vectorLength - 1; 0 <= i; i--)
                sum += uintVector[i];

            return sum;
        }


        public unsafe static uint SumVectorElts(Vector256<byte> avxByteVector)
        {
            var sum = 0U;
            var vectorLength = Vector256<byte>.Count;

            var byteBuffer = stackalloc byte[vectorLength];
            Avx2.Store(byteBuffer, avxByteVector);

            for (var i = vectorLength - 1; 0 <= i; i--)
                sum += byteBuffer[i];

            return sum;
        }
        public unsafe static uint SumVectorElts(Vector256<ushort> avxUushortVector)
        {
            var sum = 0U;
            var vectorLength = Vector256<ushort>.Count;

            var ushortBuffer = stackalloc ushort[vectorLength];
            Avx2.Store(ushortBuffer, avxUushortVector);

            for (var i = vectorLength - 1; 0 <= i; i--)
                sum += ushortBuffer[i];

            return sum;
        }
    }
}
