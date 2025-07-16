using System.Text;

namespace asp_homework_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseStaticFiles();
            
            app.Run(async (context) =>
            {
                var response = context.Response;
                var request = context.Request;
                response.ContentType = "text/html; charset=utf-8";

                var body = new StringBuilder();

                if (request.Path == "/")
                {
                    body.AppendLine("<div class=\"px-4 py-5 my-5 text-center\"> <img class=\"d-block mx-auto mb-4\" src=\"/docs/5.3/assets/brand/bootstrap-logo.svg\" alt=\"\" width=\"72\" height=\"57\"> <h1 class=\"display-5 fw-bold text-body-emphasis\">Main page</h1> <div class=\"col-lg-6 mx-auto\"> <p class=\"lead mb-4\">There's API available.</p> <div class=\"d-grid gap-2 d-sm-flex justify-content-sm-center\"> <button type=\"button\" class=\"btn btn-primary btn-lg px-4 gap-3\">Primary button</button> <button type=\"button\" class=\"btn btn-outline-secondary btn-lg px-4\">Secondary</button> </div> </div> </div>");
                }
                else if (request.Path == "/api/numbers")
                {
                    string query = request.QueryString.ToString();

                    string numberOne = request.Query["numberOne"];
                    string numberTwo = request.Query["numberTwo"];

                    if (numberOne == null && numberTwo == null)
                    {
                        numberOne = "0";
                        numberTwo = "0";
                    }

                    long.TryParse(numberOne.Trim(), out var numberOneLong);
                    long.TryParse(numberTwo.Trim(), out var numberTwoLong);

                    body.AppendLine($"""
                        <div class="px-4 py-5 my-5 text-center">
                        <div class="col-lg-6 mx-auto">
                        <p class=\"lead mb-4\">{numberOneLong + numberTwoLong}</p>
                        </div>
                        </div>
                        """);
                }
                else if (request.Path == "/api/string-lenght")
                {
                    string query = request.QueryString.ToString();

                    string inputString = request.Query["inputString"];

                    body.AppendLine($"""
                        <div class="px-4 py-5 my-5 text-center">
                        <div class="col-lg-6 mx-auto">
                        <p class=\"lead mb-4\">{inputString.Length}</p>
                        </div>
                        </div>
                        """);
                }

                response.StatusCode = 200;
                await context.Response.WriteAsync($"""
                    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.7/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-LN+7fdVzj6u52u30Kp6M/trliBMCMKTyK833zpbD+pXdCLuTusPj697FH4R/5mcr" crossorigin="anonymous">

                    <header class="d-flex justify-content-center py-3"> <ul class="nav nav-pills"> <li class="nav-item"><a href="#" class="nav-link active" aria-current="page">Home</a></li> <li class="nav-item"><a href="#" class="nav-link">Features</a></li> <li class="nav-item"><a href="#" class="nav-link">Team</a></li> <li class="nav-item"><a href="#" class="nav-link">API</a></li> <li class="nav-item"><a href="#" class="nav-link">About</a></li> </ul> </header>

                    {body.ToString()}
                    """);
            });

            // app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
