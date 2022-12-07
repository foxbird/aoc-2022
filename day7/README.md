# Running
```
foxbird@foxtome:~/aoc-2022/day7$ dotnet run -- sample.txt
- / (dir)
  - a (dir)
    - e (dir)
      - i (file, size=584)
    - f (file, size=29116)
    - g (file, size=2557)
    - h.lst (file, size=62596)
  - b.txt (file, size=14848514)
  - c.dat (file, size=8504156)
  - d (dir)
    - j (file, size=4060174)
    - d.log (file, size=8033020)
    - d.ext (file, size=5626152)
    - k (file, size=7214296)

a matches with size 94853
e matches with size 584
Total Size: 95437

21618835 is available from 70000000, with 8381165 needed
Deleting d will free 24933642 for totaling 46552477
```

# Notes
This builds a tree from the commands. It then processes the tree. I wanted to try out using `records` in C#, and also use the new multi-value return. So, this was a chance to do that. Like the one before, I'm also using the top-level statements as well, just to be straightforward.

Also, even though it wasn't required, I output the directory tree from the sample. It's a good debugging output. An improvement would be to take the sizes from the CLI, but I was lazy and didn't do that.
