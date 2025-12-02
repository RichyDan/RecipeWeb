namespace RecipeWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            var bar = 0;
            /////
            
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
