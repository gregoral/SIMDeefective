/*
 * Copyright (c) 2022, Gregor Alujevic <gregor.alujevic@yahoo.com>
 *
 * SPDX-License-Identifier: BSD-2-Clause
 */

using System;

namespace SIMDeefect
{
    partial class Program
    {
        static void DevTest1()
        {
            var alignVector = true;

            var testAeiou1 = "SXCWAAIORUEANEY";
            //             A      **     *
            //             E            *  *
            //             I        *
            //             O         *
            //             U           *
            //testAeiou1 =   "SXCWAAIORUEANEY";

            var t1 = Test1(testAeiou1, 4, 11, alignVector);
            var t1v7 = Test1vector7(testAeiou1, 4, 11, alignVector);

            // TestCorrectness3(minAeiouLen: 0, maxAeiouLen: 256, retries: 10000);
            TestCorrectness2();

            // TestCorrectness3(minAeiouLen: 1_000_000, maxAeiouLen: 1_000_100, retries: 10);

            TestCorrectness3(minAeiouLen: 0, maxAeiouLen: 1026, retries: 100);
            TestCorrectness3(minAeiouLen: 32 << 10, maxAeiouLen: (32 << 10) + 10, retries: 100);
            TestCorrectness3(minAeiouLen: 1_000_000, maxAeiouLen: 1_000_100, retries: 10);



            var aeiou = "";
            //         0123456789.123456789.123456789.123456789.123456789.123456789.123456789.
            //         0         1         2         3         4         5         6         7
            //         AEIOUAEIOUAEIOUAEIOUAEIOU

            aeiou = "ELZGFSKBCUAGHJEZCLINDAQXQWUCQSOO"; // OK
            // aeiou = "";
            // aeiou = "";
            // aeiou = "";
            // aeiou = "";
            // aeiou = "";
            // aeiou = "";
            // aeiou = "";

            var len = aeiou.Length;
            var offset = 0;
            var length = len - 1;

            RunStringTestBenchmark("T1  ", aeiou, offset, length, alignVector, Test1, 1);
            RunStringTestBenchmark("T1a ", aeiou, offset, length, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1b ", aeiou, offset, length, alignVector, Test1b, 1);
            RunStringTestBenchmark("T1c ", aeiou, offset, length, alignVector, Test1c, 1);
            RunStringTestBenchmark("T1d ", aeiou, offset, length, alignVector, Test1d, 1);
            RunStringTestBenchmark("T1e ", aeiou, offset, length, alignVector, Test1e, 1);
            RunStringTestBenchmark("T1f ", aeiou, offset, length, alignVector, Test1f, 1);
            RunStringTestBenchmark("Tv1 ", aeiou, offset, length, alignVector, Test1vector1, 1);
            RunStringTestBenchmark("Tv2 ", aeiou, offset, length, alignVector, Test1vector2, 1);
            RunStringTestBenchmark("Tv3 ", aeiou, offset, length, alignVector, Test1vector3, 1);
            RunStringTestBenchmark("Tv4 ", aeiou, offset, length, alignVector, Test1vector4, 1);
            RunStringTestBenchmark("Tv5 ", aeiou, offset, length, alignVector, Test1vector5, 1);
            RunStringTestBenchmark("Tv5a", aeiou, offset, length, alignVector, Test1vector5a, 1);
            RunStringTestBenchmark("Tv5b", aeiou, offset, length, alignVector, Test1vector5b, 1);
            RunStringTestBenchmark("Tv6 ", aeiou, offset, length, alignVector, Test1vector6, 1);
            RunStringTestBenchmark("Tv7 ", aeiou, offset, length, alignVector, Test1vector7, 1);

            //                              0123456789.123456789.123456789.123456789.123456789.123456789.123456789.
            //                              0         1         2         3         4         5         6         7
            RunStringTestBenchmark("T1a ", "AAAAEIOEIOEIOEIOUUUU", 0, 20, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1v2", "AAAAEIOEIOEIOEIOUUUU", 0, 20, alignVector, Test1vector2, 1);

            RunStringTestBenchmark("T1a ", "AAAAAEIOEIOEIOEIOUUUUU", 3, 17, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1v2", "AAAAAEIOEIOEIOEIOUUUUU", 3, 17, alignVector, Test1vector2, 1);

            RunStringTestBenchmark("T1a",  "AEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOU", 0, 45, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1v2", "AEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOU", 0, 45, alignVector, Test1vector2, 1);

            RunStringTestBenchmark("T1a ", "AAAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOUUU", 2, 46, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1v2", "AAAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOAEIOUUU", 2, 46, alignVector, Test1vector2, 1);

            RunStringTestBenchmark("T1a ", "AEIOU", 0, 5, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1v2", "AEIOU", 0, 5, alignVector, Test1vector2, 1);
            RunStringTestBenchmark("T1a ", "", 0, 0, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1v2", "", 0, 0, alignVector, Test1vector2, 1);
            RunStringTestBenchmark("T1a ", "AXEXIXOXU", 0, 9, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1v2", "AXEXIXOXU", 0, 9, alignVector, Test1vector2, 1);
            RunStringTestBenchmark("T1a ", "AAAAEEEEIIIIOOOOUUUU", 0, 17, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1v2", "AAAAEEEEIIIIOOOOUUUU", 0, 17, alignVector, Test1vector2, 1);
            RunStringTestBenchmark("T1a ", "AEIOUAEIOU", 0, 10, alignVector, Test1a, 1);
            RunStringTestBenchmark("T1v2", "AEIOUAEIOU", 0, 10, alignVector, Test1vector2, 1);

            LoooongTest(6000);

            Console.WriteLine("------- T1 finished -------");
            Console.WriteLine();
        }
    }
}
