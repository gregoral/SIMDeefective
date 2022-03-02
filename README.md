# SIMDeefective
Test CPUs for defective SIMD processing.

## TLDR
Modern processors offer support for SIMD instructions which often allows much faster data processing.
Unfortunately it appears that SIMD instruction processing is sometimes broken.
This test suite aims to stress test SIMD instructions and verifies the results are correct.

Issues are most evident on 4th generation of Intel Core processors.

Each test takes a few hours to complete and executes several million calculations using different algorithm variants.

On affected 4-th gen Intel Core processors errors are usually reproducible in under a minute.

## NOTES:
Not all systems are affected 3 out of 7 tested 4-th gen systems have an issue.
None of the 6-th gen and newer processors appear to have an issue.

## Possible causes:
- OS version ( very likely )
  there is a difference between Win 7 SP1 and Win 10 on the same machine
  issues appear when running Win 7 OS but not when runing under Win 10 OS
- BIOS version ( possibly )
  there are detectable differences between machines with a different BIOS
- an error in test procedure ( unlikely )
  the same procedure returns different results when executed in a loop
- a single defective sample ( unlikely )
  the errors appear on multiple machines
- a memory module issue ( unlikely )
  the errors appear on machines with memory from different vendors

## Algorithm:
- generate a random length string of random characters
- select random offset and length ( this insures we test aligned and unaligned vectors )
- count all occurrences of aeiou characters within a string
- return minimum count of all 5 characters

## Next steps
- test on a larger set of machines
- add new test algorithms and patterns
- create a test suite in C / C++
- test on an operating system other than Winwows ( Linux, MacOS )

## Affected processors
A table of tested processors and encountered issues:
| Gen   | Model               | # Machines tested |# runs | ErrRate | Note                                       |
|-------|:--------------------|------------------:|------:|--------:|:-------------------------------------------|
| 4-th  | Intel Core i7-4790K |                 1 |  >100 | ~ 0.08% | errors easily reproducible                 |
| 4-th  | Intel Core i7-4785T |                 1 |   >20 | ~ 0.08% | errors easily reproducible                 |
| 4-th  | Intel Core i5-4590T |                 4 |    10 |      0% | no issues so far                           |
| 4-th  | Intel Core i3-4130T |                 1 |     2 | ~ 0.11% | errors easily reproducible                 |
| 6-th  | Intel Core i5-6500  |                 1 |   >40 |    > 0% | a single issue detected so far             |
| 6-th  | Intel Core i7-6500U |                 1 |   >20 |      0% | no issues so far                           |
| 7-th  | Intel Core i5-7500T |                 1 |     4 |      0% | no issues so far                           |
| 8-th  | -                   |                 0 |     - |       - | no tests were run                          |
| 9-th  | -                   |                 0 |     - |       - | no tests were run                          |
| 10-th | -                   |                 0 |     - |       - | no tests were run                          |
| 11-th | -                   |                 0 |     - |       - | no tests were run                          |
| 12-th | -                   |                 0 |     - |       - | no tests were run                          |
