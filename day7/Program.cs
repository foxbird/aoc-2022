using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

if (args.Length != 1)
{
    Console.WriteLine("Usage: day7 <file>");
    return;
}

// Setup
Directory root = new() { Name = "/" };
Node current = root;
var contents = File.ReadAllText(args[0]);
var lines = contents.ReplaceLineEndings("\n").Split("\n");

// Handle the lines
foreach (var line in lines)
{
    if (IsCommand(line))
    {
        var (command, arg) = GetCommand(line);
        if (command == "cd" && arg == ".." )
        {
            if (current.Parent != null)
                current = current.Parent;
        }
        else if (command == "cd" && arg == "/")
        {
            current = root;
        }
        else if (command == "cd")
        {
            current = current.Dive(arg);
        }
    }
    else
    {
        Node child = null!;
        if (line.StartsWith("dir"))
        {
            child = CreateDirectory(line);
        } 
        else 
        {
            child = CreateFile(line);
        }
        child.Parent = current;
        current.Children.Add(child);

    }
}

Console.WriteLine(root.ToString(""));
var matches = new List<Node>();
FindMatchingDirectories(100000, true, matches, root);
foreach (Node n in matches)
{
    Console.WriteLine($"{n.Name} matches with size {n.GetSize()}");
}

Console.WriteLine($"Total Size: {matches.Select(x => x.GetSize()).Sum()}");
Console.WriteLine();
// Part 2
int diskSize = 70000000;
int updateSize = 30000000;
int availableSize = diskSize - root.GetSize();
int neededSize = updateSize - availableSize;

Console.WriteLine($"{availableSize} is available from {diskSize}, with {neededSize} needed");
matches = new List<Node>();
FindMatchingDirectories(neededSize, false, matches, root);
var candidate = matches.OrderBy(x => x.GetSize()).First();

Console.WriteLine($"Deleting {candidate.Name} will free {candidate.GetSize()} for totaling {candidate.GetSize() + availableSize}");

// Supporting Functions

void FindMatchingDirectories(int maxSize, bool smaller, List<Node> matches, Node current)
{
    if (smaller && current.GetSize() <= maxSize)
        matches.Add(current);
    else if (!smaller && current.GetSize() >= maxSize)
        matches.Add(current);

    foreach (Node n in current.Children.OfType<Directory>())
        FindMatchingDirectories(maxSize, smaller, matches, n);
}

Directory CreateDirectory(string line)
{
    var regex = new Regex(@"dir (\w*)");
    var match = regex.Match(line);
    return new Directory() { Name = match.Groups[1].Value };
}

FileNode CreateFile(string line)
{
    var regex = new Regex(@"(\d+) (.+)");
    var match = regex.Match(line);
    return new FileNode() { Name = match.Groups[2].Value, Size = int.Parse(match.Groups[1].Value)};
}

(string, string) GetCommand(string line)
{
    var regex = new Regex(@"\$\s+(cd|ls)\s*(\S*)");
    var result = regex.Match(line);
    return (result.Groups[1].Value, result.Groups[2].Value);
}

bool IsCommand(string line)
{
    return line.StartsWith("$");
}

abstract record Node {
    public string Name { get; set; } = default!;
    public int Size { get; set; } = 0;
    public abstract int GetSize();
    public List<Node> Children = new();
    public Node Parent = default!;
    public Node Dive(string name)
    {
        var match = Children.Where(c => c.Name == name).FirstOrDefault();
        if (match != null)
            return match;

        var child = new Directory() { Name = name, Parent = this };
        Children.Add(child);
        return child;
    }

    public abstract string ToString(string spacing = "");
}

record Directory : Node {
    
    public override int GetSize()
    {
        int total = 0;
        foreach (Node child in Children)
        {
            total += child.GetSize();
        }

        return total;
    }

    public override string ToString(string spacing = "")
    {
        var value = $"{spacing}- {Name} (dir)\n";
        foreach (var child in Children)
        {
            value += child.ToString(spacing + "  ");
        }
        return value;
    }
}

record FileNode : Node {
    public override int GetSize()
    {
        return Size;
    }

    public override string ToString(string spacing = "")
    {
        return $"{spacing}- {Name} (file, size={Size})\n";
    }
}
