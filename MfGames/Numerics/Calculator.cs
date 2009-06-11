#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;
using System.Linq.Expressions;

#endregion

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
		private static Func<T, T, T> CompileOperator(
			Func<Expression, Expression, Expression> operation)
		{
			// Create the two input parameters.
			ParameterExpression value1 = Expression.Parameter(typeof(T), "value1");
			ParameterExpression value2 = Expression.Parameter(typeof(T), "value2");

			// Create the expression itself and compile it.
			Expression body = operation(value1, value2);
			LambdaExpression lambda = Expression.Lambda(typeof(Func<T, T, T>),
			                                            body,
			                                            value1,
			                                            value2);
			var compiled = (Func<T, T, T>) lambda.Compile();

			// Return the results.
			return compiled;
		}

		#endregion Compiling

		#region Math Operators

		private static readonly Func<T, T, T> add = CompileOperator(Expression.Add);

		private static readonly Func<T, T, T> divide =
			CompileOperator(Expression.Divide);

		private static readonly Func<T, T, T> multiply =
			CompileOperator(Expression.Multiply);

		private static readonly Func<T, T, T> subtract =
			CompileOperator(Expression.Subtract);

		/// <summary>
		/// Contains the function for adding two values together and returning the results.
		/// </summary>
		public static Func<T, T, T> Add
		{
			get { return add; }
		}

		/// <summary>
		/// Contains the function for dividing two values together and returning the results.
		/// </summary>
		public static Func<T, T, T> Divide
		{
			get { return divide; }
		}

		/// <summary>
		/// Contains the function for multipying two values together and returning the results.
		/// </summary>
		public static Func<T, T, T> Multiply
		{
			get { return multiply; }
		}

		/// <summary>
		/// Contains the function for subtractng two values together and returning the results.
		/// </summary>
		public static Func<T, T, T> Subtract
		{
			get { return subtract; }
		}

		#endregion Math Operators
	}
}