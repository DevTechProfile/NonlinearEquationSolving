using Mathematics.NL;
using System.Linq;

namespace Analytics.Nonlinear
{
    /// <summary>
    /// Nonlinear System of Analytical Equations given by lambda expressions.
    /// </summary>
    public class AnalyticalEquationSystem : EquationSystem
    {
        private const double h = 1E-12;

        public AnalyticalEquationSystem(Equation[] equationSystem) :
            base(equationSystem)
        {
            equations = equationSystem;
            CreateDerivations();
        }

        private void CreateDerivations()
        {
            // Dimension is number of equations
            derivatives = new Derivative[Dimension];

            for (int i = 0; i < Dimension; i++)
            {
                int index = i;

                derivatives[i] = (int j, double[] x) =>
                {
                    // x-h
                    var leftsided = x.ToArray();
                    leftsided[j] -= h;
                    // x+h
                    var rightsided = x.ToArray();
                    rightsided[j] += h;
                    // symmetric difference quotient
                    return (equations[index](rightsided) - equations[index](leftsided)) / (2 * h);
                };
            }
        }

        /// <summary>
        /// Analytical equation system type description.
        /// </summary>
        /// <returns></returns>
        protected override string GetSystemType()
        {
            return "Analytical equations system defined with lambda expressions";
        }
    }
}
