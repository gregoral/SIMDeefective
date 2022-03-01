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
        static void LoooongTest(uint loopCount)
        {
            var maxAeiouLen = 202_000;
            var rnd = new System.Random();
            var sb = new System.Text.StringBuilder(maxAeiouLen);

            while(sb.Length < maxAeiouLen)
                switch (rnd.Next(0, 10))
                {
                    case 0:
                        sb.Append("AEI");
                        break;
                    case 1:
                        sb.Append("IOU");
                        break;
                    case 2:
                        sb.Append("EIO");
                        break;
                    case 3:
                        sb.Append("IEA");
                        break;
                    case 4:
                        sb.Append("UOI");
                        break;
                    case 5:
                        sb.Append("HASSWELL");
                        break;
                    case 6:
                        sb.Append("IEC");
                        break;
                    case 7:
                        sb.Append("QWERTY");
                        break;
                    case 8:
                        sb.Append("AEIOU");
                        break;
                    case 9:
                        sb.Append("XKCD");
                        break;
                    default:
                        sb.Append("YIO");
                        break;
                }
                
            var aeiou = sb.ToString();
            var aeiouLen = aeiou.Length;

            var length = rnd.Next(aeiouLen - 100, aeiouLen);
            // length = rnd.Next(0, aeiouLen);

            var offset = rnd.Next(0, aeiouLen - length);
            var alignVector = true;

            Console.WriteLine();

            RunStringTestBenchmark("T1    ", aeiou, offset, length, alignVector, Test1,       loopCount);
            RunStringTestBenchmark("T1a   ", aeiou, offset, length, alignVector, Test1a,      loopCount);
            RunStringTestBenchmark("T1b   ", aeiou, offset, length, alignVector, Test1b,      loopCount);
            RunStringTestBenchmark("T1c   ", aeiou, offset, length, alignVector, Test1c,      loopCount);
            RunStringTestBenchmark("T1d   ", aeiou, offset, length, alignVector, Test1d,      loopCount);
            RunStringTestBenchmark("T1e   ", aeiou, offset, length, alignVector, Test1e,      loopCount);
            RunStringTestBenchmark("T1f   ", aeiou, offset, length, alignVector, Test1f,      loopCount);
            RunStringTestBenchmark("T1v1  ", aeiou, offset, length, alignVector, Test1vector1, loopCount);
            RunStringTestBenchmark("T1v2  ", aeiou, offset, length, alignVector, Test1vector2, loopCount);
            RunStringTestBenchmark("T1v3  ", aeiou, offset, length, alignVector, Test1vector3, loopCount);
            RunStringTestBenchmark("T1v4  ", aeiou, offset, length, alignVector, Test1vector4, loopCount);
            RunStringTestBenchmark("T1v5  ", aeiou, offset, length, alignVector, Test1vector5, loopCount);
            RunStringTestBenchmark("T1v5a ", aeiou, offset, length, alignVector, Test1vector5a, loopCount);
            RunStringTestBenchmark("T1v5b ", aeiou, offset, length, alignVector, Test1vector5b, loopCount);
            RunStringTestBenchmark("T1v6  ", aeiou, offset, length, alignVector, Test1vector6, loopCount);
            RunStringTestBenchmark("T1v7  ", aeiou, offset, length, alignVector, Test1vector7, loopCount);

            RunStringTestBenchmark("T1x   ", aeiou, offset, length, alignVector, Test1x,      loopCount);
        }
    }
}
