using System.Net;
using System.Text;
using System.Web;


namespace SimpleWebServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            StartServer();
        }

        public static void StartServer()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5000/");
            listener.Start();
            Console.WriteLine("Listening on http://localhost:5000/");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                if (request.HttpMethod == "GET")
                {
                    ServeStaticFile(response, "D:\\paracforstu\\Three-Tier_Architechture\\text.html");
                }
                else if (request.HttpMethod == "POST")
                {
                    if (request.Url.AbsolutePath == "/binary")
                    {
                        HandleBinaryOperation(request, response);
                    }
                    else if (request.Url.AbsolutePath == "/sphere")
                    {
                        HandleSphereCalculation(request, response);
                    }
                }
            }
        }

        private static void ServeStaticFile(HttpListenerResponse response, string filePath)
        {
            if (File.Exists(filePath))
            {
                string responseString = File.ReadAllText(filePath);
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                response.StatusCode = 404;
                response.Close();
            }
        }

        private static void HandleBinaryOperation(HttpListenerRequest request, HttpListenerResponse response)
        {
            var logic = DependencyInjector.GetBusinessLogicLayer();

            string data;
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                data = reader.ReadToEnd();
            }

            var parsedData = HttpUtility.ParseQueryString(data);
            string name = parsedData["name"];
            int number1 = int.Parse(parsedData["number1"]);
            int number2 = int.Parse(parsedData["number2"]);
            string operation = parsedData["operation"];

            int result = logic.BinaryOpp(operation, number1, number2);

            logic.StoreOperation(name, operation);

            string responseString = $@"
            <html>
            <body>
                <h1>Binary Operation Result</h1>
                <p>Name: {name}</p>
                <p>Operation: {operation}</p>
                <p>Result: {result}</p>
                <a href='/'>Back to Home</a>
            </body>
            </html>";

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            response.ContentType = "text/html";
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        private static void HandleSphereCalculation(HttpListenerRequest request, HttpListenerResponse response)
        {
            var logic = DependencyInjector.GetBusinessLogicLayer();

            string data;
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                data = reader.ReadToEnd();
            }

            var parsedData = HttpUtility.ParseQueryString(data);
            int radius = int.Parse(parsedData["radius"]);

            logic.GeometryCal(radius, out float area, out float volume);

            string responseString = $@"
            <html>
            <body>
                <h1>Sphere Calculation Result</h1>
                <p>Area: {area}</p>
                <p>Volume: {volume}</p>
                <a href='/'>Back to Home</a>
            </body>
            </html>";

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            response.ContentType = "text/html";
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}

//namespace SimpleWebServer
//{
//    class Program
//    {
//        public static void Main(string[] args)
//        {
//            StartServer();
//        }

//        public static void StartServer()
//        {
//            HttpListener listener = new HttpListener();
//            listener.Prefixes.Add("http://localhost:5000/");
//            listener.Start();
//            Console.WriteLine("Listening on http://localhost:5000/");

//            while (true)
//            {
//                HttpListenerContext context = listener.GetContext();
//                HttpListenerRequest request = context.Request;
//                HttpListenerResponse response = context.Response;

//                if (request.HttpMethod == "GET")
//                {
//                    ServeStaticFile(response, "D:\\paracforstu\\Three-Tier_Architechture\\text.html");
//                }
//                else if (request.HttpMethod == "POST")
//                {
//                    if (request.Url.AbsolutePath == "/binary")
//                    {
//                        HandleBinaryOperation(request, response);
//                    }
//                    else if (request.Url.AbsolutePath == "/sphere")
//                    {
//                        HandleSphereCalculation(request, response);
//                    }
//                }
//            }
//        }

//        private static void ServeStaticFile(HttpListenerResponse response, string filePath)
//        {
//            if (File.Exists(filePath))
//            {
//                string responseString = File.ReadAllText(filePath);
//                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
//                response.ContentLength64 = buffer.Length;
//                response.ContentType = "text/html";
//                Stream output = response.OutputStream;
//                output.Write(buffer, 0, buffer.Length);
//                output.Close();
//            }
//            else
//            {
//                response.StatusCode = 404;
//                response.Close();
//            }
//        }

//        private static void HandleBinaryOperation(HttpListenerRequest request, HttpListenerResponse response)
//        {
//            var logic = new BusinessLogicLayer();

//            string data;
//            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
//            {
//                data = reader.ReadToEnd();
//            }

//            var parsedData = HttpUtility.ParseQueryString(data);
//            string name = parsedData["name"];
//            int number1 = int.Parse(parsedData["number1"]);
//            int number2 = int.Parse(parsedData["number2"]);
//            string operation = parsedData["operation"];

//            int result = logic.BinaryOpp(operation, number1, number2);

//            logic.StoreOperation(name, operation);

//            string responseString = $@"
//            <html>
//            <body>
//                <h1>Binary Operation Result</h1>
//                <p>Name: {name}</p>
//                <p>Operation: {operation}</p>
//                <p>Result: {result}</p>
//                <a href='/'>Back to Home</a>
//            </body>
//            </html>";

//            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
//            response.ContentLength64 = buffer.Length;
//            response.ContentType = "text/html";
//            Stream output = response.OutputStream;
//            output.Write(buffer, 0, buffer.Length);
//            output.Close();
//        }

//        private static void HandleSphereCalculation(HttpListenerRequest request, HttpListenerResponse response)
//        {
//            var logic = new BusinessLogicLayer();

//            string data;
//            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
//            {
//                data = reader.ReadToEnd();
//            }

//            var parsedData = HttpUtility.ParseQueryString(data);
//            int radius = int.Parse(parsedData["radius"]);

//            logic.GeometryCal(radius, out float area, out float volume);

//            string responseString = $@"
//            <html>
//            <body>
//                <h1>Sphere Calculation Result</h1>
//                <p>Area: {area}</p>
//                <p>Volume: {volume}</p>
//                <a href='/'>Back to Home</a>
//            </body>
//            </html>";

//            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
//            response.ContentLength64 = buffer.Length;
//            response.ContentType = "text/html";
//            Stream output = response.OutputStream;
//            output.Write(buffer, 0, buffer.Length);
//            output.Close();
//        }
//    }
//}


//namespace SimpleWebServer
//{
//    class Program
//    {
//        public static void Main(string[] args)
//        {
//            StartServer();
//        }

//        public static void StartServer()
//        {
//            HttpListener listener = new HttpListener();
//            listener.Prefixes.Add("http://localhost:5000/");
//            listener.Start();
//            Console.WriteLine("Listening on http://localhost:5000/");

//            while (true)
//            {
//                HttpListenerContext context = listener.GetContext();
//                HttpListenerRequest request = context.Request;
//                HttpListenerResponse response = context.Response;

//                if (request.HttpMethod == "GET")
//                {
//                    ServeStaticFile(response, "D:\\paracforstu\\Three-Tier_Architechture\\text.html"); 
//                }
//                else if (request.HttpMethod == "POST")
//                {
//                    if (request.Url.AbsolutePath == "/binary")
//                    {
//                        HandleBinaryOperation(request, response);
//                    }
//                    else if (request.Url.AbsolutePath == "/sphere")
//                    {
//                        HandleSphereCalculation(request, response);
//                    }
//                }
//            }
//        }

//        private static void ServeStaticFile(HttpListenerResponse response, string filePath)
//        {
//            if (File.Exists(filePath))
//            {
//                string responseString = File.ReadAllText(filePath);
//                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
//                response.ContentLength64 = buffer.Length;
//                response.ContentType = "text/html";
//                Stream output = response.OutputStream;
//                output.Write(buffer, 0, buffer.Length);
//                output.Close();
//            }
//            else
//            {
//                response.StatusCode = 404;
//                response.Close();
//            }
//        }

//        private static void HandleBinaryOperation(HttpListenerRequest request, HttpListenerResponse response)
//        {
//            var logic = new BusinessLogicLayer();

//            string data;
//            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
//            {
//                data = reader.ReadToEnd();
//            }

//            var parsedData = System.Web.HttpUtility.ParseQueryString(data);
//            int number1 = int.Parse(parsedData["number1"]);
//            int number2 = int.Parse(parsedData["number2"]);
//            string operation = parsedData["operation"];

//            int result = logic.BinaryOpp(operation, number1, number2);

//            string responseString = $@"
//            <html>
//            <body>
//                <h1>Binary Operation Result</h1>
//                <p>Result: {result}</p>
//                <a href='/'>Back to Home</a>
//            </body>
//            </html>";

//            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
//            response.ContentLength64 = buffer.Length;
//            response.ContentType = "text/html";
//            Stream output = response.OutputStream;
//            output.Write(buffer, 0, buffer.Length);
//            output.Close();
//        }

//        private static void HandleSphereCalculation(HttpListenerRequest request, HttpListenerResponse response)
//        {
//            var logic = new BusinessLogicLayer();

//            string data;
//            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
//            {
//                data = reader.ReadToEnd();
//            }

//            var parsedData = System.Web.HttpUtility.ParseQueryString(data);
//            int radius = int.Parse(parsedData["radius"]);

//            logic.GeometryCal(radius, out float area, out float volume);

//            string responseString = $@"
//            <html>
//            <body>
//                <h1>Sphere Calculation Result</h1>
//                <p>Area: {area}</p>
//                <p>Volume: {volume}</p>
//                <a href='/'>Back to Home</a>
//            </body>
//            </html>";

//            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
//            response.ContentLength64 = buffer.Length;
//            response.ContentType = "text/html";
//            Stream output = response.OutputStream;
//            output.Write(buffer, 0, buffer.Length);
//            output.Close();
//        }
//    }
//}

//public class FrontEnd
//{
//    public static void Main(string[] args)
//    {
//        BusinessLogicLayer logic = new BusinessLogicLayer();
//        Console.WriteLine("if you want to do binary operations like addition press '1'. if you want to calculate area or volume of sphere press'0'.");
//        int flag = int.Parse(Console.ReadLine());
//        if (flag == 1)
//        {
//            Console.WriteLine("enter two numbers you want to operate on");
//            int number1 = int.Parse(Console.ReadLine());
//            int number2 = int.Parse(Console.ReadLine());
//            Console.WriteLine("enter the type of operation you want which can be 'Addition','Subtraction', 'Multiplication', 'Division'.");
//            string operation = Console.ReadLine();
//            logic.BinaryOpp(operation, number1, number2);
//        }
//        else if (flag == 0)
//        {
//            Console.WriteLine("enter the shape you want area or volume for.");
//            string shape = Console.ReadLine();
//            if (shape == "Sphere")
//            {
//                Console.WriteLine("Enter Radius of Sphere.");
//                int num1 = int.Parse(Console.ReadLine());
//                logic.GeometryCal(shape, num1, out float area, out float volume);
//                Console.WriteLine($"Area of sphere is {area}. Volume of Sphere is{volume}.");

//            }
//        }
//    }
//}
