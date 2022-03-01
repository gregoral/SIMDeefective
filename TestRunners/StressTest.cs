/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

using System;

namespace SIMDeefective
{
    partial class Program
    {
        private static String ConsoleSeparator = GenerateConsoleSeparator();
        static void StressTest()
        {
            var rnd = new System.Random();
            var stopWatch = new System.Diagnostics.Stopwatch();

            var fnDelegates = new StringTestFnDelegate[] { Test1, Test1a, Test1b, Test1c, Test1d, Test1e, Test1f, Test1x, Test1vector1, Test1vector2, Test1vector3, Test1vector4, Test1vector5, Test1vector5a, Test1vector5b, Test1vector6, Test1vector7 };
            var fnNames = new String[] { "T1", "T1a", "T1b", "T1c", "T1d", "T1e", "T1f", "T1x", "T1v1", "T1v2", "T1v3", "T1v4", "T1v5", "T1v5a", "T1v5b", "T1v6", "T1v7" };

            var alignedErrCounts = new UInt32[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var unalignedErrCounts = new UInt32[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var underCounts = new UInt32[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var overCounts = new UInt32[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var runResults = new UInt32[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var minErrLengths = new UInt32[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var maxErrLengths = new UInt32[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var testRunCount = 0U;

            var testString = "";
            var testStringLen = testString.Length;
            var offset = 0;
            var maxOffset = 0;
            var length = 0;
            var repetition = 0;
            var minAeiouLen = 0;
            var maxAeiouLen = 0; 

            var shortLength = 20_000;
            var mediumLength = 200_000;
            var longLength = 2_000_000;

            var shortRepetitions = 4;
            var mediumRepetitions = 2;
            var longRepetitions = 1;
            var alignVector = true;

            ShowStressResults(testRunCount, fnNames, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, offset, length);
            stopWatch.Restart();

            // test long runs
            for(repetition = 0; repetition < longRepetitions; repetition++)
            {
                minAeiouLen = rnd.Next(longLength, longLength + 11);
                maxAeiouLen = rnd.Next(minAeiouLen, minAeiouLen + 42); 

                testString = GenerateTestString(minAeiouLen, maxAeiouLen);
                testStringLen = testString.Length;

                for(length = 0; length < testStringLen; length += 1007)
                {
                    maxOffset = Math.Min(211, testStringLen - length);
                    for(offset = 0; offset < maxOffset; offset++)
                    {
                        if(0 == (0xFF & testRunCount) && 10000 < stopWatch.ElapsedMilliseconds)
                        {
                            stopWatch.Restart();
                            ShowStressResults(testRunCount, fnNames, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, offset, length);
                        }

                        alignVector = true;
                        SpecificStressTest(fnDelegates, runResults, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, testString, offset, length, alignVector, 2, ref testRunCount);
                        alignVector = false;
                        SpecificStressTest(fnDelegates, runResults, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, testString, offset, length, alignVector, 2, ref testRunCount);
                    }
                }
            }

            if(10000 < stopWatch.ElapsedMilliseconds)
            {
                stopWatch.Restart();
                ShowStressResults(testRunCount, fnNames, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, offset, length);
            }

            // test medium runs
            for(repetition = 0; repetition < mediumRepetitions; repetition++)
            {
                minAeiouLen = rnd.Next(mediumLength, mediumLength + 11);
                maxAeiouLen = rnd.Next(minAeiouLen, minAeiouLen + 42); 

                testString = GenerateTestString(minAeiouLen, maxAeiouLen);
                testStringLen = testString.Length;

                for(length = 0; length < testStringLen; length += 107)
                {
                    maxOffset = Math.Min(211, testStringLen - length);
                    for(offset = 0; offset < maxOffset; offset++)
                    {
                        if(0 == (0xFF & testRunCount) && 10000 < stopWatch.ElapsedMilliseconds)
                        {
                            stopWatch.Restart();
                            ShowStressResults(testRunCount, fnNames, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, offset, length);
                        }

                        alignVector = true;
                        SpecificStressTest(fnDelegates, runResults, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, testString, offset, length, alignVector, 4, ref testRunCount);
                        alignVector = false;
                        SpecificStressTest(fnDelegates, runResults, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, testString, offset, length, alignVector, 4, ref testRunCount);
                    }
                }
            }

            if(10000 < stopWatch.ElapsedMilliseconds)
            {
                stopWatch.Restart();
                ShowStressResults(testRunCount, fnNames, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, offset, length);
            }

            // test short runs
            for(repetition = 0; repetition < shortRepetitions; repetition++)
            {
                minAeiouLen = rnd.Next(shortLength, shortLength + 11);
                maxAeiouLen = rnd.Next(minAeiouLen, minAeiouLen + 42); 

                testString = GenerateTestString(minAeiouLen, maxAeiouLen);
                testStringLen = testString.Length;

                for(length = 0; length < testStringLen; length += 7)
                {
                    maxOffset = Math.Min(211, testStringLen - length);
                    for(offset = 0; offset < maxOffset; offset++)
                    {
                        if(0 == (0xFF & testRunCount) && 10000 < stopWatch.ElapsedMilliseconds)
                        {
                            stopWatch.Restart();
                            ShowStressResults(testRunCount, fnNames, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, offset, length);
                        }

                        alignVector = true;
                        SpecificStressTest(fnDelegates, runResults, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, testString, offset, length, alignVector, 8, ref testRunCount);
                        alignVector = false;
                        SpecificStressTest(fnDelegates, runResults, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, testString, offset, length, alignVector, 8, ref testRunCount);
                    }
                }
            }

            ShowStressResults(testRunCount, fnNames, alignedErrCounts, unalignedErrCounts, underCounts, overCounts, minErrLengths, maxErrLengths, offset, length);
        }
        
        static void ShowStressResults(UInt32 testRunCount, String[] fnNames, UInt32[] alignedErrCounts, UInt32[] unalignedErrCounts, UInt32[] underCounts, UInt32[] overCounts, UInt32[] minErrLengths, UInt32[] maxErrLengths, int offset, int length)
        {
            ConsoleSeparator = GenerateConsoleSeparator();

            var totalErrCount = SumArrayElts(alignedErrCounts) + SumArrayElts(unalignedErrCounts);

            ClearConsoleHelper();

            if(!System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine(ConsoleSeparator);
                MachineInfo();
            }

            Console.WriteLine(ConsoleSeparator);
            Console.WriteLine($"Total tests:  {testRunCount}");
            Console.WriteLine($"Total errors: {totalErrCount}");
            Console.WriteLine();
            Console.WriteLine($"Current offset:  {offset}");
            Console.WriteLine($"Current length:  {length}");
            
            Console.WriteLine(ConsoleSeparator);

            var fnCount = fnNames.Length;

            Console.Write("                   ");
            for (var i = 0; i < fnCount; i++)
                Console.Write($" {fnNames[i],6}");
            Console.WriteLine();

            Console.Write("aligned.errs:      ");
            for (var i = 0; i < fnCount; i++)
                Console.Write($" {alignedErrCounts[i],6}");
            Console.WriteLine();

            Console.Write("unaligned.errs:    ");
            for (var i = 0; i < fnCount; i++)
                Console.Write($" {unalignedErrCounts[i],6}");
            Console.WriteLine();

            Console.WriteLine();

            Console.Write("under.count:       ");
            for (var i = 0; i < fnCount; i++)
                Console.Write($" {underCounts[i],6}");
            Console.WriteLine();

            Console.Write("over.count:        ");
            for (var i = 0; i < fnCount; i++)
                Console.Write($" {overCounts[i],6}");
            Console.WriteLine();

            Console.WriteLine();

            Console.Write("minErrLen:         ");
            for (var i = 0; i < fnCount; i++)
                Console.Write($" {minErrLengths[i],6}");
            Console.WriteLine();

            Console.WriteLine(ConsoleSeparator);
        }

        static bool SpecificStressTest(StringTestFnDelegate[] fnDelegates, UInt32[] runResults, UInt32[] alignedErrCounts, UInt32[] unalignedErrCounts, UInt32[] underCounts, UInt32[] overCounts, UInt32[] minErrLengths, UInt32[] maxErrLengths, string S, int offset, int length, bool alignVector, int repetions, ref uint testRunCount)
        {
            var fnCount = fnDelegates.Length;

            // run each delegate once
            for (var i = 0; i < fnCount; i++)
            {
                testRunCount += 1;
                runResults[i] = fnDelegates[i](S, offset, length, alignVector);
            }
            
            // sum of all results
            var sumAll = SumArrayElts(runResults);

            // drop min and max value, avg the rest
            var midSum = sumAll - MaxArrayElt(runResults) - MinArrayElt(runResults);
            var midSumAvg = midSum / (fnCount - 2);

            var probablyCorrectResult = midSumAvg;
            var probablyWrongResult = false;

            // add error counts
            for (var i = 0; i < fnCount; i++)
            {
                    probablyWrongResult = (probablyCorrectResult != runResults[i]);
                    if(!probablyWrongResult) continue;

                    if(alignVector)
                        alignedErrCounts[i] += 1U;
                    else
                        unalignedErrCounts[i] += 1U;

                    if(probablyCorrectResult > runResults[i]) underCounts[i] += 1U;
                    if(probablyCorrectResult < runResults[i]) overCounts[i] += 1U;

                    maxErrLengths[i] = (uint)Math.Max(maxErrLengths[i], length);

                    minErrLengths[i] = (0 != minErrLengths[i]) ? minErrLengths[i] : (uint)length;
                    minErrLengths[i] = (uint)Math.Min(minErrLengths[i], length);
            }

            // run each delegate a number of repetions
            for (var i = 0; i < fnCount; i++)
                for (var repetion = 0; repetion < repetions; repetion++)
                {
                    testRunCount += 1;
                    runResults[i] = fnDelegates[i](S, offset, length, alignVector);

                    probablyWrongResult = (probablyCorrectResult != runResults[i]);
                    if(!probablyWrongResult) continue;

                    if(alignVector)
                        alignedErrCounts[i] += 1U;
                    else
                        unalignedErrCounts[i] += 1U;

                    if(probablyCorrectResult > runResults[i]) underCounts[i] += 1U;
                    if(probablyCorrectResult < runResults[i]) overCounts[i] += 1U;

                    maxErrLengths[i] = (uint)Math.Max(maxErrLengths[i], length);

                    minErrLengths[i] = (0 != minErrLengths[i]) ? minErrLengths[i] : (uint)length;
                    minErrLengths[i] = (uint)Math.Min(minErrLengths[i], length);
                }

            var totalErrCount = SumArrayElts(alignedErrCounts);

            // test failed if errCount > 0
            return 0 == totalErrCount;
        }
    }
}