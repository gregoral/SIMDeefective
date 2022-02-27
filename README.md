# SIMDeefective
Test CPUs for defective SIMD processing.

## TLDR
Modern processors offer support for SIMD instructions which often allow much faster data processing.
Unforutately SIMD processing is sometimes broken.
This test suite aims to stress test SIMD instruction and verifies the results are correct.

Issues are most evident on 4th generation of Intel Core processors.

## Next steps
- test on more machines
- add new test patterns

## Affected processors
So far issues were encountered with the following processors:
| Gen   | Model               | # Machines tested |# runs | ErrRate | Note                                       |
|-------|:--------------------|------------------:|------:|--------:|:-------------------------------------------|
| 6-th  | -                   |                 0 |     - |       - | no tests were run                          |
| 4-th  | Intel Core i7-4790K |                 1 |  >100 | ~ 0.08% | error easily reproducible                  |
| 4-th  | Intel Core i7-4785T |                 1 |   >20 | ~ 0.08% | error easily reproducible                  |
| 5-th  | -                   |                 0 |     - |       - | no tests were run                          |
| 6-th  | Intel Core i5-6500  |                 1 |   >40 |    > 0% | a single issue detected so far             |
| 6-th  | Intel Core i7-6500U |                 1 |   >20 |      0% | no issues so far                           |
| 6-th  | -                   |                 0 |     - |       - | no tests were run                          |
| 7-th  | -                   |                 0 |     - |       - | no tests were run                          |
| 8-th  | -                   |                 0 |     - |       - | no tests were run                          |
| 9-th  | -                   |                 0 |     - |       - | no tests were run                          |
| 10-th | -                   |                 0 |     - |       - | no tests were run                          |
| 11-th | -                   |                 0 |     - |       - | no tests were run                          |
| 12-th | -                   |                 0 |     - |       - | no tests were run                          |
