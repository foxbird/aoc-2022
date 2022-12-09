# Running
```
foxbird@foxtome:~/aoc-2022/day8$ dotnet run -- sample.txt
30373
25512
65332
33549
35390

There are 21 trees visible from the permiter

The best scenic score is 8 at 3,2
```

# Notes
It's horribly inefficient. Calculating the tree visibility from outside checks each tree individually and adds them up. A smarter way to do it is to look across the whole row or column, and keep increasing the max. This instead checks every tree along the way multiple times. It was written in this fashion since I anticipated needing to calculate certain paths or visibilities from other trees. This wasn't tree. You could remove IsTreeVisible entirely, and replace PerimeterLook with a series of calls to CountVisibleTrees, specifying a negative start/end. I didn't, since that part of the challenge was over.
