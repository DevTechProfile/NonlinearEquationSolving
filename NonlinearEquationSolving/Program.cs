using Analytics.Nonlinear;
using Mathematics.NL;
using System;

namespace NonlinearEquationSolving
{
    class Program
    {
        static void Main(string[] args)
        {
            SolverTest();
            Console.ReadKey();
        }

        private static void SolverTest()
        {
            Equation[] equations = new Equation[2];
            // Normal forms eq(x) - c = 0
            equations[0] = arg => arg[0] * arg[0] + arg[1] * arg[1] - 1;
            equations[1] = arg => arg[0] * arg[0] - arg[1];
            NonlinearSystem system = new AnalyticalEquationSystem(equations);


            double[] x0;
            SolverOptions options;
            double[] result;
            double[] expected;
            SolutionResult actual;

            // creating nonlinear solver instance - Newton-Raphson solver.
            NonlinearSolver solver = new NewtonRaphsonSolver();

            x0 = new double[] { 0.2, 0.2 }; // initial guess for variable values
            options = new SolverOptions() // options for solving nonlinear problem
            {
                MaxIterationCount = 100,
                SolutionPrecision = 1e-6
            };

            result = null;
            // solving the system
            actual = solver.Solve(system, x0, options, ref result);

            expected = new double[] { 0.7861513778, 0.6180339887 }; // expected values
                                                                    // printing solution result into console out
            PrintNLResult(system, actual, result, expected, options.SolutionPrecision);
        }

        private static void PrintNLResult(NonlinearSystem system, SolutionResult r, double[] result, double[] expected, double prec)
        {
            string s = system.Print() + Environment.NewLine + "Solution";

            s = s + Environment.NewLine + "Converged: " + r.Converged.ToString();

            string s1 = "RESULT:";
            string s2 = "EXPECT:";
            int l = result.Length;

            bool b = true;
            for (int i = 0; i < l; i++)
            {
                s1 = s1 + " " + result[i].ToString();
                s2 = s2 + " " + expected[i].ToString();
                if (Math.Abs(result[i] - expected[i]) > prec)
                {
                    b = false;
                }
            }

            s = s + Environment.NewLine + s1 + Environment.NewLine + s2 + Environment.NewLine;

            if (b)
            {
                s = s + "SUCCESS";
            }
            else
            {
                s = s + "ERROR: " + r.Message;
            }

            Console.Out.WriteLine(Environment.NewLine + s);
        }
    }
}
