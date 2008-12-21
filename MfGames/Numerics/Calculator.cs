using System;
using System.Linq.Expressions;

namespace MfGames.Numerics
{
	/// <summary>
	/// Generics calculator that uses LINQ and compiled functions to do the actual
	/// math.
	///
	/// Ideas come from http://rogeralsing.com/2008/02/27/linq-expressions-calculating-with-generics/
	/// as written by Roger Alsing.
	/// </summary>
	public static class Calculator
	{
		#region Operations
		/// <summary>
		/// Adds to values together and returns the result.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <returns></returns>
		public static T Add<T>(T value1, T value2)
		{
			return Calculator<T>.Add(value1, value2);
		}

		/// <summary>
		/// Divides to values and returns the result.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <returns></returns>
		public static T Divide<T>(T value1, T value2)
		{
			return Calculator<T>.Divide(value1, value2);
		}

		/// <summary>
		/// Multiplies to values together and returns the result.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <returns></returns>
		public static T Multiply<T>(T value1, T value2)
		{
			return Calculator<T>.Multiply(value1, value2);
		}

		/// <summary>
		/// Subtracts to values together and returns the result.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <returns></returns>
		public static T Subtract<T>(T value1, T value2)
		{
			return Calculator<T>.Subtract(value1, value2);
		}
		#endregion
	}

	/// <summary>
	/// Implements a type-specific calculator.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class Calculator<T>
	{
		#region Compiling
		/// <summary>
		/// Compiles the operator by building up a LINQ expression and compiling the results.
		/// </summary>
		/// <param name="operation">The operation.</param>
		/// <returns></returns>
		private static Func<T, T, T> CompileOperator(Func<Expression, Expression, Expression> operation)
		{
			// Create the two input parameters.
			ParameterExpression value1 = Expression.Parameter(typeof(T), "value1");
			ParameterExpression value2 = Expression.Parameter(typeof(T), "value2");

			// Create the expression itself and compile it.
			Expression body = operation(value1, value2);
			LambdaExpression lambda = Expression.Lambda(typeof(Func<T, T, T>), body, value1, value2);
			Func<T, T, T> compiled = (Func<T, T, T>)lambda.Compile();

			// Return the results.
			return compiled;
		}
		#endregion Compiling

		#region Math Operators
		private static readonly Func<T, T, T> add = CompileOperator(Expression.Add);
		private static readonly Func<T, T, T> divide = CompileOperator(Expression.Divide);
		private static readonly Func<T, T, T> multiply = CompileOperator(Expression.Multiply);
		private static readonly Func<T, T, T> subtract = CompileOperator(Expression.Subtract);

		/// <summary>
		/// Contains the function for adding two values together and returning the results.
		/// </summary>
		public static Func<T, T, T> Add
		{
			get
			{
				return add;
			}
		}

		/// <summary>
		/// Contains the function for dividing two values together and returning the results.
		/// </summary>
		public static Func<T, T, T> Divide
		{
			get
			{
				return divide;
			}
		}

		/// <summary>
		/// Contains the function for multipying two values together and returning the results.
		/// </summary>
		public static Func<T, T, T> Multiply
		{
			get
			{
				return multiply;
			}
		}

		/// <summary>
		/// Contains the function for subtractng two values together and returning the results.
		/// </summary>
		public static Func<T, T, T> Subtract
		{
			get
			{
				return subtract;
			}
		}
		#endregion Math Operators
	}
}