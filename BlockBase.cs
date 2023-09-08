using IronBlock;
using IronBlock.Blocks;

namespace Project;
public abstract class BlockBase : IBlock
{
    private bool workDone ;


    public abstract Task<object> PerformStuff(Context context);
    public override object Evaluate(Context context)
    {

        workDone = false; // Initial condition
        Console.WriteLine("[Main] Starting worker thread...");
        // Simulate work being done in a separate thread
        Task workTask = Task.Run(async () =>
        {
            await PerformStuff(context);
            workDone = true;
        });


        // Continue executing other tasks while waiting for the condition to become true
        while (!workDone)
        {
            Console.Write("...");
            // Sleep for 200 milliseconds
            Thread.Sleep(200);
        }
        // read a field
        var myField = this.Fields.Get("MY_FIELD");
        // Console.WriteLine(myField);
        // evaluate a value
        var myValue = this.Values.Evaluate("MY_VALUE", context);
        // Console.WriteLine(myValue);
        Console.WriteLine("Done");

        // evaluate a statement
        // var myStatement = this.Statements.Get("MY_STATEMENT");
        // myStatement.Evaluate(context); // evaluate your statement

        // if your block returns a value, simply `return myValue`

        // if your block is part of a statment, and another block runs after it, call
        base.Evaluate(context);
        return null;
    }
}

