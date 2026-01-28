using System;

class Calculator
{
    public double Add(double a, double b)
    {
        return a + b;
    }

    public double Subtract(double a, double b)
    {
        return a - b;
    }
    public double Multiply(double a, double b)
    {
        return a * b;
    }

    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            Console.WriteLine("Cannot divide by zero.");
            return 0;
        }
        return a / b;
    }
}
class Program
{
    static void Main(string[] args)
    {
        Calculator calc = new Calculator(); // Object creation

        Console.Write("Enter first number: ");
        double num1 = Convert.ToDouble(Console.ReadLine()); //Type Casting

        Console.Write("Enter second number: ");
        double num2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Choose operation:");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Subtract");
        Console.WriteLine("3. Multiply");
        Console.WriteLine("4. Divide");

        int choice = Convert.ToInt32(Console.ReadLine());

        double result = 0;

        switch (choice)  //conditional statement
        {
            case 1:
                result = calc.Add(num1, num2);
                break;
            case 2:
                result = calc.Subtract(num1, num2);
                break;
            case 3:
                result = calc.Multiply(num1, num2);
                break;
            case 4:
                result = calc.Divide(num1, num2);
                break;
            default:
                Console.WriteLine("Invalid choice");
                return;
        }

        Console.WriteLine("Result: " + result);
    }
}