using System.ComponentModel;
using System.Reflection;

namespace Helper
{
    public class Debug
    {
        public static void GetPropertyValues(Object obj)
        {
            Type t = obj.GetType();
            Console.Write($"Type: {t.Name}; ");

            FieldInfo[] fields = t.GetFields();
            Console.Write($"{fields.Length} fields; ");

            PropertyInfo[] props = t.GetProperties();
            Console.Write($"{props.Length} properties; ");

            MethodInfo[] methods = t.GetMethods();
            Console.WriteLine($"{methods.Length} methods;");

            foreach (FieldInfo field in fields)
            {
                Console.Write($"{field.FieldType.Name.ToLower()} {field.Name} = {field.GetValue(obj)}; ");
            }
            Console.WriteLine();

            foreach (PropertyInfo prop in props)
            {
                if (prop.GetIndexParameters().Length == 0)
                {
                    Console.Write($"{prop.PropertyType.Name.ToLower()} {prop.Name} = {prop.GetValue(obj)}; ");
                }
                else
                {
                    Console.Write($"{prop.PropertyType.Name.ToLower()} {prop.Name} = <Indexed>; ");
                }
            }
            Console.WriteLine();

            foreach (MethodInfo method in methods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                string parameterString = "";
                foreach (ParameterInfo param in parameters)
                {
                    if (parameterString != "")
                    {
                        parameterString += ", ";
                    }
                    parameterString += $"{param.ParameterType.Name.ToLower()} {param.Name}";
                }
                Console.Write($"{method.ReturnType.Name.ToLower()} {method.Name}({parameterString}); ");
            }
            Console.WriteLine();
        }
    }
    public class ConditionWatcher
    {
        public ConditionWatcher(Action action, Func<bool> condition, int timeout = 100, bool once = true)
        {
            this.action = action;
            this.condition = condition;
            this.timeout = timeout;
            this.once = once;
            Thread thread = new Thread(Loop);
            thread.Start();
        }

        private Action action;
        private Func<bool> condition;
        private int timeout;
        private bool once;

        public void Loop()
        {
            while (true)
            {
                if (condition())
                {
                    action();
                    if (once)
                    {
                        return;
                    }
                }
                Thread.Sleep(timeout);
            }
        }
    }
    public class Input
    {
        public static int GetInt (string startMessage = "", string errorMessage = "Not an integer, try again:")
        {
            while (true)
            {
                if (startMessage != "")
                {
                    Console.Write(startMessage);
                }
                string? stringInput = Console.ReadLine();
                int intInput;
                if (int.TryParse(stringInput, out intInput))
                {
                    return intInput;
                } else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        public static double GetDouble(string startMessage = "", string errorMessage = "Not a double, try again:")
        {
            while (true)
            {
                if (startMessage != "")
                {
                    Console.Write(startMessage);
                }
                string? stringInput = Console.ReadLine();
                double doubleInput;
                if (double.TryParse(stringInput, out doubleInput))
                {
                    return doubleInput;
                }
                else
                {
                    if (errorMessage != "")
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
            }
        }

    }
}