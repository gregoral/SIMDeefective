# SIMDeefective
Test a compter system for defective SIMD processing.

## TLDR
It appears the issue is tied to Windows 7.  
Errors are still present with the last available security update pack for Win 7 applied.  
I did get 1 error on Windows 10, but I'm not able to reproduce.  

Modern processors offer support for SIMD instruction processing which often allows much faster data processing.  
Unfortunately it appears that SIMD instruction processing is sometimes broken.  
This test suite aims to stress test SIMD instructions and verifies the results are correct.  

Issues are most evident on Windows 7 operating system, which is out of regular support but still in use.  

Each test takes a few hours to complete and executes several million calculations using different algorithm variants.  

On affected systems errors are usually reproducible in under a minute.  

## NOTES:
Not all systems are affected 3 out of 11 tested systems have an issue.

## Possible causes:
- **OS version** ( most likely )  
  there is a difference between Win 7 SP1 and Win 10 on the same machine  
  issues appear when running Win 7 OS but not when runing under Win 10 OS  
  the issue is still present in the last update that can be installed without having an ESU license ( JAN 2020 )
  I'm unable to test with newer update files.

- **BIOS version** ( possibly )  
  there are differences between machines with a different BIOS vesion  

- **an error in test procedure** ( unlikely )  
  the same procedure returns different results when executed in a loop  

- **a single defective sample** ( unlikely )  
  errors appear on multiple machines  

- **a memory module issue** ( unlikely )
  errors appear on machines with memory from different vendors

## Algorithm:
- generate a random length string of random characters
- select random offset and length ( this insures we test aligned and unaligned vectors )
- count all occurrences of AEIOU characters within a string
- return minimum count of all 5 characters

## Next steps
- test on a larger set of machines
- add new test algorithms and patterns
- create a test suite in C / C++
- test on an operating system other than Winwows ( Linux, MacOS )

## Affected processors
A table of tested processors and encountered issues:
| Gen   | Model               | # Machines |OS       |# runs | ErrRate | Note                                       |
|-------|:--------------------|------------|:--------|------:|--------:|--------------------------------------------|
| 4-th  | Intel Core i7-4790K |          1 | Win 7   |  >100 | ~ 0.08% | errors easily reproducible                 |
| 4-th  | Intel Core i7-4785T |          1 | Win 7   |   >20 | ~ 0.08% | errors easily reproducible                 |
| 4-th  | Intel Core i5-4590T |          1 | Win 7   |     2 | ~ 0.09% | issues under Win 7                         |
| 4-th  | Intel Core i3-4130T |          1 | Win 7   |     2 | ~ 0.11% | errors easily reproducible                 |
| 4-th  | Intel Core i5-4590T |          4 | Win 10  |    10 |      0% | no issues under Win 10                     |
| 6-th  | Intel Core i5-6500  |          1 | Win 10  |   >40 |    > 0% | a single issue detected                    |
| 6-th  | Intel Core i7-6500U |          1 | Win 10  |   >20 |      0% | no issues so far                           |
| 7-th  | Intel Core i5-7500T |          1 | Win 10  |     4 |      0% | no issues so far                           |
| 8-th  | -                   |          0 | Win 10  |     - |       - | no tests were run                          |
| 9-th  | -                   |          0 | Win 10  |     - |       - | no tests were run                          |
| 10-th | -                   |          0 | Win 10  |     - |       - | no tests were run                          |
| 11-th | -                   |          0 | Win 10  |     - |       - | no tests were run                          |
| 12-th | -                   |          0 | Win 10  |     - |       - | no tests were run                          |
