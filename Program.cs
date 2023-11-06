class Program
{
	public static class Globals
	{
		public static List<int> pole1 = new List<int>();
		public static List<int> pole2 = new List<int>();
		public static List<int> pole3 = new List<int>();
		public static List<int>[] poles = new List<int>[3];

		public static int maxSpace;
		public static int disks;
	}

	public static void Main(string[] args)
	{
		bool playing = true;
		string answer;

		Console.WriteLine("\nWelcome to Tower of Hanoi!!!");

		
		while (true){
			Console.Write("\nWhat how many disks do you want? ");
			try
			{
				Globals.disks = int.Parse(Console.ReadLine());
				break;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		if (Globals.disks < 3)
		{
			Globals.disks = 3;
		}
		
		if (Globals.disks > 20)
		{
			Globals.disks=20;
		}

		Globals.poles[0] = Globals.pole1;
		Globals.poles[1] = Globals.pole2;
		Globals.poles[2] = Globals.pole3;

		for (int i=Globals.disks; i>0; i--)
		{
			Globals.poles[0].Add(i);
		}

		Globals.maxSpace = 5 + Globals.disks;


		printHanoi();

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
					Console.WriteLine("\nAvailable actions:\n- mv <colNumber(0,1,2)> <colNumber>\n- see : check towers\n- h : help\n- q : quit");
					Console.WriteLine("\nThe objective of this game is to move all the disks to the 3rd pole (pole 2).");
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
		for (int i=Globals.disks+2; i>0; i--)
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
		
		printHanoi();

	}
}

