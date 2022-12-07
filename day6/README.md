# Day 6 of Advent of of Code 2022
## Run
```text
foxbird@foxtome:~/aoc-2022/day6$ dotnet run -- nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg 4 14
Start of packet after 10
Start of message after 29
```

## Debugging
To get debug logs define `Logging_LogLevel_Default=Debug`.

**NOTE:** The output here is intermixed with the regular console output. You can and perhaps should direct it to a file if needed.

The format is:
* the first number is start of the buffer where we are matching
* the second is the character in the marker we're checking for
* the third is the marker length matched so far
* Then after the equals is the current marker
* And the final value is if it passes or fails the duplicate check.

```text
foxbird@foxtome:~/aoc-2022/day6$ dotnet run -- nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg 4 14
Start of packet after 10
dbug: Program[0]
      0 - 0 - 0 == n == PASS
dbug: Program[0]
      0 - 1 - 1 == nz == PASS
dbug: Program[0]
      0 - 2 - 2 == nzn == FAIL
dbug: Program[0]
      1 - 1 - 0 == z == PASS
dbug: Program[0]
      1 - 2 - 1 == zn == PASS
dbug: Program[0]
      1 - 3 - 2 == znr == PASS
dbug: Program[0]
      1 - 4 - 3 == znrn == FAIL
dbug: Program[0]
      3 - 3 - 0 == r == PASS
dbug: Program[0]
      3 - 4 - 1 == rn == PASS
dbug: Program[0]
      3 - 5 - 2 == rnf == PASS
dbug: Program[0]
      3 - 6 - 3 == rnfr == FAIL
dbug: Program[0]
      4 - 4 - 0 == n == PASS
dbug: Program[0]
      4 - 5 - 1 == nf == PASS
dbug: Program[0]
      4 - 6 - 2 == nfr == PASS
dbug: Program[0]
      4 - 7 - 3 == nfrf == FAIL
Start of message after 29
dbug: Program[0]
      6 - 6 - 0 == r == PASS
dbug: Program[0]
      6 - 7 - 1 == rf == PASS
dbug: Program[0]
      6 - 8 - 2 == rfn == PASS
dbug: Program[0]
      6 - 9 - 3 == rfnt == PASS
dbug: Program[0]
      Marker found after 10
dbug: Program[0]
      0 - 0 - 0 == n == PASS
dbug: Program[0]
      0 - 1 - 1 == nz == PASS
dbug: Program[0]
      0 - 2 - 2 == nzn == FAIL
dbug: Program[0]
      1 - 1 - 0 == z == PASS
dbug: Program[0]
      1 - 2 - 1 == zn == PASS
dbug: Program[0]
      1 - 3 - 2 == znr == PASS
dbug: Program[0]
      1 - 4 - 3 == znrn == FAIL
dbug: Program[0]
      3 - 3 - 0 == r == PASS
dbug: Program[0]
      3 - 4 - 1 == rn == PASS
dbug: Program[0]
      3 - 5 - 2 == rnf == PASS
dbug: Program[0]
      3 - 6 - 3 == rnfr == FAIL
dbug: Program[0]
      4 - 4 - 0 == n == PASS
dbug: Program[0]
      4 - 5 - 1 == nf == PASS
dbug: Program[0]
      4 - 6 - 2 == nfr == PASS
dbug: Program[0]
      4 - 7 - 3 == nfrf == FAIL
dbug: Program[0]
      6 - 6 - 0 == r == PASS
dbug: Program[0]
      6 - 7 - 1 == rf == PASS
dbug: Program[0]
      6 - 8 - 2 == rfn == PASS
dbug: Program[0]
      6 - 9 - 3 == rfnt == PASS
dbug: Program[0]
      6 - 10 - 4 == rfntj == PASS
dbug: Program[0]
      6 - 11 - 5 == rfntjf == FAIL
dbug: Program[0]
      8 - 8 - 0 == n == PASS
dbug: Program[0]
      8 - 9 - 1 == nt == PASS
dbug: Program[0]
      8 - 10 - 2 == ntj == PASS
dbug: Program[0]
      8 - 11 - 3 == ntjf == PASS
dbug: Program[0]
      8 - 12 - 4 == ntjfm == PASS
dbug: Program[0]
      8 - 13 - 5 == ntjfmv == PASS
dbug: Program[0]
      8 - 14 - 6 == ntjfmvf == FAIL
dbug: Program[0]
      12 - 12 - 0 == m == PASS
dbug: Program[0]
      12 - 13 - 1 == mv == PASS
dbug: Program[0]
      12 - 14 - 2 == mvf == PASS
dbug: Program[0]
      12 - 15 - 3 == mvfw == PASS
dbug: Program[0]
      12 - 16 - 4 == mvfwm == FAIL
dbug: Program[0]
      13 - 13 - 0 == v == PASS
dbug: Program[0]
      13 - 14 - 1 == vf == PASS
dbug: Program[0]
      13 - 15 - 2 == vfw == PASS
dbug: Program[0]
      13 - 16 - 3 == vfwm == PASS
dbug: Program[0]
      13 - 17 - 4 == vfwmz == PASS
dbug: Program[0]
      13 - 18 - 5 == vfwmzd == PASS
dbug: Program[0]
      13 - 19 - 6 == vfwmzdf == FAIL
dbug: Program[0]
      15 - 15 - 0 == w == PASS
dbug: Program[0]
      15 - 16 - 1 == wm == PASS
dbug: Program[0]
      15 - 17 - 2 == wmz == PASS
dbug: Program[0]
      15 - 18 - 3 == wmzd == PASS
dbug: Program[0]
      15 - 19 - 4 == wmzdf == PASS
dbug: Program[0]
      15 - 20 - 5 == wmzdfj == PASS
dbug: Program[0]
      15 - 21 - 6 == wmzdfjl == PASS
dbug: Program[0]
      15 - 22 - 7 == wmzdfjlv == PASS
dbug: Program[0]
      15 - 23 - 8 == wmzdfjlvt == PASS
dbug: Program[0]
      15 - 24 - 9 == wmzdfjlvtq == PASS
dbug: Program[0]
      15 - 25 - 10 == wmzdfjlvtqn == PASS
dbug: Program[0]
      15 - 26 - 11 == wmzdfjlvtqnb == PASS
dbug: Program[0]
      15 - 27 - 12 == wmzdfjlvtqnbh == PASS
dbug: Program[0]
      15 - 28 - 13 == wmzdfjlvtqnbhc == PASS
dbug: Program[0]
      Marker found after 29
```
