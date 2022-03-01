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
        static bool TestCorrectness1(string S, int offset, int length)
        {
            var alignVector = true;

            var r1 = Test1(S, offset, length, alignVector);
            var r1a = Test1a(S, offset, length, alignVector);
            var r1b = Test1b(S, offset, length, alignVector);
            var r1c = Test1c(S, offset, length, alignVector);
            var r1d = Test1d(S, offset, length, alignVector);
            var r1e = Test1e(S, offset, length, alignVector);
            var r1f = Test1f(S, offset, length, alignVector);
            var rv1 = Test1vector1(S, offset, length, alignVector);
            var rv2 = Test1vector2(S, offset, length, alignVector);
            var rv3 = Test1vector3(S, offset, length, alignVector);
            var rv4 = Test1vector4(S, offset, length, alignVector);
            var rv5 = Test1vector5(S, offset, length, alignVector);
            var rv5a = Test1vector5a(S, offset, length, alignVector);
            var rv5b = Test1vector5b(S, offset, length, alignVector);
            var rv6 = Test1vector6(S, offset, length, alignVector);
            var rv7 = Test1vector7(S, offset, length, alignVector);

            var r1x = Test1x(S, offset, length, alignVector);

            var allok = true;
            allok &= (r1 == r1a);
            allok &= (r1 == r1b);
            allok &= (r1 == r1c);
            allok &= (r1 == r1d);
            allok &= (r1 == r1e);
            allok &= (r1 == r1f);

            allok &= (r1 == rv1);
            allok &= (r1 == rv2);
            allok &= (r1 == rv3);
            allok &= (r1 == rv4);
            allok &= (r1 == rv5);
            allok &= (r1 == rv5a);
            allok &= (r1 == rv5b);
            allok &= (r1 == rv6);
            allok &= (r1 == rv7);

            allok &= (r1 == r1x);
            if(allok) return true;

            // this input results in at least 1 difference

            ErrorWriteLine("ERROR(s) DETECTED!");
            ErrorWriteLine($"    r1:   {r1}");
            ErrorWriteLine($"    r1a:  {r1a}");
            ErrorWriteLine($"    r1b:  {r1b}");
            ErrorWriteLine($"    r1c:  {r1c}");
            ErrorWriteLine($"    r1d:  {r1d}");
            ErrorWriteLine($"    r1e:  {r1e}");
            ErrorWriteLine($"    r1f:  {r1f}");
            ErrorWriteLine($"    rv1:  {rv1}");
            ErrorWriteLine($"    rv2:  {rv2}");
            ErrorWriteLine($"    rv3:  {rv3}");
            ErrorWriteLine($"    rv4:  {rv4}");
            ErrorWriteLine($"    rv5:  {rv5}");
            ErrorWriteLine($"    rv5a: {rv5a}");
            ErrorWriteLine($"    rv5b: {rv5b}");
            ErrorWriteLine($"    rv6:  {rv6}");
            ErrorWriteLine($"    rv7:  {rv7}");
            
            var len = S.Length;
            if(100 > len) ErrorWriteLine($"[{offset} .. {offset + length}]{S}");

            return false;
        }

        static bool TestCorrectness2()
        {
            //           0                                                                                                   1                                                                                                   2
            //           0123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.
            //           0         1         2         3         4         5         6         7         8         9         0         1         2         3         4         5         6         7         8         9         0
            var aeiou = "AEIOUXXXXXXXAEIOUXXXXXXAEIOUXXXXXAEIOUXXXXAEIOUXXXAEIOUXXAEIOUXAEIOUXXXXXXXUOIEAUOIEAXXXXXXUOIEAUOIEAXXXXXUOIEAUOIEAXXXXUOIEAUOIEAXXXUOIEAUOIEAXXUIIEXEEAXXUIIXEEEAIIXUAEIOUAEIOXXXXXXIIUAEIOXXIIUAEIOXX";
            var alignVector = true;

            RunStringTestBenchmark("T1", aeiou, 0, 74, alignVector, Test1, 1);
            RunStringTestBenchmark("T1v2", aeiou, 0, 74, alignVector, Test1vector2, 1);

            var allok = true;
            var aeiouLen = 0;

            //         0                                                                                                   1                                                                                                   2
            //         0123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.
            //         0         1         2         3         4         5         6         7         8         9         0
            aeiou = "XAXEXIXOXUXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXAXEXIXOXUXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXAXEXIXOXUXXX";
            aeiouLen = aeiou.Length;

            for(var i = 0; i <= aeiouLen; i++)
                for(var j = i; j <= aeiouLen; j++)
                    allok &= TestCorrectness1(aeiou, i, j - i);


            var rnd = new System.Random();
            var repetitions = 8;
            for(var repetition = 0; repetition < repetitions; repetition++)
            {
                Console.WriteLine($"repetition: {repetition+1} of {repetitions}");

                var minAeiouLen = rnd.Next(117, 211);
                var maxAeiouLen = rnd.Next(minAeiouLen, minAeiouLen + 42); 

                aeiou = GenerateTestString(minAeiouLen, maxAeiouLen);

                aeiouLen = aeiou.Length;
                for(var i = 0; i < aeiouLen; i++)
                    for(var j = i; j < aeiouLen; j += 7)
                        allok &= TestCorrectness1(aeiou, i, j - i);

                if(allok) continue;

                ErrorWriteLine("ERROR(s) DETECTED!");
            }

            if(allok) Console.WriteLine("------- no correctness1 issues detected. -------");
            return allok;
        }

        static bool TestCorrectness3(int minAeiouLen = 0, int maxAeiouLen = 1000, uint retries = 100)
        {
            var alignVector = true;

            var rnd = new System.Random();
            var sb = new System.Text.StringBuilder(maxAeiouLen);

            Console.WriteLine($"testing rnd[len]: {retries} x [{minAeiouLen} .. {maxAeiouLen}]");
            for(var len = minAeiouLen; len < maxAeiouLen; len++)
            {
                Console.Write($"{len,4}, ");
                if(0 == len % 20) Console.WriteLine();

                for(var attempt = 0; attempt < retries; attempt++)
                {
                    sb.Clear();

                    while(sb.Length < len)
                    {
                        var ch = (char)(65 + rnd.Next(0, 26));
                        sb.Append(ch);
                    }

                    var aeiou = sb.ToString();
                    var aeiouLen = aeiou.Length;

                    var length = rnd.Next(0, aeiouLen);
                    var offset = rnd.Next(0, aeiouLen - length);

                    var r1 = Test1(aeiou, offset, length, alignVector);
                    var r1a = Test1a(aeiou, offset, length, alignVector);
                    var r1b = Test1b(aeiou, offset, length, alignVector);
                    var r1c = Test1c(aeiou, offset, length, alignVector);
                    var r1d = Test1d(aeiou, offset, length, alignVector);
                    var r1e = Test1e(aeiou, offset, length, alignVector);
                    var r1f = Test1f(aeiou, offset, length, alignVector);
                    var rv1 = Test1vector1(aeiou, offset, length, alignVector);
                    var rv2 = Test1vector2(aeiou, offset, length, alignVector);
                    var rv3 = Test1vector3(aeiou, offset, length, alignVector);
                    var rv4 = Test1vector4(aeiou, offset, length, alignVector);
                    var rv5 = Test1vector5(aeiou, offset, length, alignVector);
                    var rv5a = Test1vector5a(aeiou, offset, length, alignVector);
                    var rv5b = Test1vector5b(aeiou, offset, length, alignVector);
                    var rv6 = Test1vector6(aeiou, offset, length, alignVector);
                    var rv7 = Test1vector7(aeiou, offset, length, alignVector);

                    var r1x = Test1x(aeiou, offset, length, alignVector);

                    var allok = true;

                    allok &= (r1 == r1a);
                    allok &= (r1 == r1b);
                    allok &= (r1 == r1c);
                    allok &= (r1 == r1d);
                    allok &= (r1 == r1e);
                    allok &= (r1 == r1f);

                    allok &= (r1 == rv1);
                    allok &= (r1 == rv2);
                    allok &= (r1 == rv3);
                    allok &= (r1 == rv4);
                    allok &= (r1 == rv5);
                    allok &= (r1 == rv5a);
                    allok &= (r1 == rv5b);
                    allok &= (r1 == rv6);
                    allok &= (r1 == rv7);

                    allok &= (r1 == r1x);
                    if(allok) continue;

                    // rerun the same test to rule out HW error ( get repeatable results )
                    var isError =  true;
                    
                    isError = TestCorrectness1(aeiou, offset, length);
                    if(!isError) continue;
                    isError = TestCorrectness1(aeiou, offset, length);
                    if(!isError) continue;
                    isError = TestCorrectness1(aeiou, offset, length);
                    if(!isError) continue;

                    // this input results in at least 1 difference

                    var minErrorAeiou = MinimizeErrorAeiouInput(aeiou, offset, length);

                    ErrorWriteLine("ERROR(s) DETECTED!");
                    ErrorWriteLine($"    r1:   {r1}");
                    ErrorWriteLine($"    r1a:  {r1a}");
                    ErrorWriteLine($"    r1b:  {r1b}");
                    ErrorWriteLine($"    r1c:  {r1c}");
                    ErrorWriteLine($"    r1d:  {r1d}");
                    ErrorWriteLine($"    r1e:  {r1e}");
                    ErrorWriteLine($"    r1f:  {r1f}");
                    ErrorWriteLine($"    rv1:  {rv1}");
                    ErrorWriteLine($"    rv2:  {rv2}");
                    ErrorWriteLine($"    rv3:  {rv3}");
                    ErrorWriteLine($"    rv4:  {rv4}");
                    ErrorWriteLine($"    rv5:  {rv5}");
                    ErrorWriteLine($"    rv5a: {rv5a}");
                    ErrorWriteLine($"    rv5b: {rv5b}");
                    ErrorWriteLine($"    rv6:  {rv6}");
                    ErrorWriteLine($"    rv7:  {rv7}");

                    if(100 > len) ErrorWriteLine($"[{offset} .. {offset + length}]{aeiou}");

                    return false;
                }
            }

            Console.WriteLine();
            Console.WriteLine("------- no correctness2 issues detected. -------");
            return true;
        }
    }
}
