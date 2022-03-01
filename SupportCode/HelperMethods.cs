/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

using System;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace SIMDeefective
{
    partial class Program
    {
        static void ErrorWriteLine(string message)
        {
            Console.WriteLine($"\x1B[38;5;1m{message}\x1B[0m");
        }

        static void MachineInfo()
        {
            Console.WriteLine($"Vector.IsHardwareAccelerated:    {Vector.IsHardwareAccelerated}");
            Console.WriteLine($"Vector<ushort>.Count:            {Vector<ushort>.Count}");
            Console.WriteLine($"Vector128<ushort>.Count:         {Vector128<ushort>.Count}");
            Console.WriteLine($"Vector256<ushort>.Count:         {Vector256<ushort>.Count}");
            Console.WriteLine($"Avx2.IsSupported:                {Avx2.IsSupported}");
        }

        static void ClearConsoleHelper()
        {
            if(System.Diagnostics.Debugger.IsAttached) return;
            Console.Clear();
        }
        static String GenerateConsoleSeparator(int defultConsoleWidth = 80, char separator = '-')
        {
            var width = (!System.Diagnostics.Debugger.IsAttached) ? Console.WindowWidth : defultConsoleWidth;
            return (width == ConsoleSeparator?.Length) ? ConsoleSeparator : new String(separator, width - 2);
        }

        delegate uint StringTestFnDelegate(string S, int offset, int length, bool alignVector);

        static uint RunStringTestBenchmark(string testName, string testString, int offset, int length, bool alignVector, StringTestFnDelegate fn, uint loopCount)
        {
            var timer = new System.Diagnostics.Stopwatch();

            var result = fn(testString, offset, length, alignVector);

            timer.Restart();
            for(var loop = loopCount; 0 < loop; loop--)
                fn(testString, offset, length, alignVector);
            timer.Stop();

            var duration = timer.Elapsed.TotalSeconds;
            Console.WriteLine($"{testName} . {loopCount} x aeiou.count(S.len:{length}) result: {result} duration: {duration}s");
            Console.WriteLine();

            return result;
        }
        static string MinimizeErrorAeiouInput(string S, int offset, int length)
        {
            var testSample = "";
            var charsRemoved = 0;

            var isError = TestCorrectness1(S, offset, length);

            // end trim
            while(true)
            {
                testSample = S.Substring(0, S.Length - 1);
                if(TestCorrectness1(testSample, offset, length)) break;

                S = testSample;
                charsRemoved += 1;
            }

            // start trim
            while(true)
            {
                testSample = S.Substring(1, S.Length - 1);
                if(TestCorrectness1(testSample, offset + 1, length)) break;

                S = testSample;
                charsRemoved += 1;
            }

            return S;
        }
    }
}
