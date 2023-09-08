using IronBlock;
using IronBlock.Blocks;
namespace Project;

public class Program
{
    public static int Main(string[] args)
    {
        // create a parser
        var parser = new Parser();


        // add the standard blocks to the parser
        parser.AddStandardBlocks();
        parser.AddBlock<MyCustomBlock1>("my_custom_block");

        // read xml from a file
        var xml = File.ReadAllText("my_file.xml");



        // parse the xml file to create a workspace
        var workspace = parser.Parse(xml);

        // run the workspace
        var output = workspace.Evaluate();

        // "Hello World"

        Console.WriteLine(output);
        return 0;
    }
}


public class MyCustomBlock1 : BlockBase
{
    public override async Task<object> PerformStuff(Context context)
    {   
        //print all context.Variables
        context.Variables.ToList().ForEach(x => Console.WriteLine(x.Key + " " + x.Value));
        
        Console.WriteLine("[WorkerThread] Performing work ");
        await Task.Delay(3000);
        return null;
    }
}