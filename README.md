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
- Intel 4-th gen
  Intel Core i7-4790K          tested on 1 machine          easily reproducible
  Intel Core i7-4785T          tested on 1 machine          easily reproducible

- Intel 5-th gen
  no tests were run

- Intel 6-th gen
  Intel Core i5-6500           tested on 1 machine          only a single issue was detected
  Intel Core i7-6500U          tested on 1 machine          so far no issues

- Intel 7-th gen
  no tests were run

- Intel 8-th gen
  no tests were run

- Intel 9-th gen
  no tests were run

- Intel 10-th gen
  no tests were run

- Intel 11-th gen
  no tests were run

- Intel 12-th gen
  no tests were run
