
using System;

namespace MfGames.Utility.Dice {



internal class Parser {
	const int _EOF = 0;
	const int _DICE = 1;
	const int _NUMBER = 2;
	const int _PLUS = 3;
	const int _MINUS = 4;
	const int maxT = 5;

	const bool T = true;
	const bool x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	private Errors  errors;

	private IDice dice = null;

	public Token token; // last recognized token
	public Token la;    // lookahead token
	int errDist = minErrDist;



	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
		errors.Format = scanner.Format;
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist)
			errors.Error(token.line, token.col, msg);
		errDist = 0;
	}

   	public Errors Errors { get { return errors; } }
	
	void Get () {
		for (;;) {
			token = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = token;
		}
	}
	
	protected void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	protected bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void Dice() {
		Addition(out dice);
	}

	void Addition(out IDice dice) {
		IDice d1, d2 = null; 
		Subtraction(out d1);
		if (la.kind == 3) {
			Get();
			Addition(out d2);
		}
		if (d2 == null)
		dice = d1;
		else
			dice = new AdditionDice(d1, d2);
		
	}

	void Subtraction(out IDice dice) {
		IDice d1, d2 = null; 
		Expression(out d1);
		if (la.kind == 4) {
			Get();
			Subtraction(out d2);
		}
		if (d2 == null)
		dice = d1;
		else
			dice = new SubtractionDice(d1, d2);
		
	}

	void Expression(out IDice dice) {
		int number = 0; int sides = 0; 
		Integer(out number);
		if (la.kind == 1) {
			Get();
			Integer(out sides);
		}
		if (sides == 0)
		dice = new ConstantDice(number);
		else
			dice = new RandomDice(number, sides);
		
	}

	void Integer(out int value) {
		Expect(2);
		value = Int32.Parse(token.val); 
	}



	public IDice Parse() {
		la = new Token();
		la.val = "";		
		Get();
		Dice();

    Expect(0);
		return dice;
	}
	
	bool[,] set = {
		{T,x,x,x, x,x,x}

	};
} // end Parser


public class Errors : Logable {
	public int Count = 0;                                    // number of errors detected
	public string Format = null;
  public string errMsgFormat = "{3} ({0},{1}): {2}"; // 0=line, 1=column, 2=text
	
	public void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "DICE expected"; break;
			case 2: s = "NUMBER expected"; break;
			case 3: s = "PLUS expected"; break;
			case 4: s = "MINUS expected"; break;
			case 5: s = "??? expected"; break;

			default: s = "error " + n; break;
		}
		Alert(errMsgFormat, line, col, s, Format);
		Count++;
	}

	public void SemErr (int line, int col, int n) {
		Alert(errMsgFormat, line, col, ("error " + n), Format);
		Count++;
	}

	public void Error (int line, int col, string s) {
		Alert(errMsgFormat, line, col, s, Format);
		Count++;
	}

	public void Exception (string s) {
	/*
		Console.WriteLine(s); 
		System.Environment.Exit(1);
	*/
	}
} // Errors

}