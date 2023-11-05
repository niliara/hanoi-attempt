class Program
{
	public static class Globals
	{
		public static List<int> pole1 = new List<int>() {3, 2, 1};
		public static List<int> pole2 = new List<int>();
		public static List<int> pole3 = new List<int>();
		public static List<int>[] poles = new List<int>[3];

		public static int maxSpace = 16;
	}

	public static void Main(string[] args)
	{
		bool playing = true;
		string answer;

		Globals.poles[0] = Globals.pole1;
		Globals.poles[1] = Globals.pole2;
		Globals.poles[2] = Globals.pole3;


		Console.WriteLine("\nWelcome to Tower of Hanoi!!!\n");

		Console.WriteLine(Globals.poles.Length);

		while (playing)
		{
			Console.Write("\nChoose your action!! ");
			answer = Console.ReadLine();

			switch(answer != null ? answer.Split(' ')[0] : answer)
			{
				case "mv":
					moveHanoi(answer.Split(' '));
					break;
				case "see":
					printHanoi();
					break;
				case "q":
					playing = false;
					break;
				case "h":
					Console.WriteLine("\nAvailable actions:\n- see : check towers\n- h : help\n- q : quit");
					break;
				default:
					Console.WriteLine("\nnot a valid action!! type h to see available actions");
					break;
			}
		}


	}

	public static void printHanoi()
	{
		int width;
		for (int i=5; i>0; i--)
		{
			for (int r=0; r<Globals.poles.Length; r++)
			{
				width = 0;
				if (Globals.poles[r] != null)
				{
					if (Globals.poles[r].Count >= i)
					{
						width = Globals.poles[r][i-1];
					}
				}
				

				Console.Write(
					String.Concat(Enumerable.Repeat(" ",Globals.maxSpace - width)) +
					(width == 0 ? "||" : "(" + String.Concat(Enumerable.Repeat("-",width*2)) + ")") +
					String.Concat(Enumerable.Repeat(" ",Globals.maxSpace - width))
					);
			}
			Console.WriteLine("");

		}

	}

	public static void moveHanoi(string[] movingHanoi)
	{
		if (movingHanoi.Length < 3)
		{
			Console.WriteLine("not enough arguments!!");
			return;
		}

		int[] operators = new int[2];

		try
		{
			operators[0] = int.Parse(movingHanoi[1]);
			operators[1] = int.Parse(movingHanoi[2]);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return;
		}

		if (!(Enumerable.Range(0,3).Contains(operators[0]) && Enumerable.Range(0,3).Contains(operators[1])))
		{
			Console.WriteLine("invalid poles");
			return;
		}

		if (operators[0] == operators[1]
				|| Globals.poles[operators[0]] == null
				|| (!Globals.poles[operators[1]].Any() ? false : Globals.poles[operators[0]].LastOrDefault() > Globals.poles[operators[1]].LastOrDefault()))
		{
			Console.WriteLine("invalid move");
			return;
		}

		var last = Globals.poles[operators[0]].Count-1;
		Globals.poles[operators[1]].Add(Globals.poles[operators[0]][last]);
		Globals.poles[operators[0]].RemoveAt(last);
		//Array.Clear(Globals.poles[operators[0]],last-1,1);
		//Globals.poles[operators[0]].RemoveAt(last-1);
		
		for (int i=1; i<Globals.poles[operators[0]].Count; i++){
			Console.WriteLine(Globals.poles[operators[0]][i]);
		}

		printHanoi();

	}
}

