using IronBlock;
using IronBlock.Blocks;



// create a parser
var parser = new Parser();


// add the standard blocks to the parser
parser.AddStandardBlocks();
parser.AddBlock<MyCustomBlock>("my_custom_block");

// read xml from a file
var xml = File.ReadAllText("my_file.xml");



// parse the xml file to create a workspace
var workspace = parser.Parse(xml);

// run the workspace
var output = workspace.Evaluate();

// "Hello World"

Console.WriteLine(output);


public class MyCustomBlock : IBlock
{
    private bool workDone ;
    public async Task<string> FetchGoogleHomePageAsync()
    {
        Console.WriteLine("FetchGoogleHomePageAsync");
        try
        {
            using (var httpClient = new HttpClient())
            {
                // Send an HTTP GET request to google.com
                HttpResponseMessage response = await httpClient.GetAsync("http://www.google.com");
                Console.WriteLine("FetchGoogleHomePageAsync2");
                // Check if the response is successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as text
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("FetchGoogleHomePageAsync3");
                    return content;
                }
                else
                {
                    // Handle the case where the request was not successful
                    Console.WriteLine($"HTTP request failed with status code {response.StatusCode}");
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the request
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
    public void doStuff() {
        // do stuff
        Console.WriteLine("do stuff");
    }

    public async Task<object> PerformWork() {
        Console.WriteLine("[WorkerThread] Performing work ");
        await Task.Delay(3000);
        return null;
    }
    public override object Evaluate(Context context)
    {

        workDone = false; // Initial condition
        Console.WriteLine("[Main] Starting worker thread...");
        // Simulate work being done in a separate thread
        Task workTask = Task.Run(async () =>
        {
            await PerformWork();
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