using System;

namespace Shops {
	public class Customer {
		public string name;
		protected string address;
		private int cash;

		public Customer (string name, string address, int cash) {
			this.name=name; this.address=address; this.cash=cash;
		}

		// the main loop for simulation for a singler customer
		public void RunSimulation () {
			string input = "";

			while (input != "exit") {
				PrintMenu();
				switch (Console.ReadLine()){
					case "1":
						Console.WriteLine(name+" Walk around and see the following stores");
						
						if ((Store.PrintStores()) == 0) {
							Console.WriteLine("The Mall is deserted, all stores are ruined! what a depressing sight!");
							break;
						}
						Console.WriteLine("Which store do you want to enter? type the number or anything else to return");
						string userInput = Console.ReadLine();
						int tryVal =-1; 
						if ( int.TryParse(userInput, out tryVal) && tryVal > 0 && tryVal < Store.StoresCount() ) 
							Store.EnterStore(tryVal, this);
						
					break;
					case "2": 
						Console.WriteLine("You have " + cash + "$");
					break;
					case "3": 
						switch (new Random().Next(0,4)) { // Maxvalue is not included\excluded from the range
							case 0: Console.WriteLine("My name is " + name + " and my address is " + address); break;
							case 1: Console.WriteLine("Good day! How are you?"); break;
							case 2: Console.WriteLine(name + " is here, and i am happy to meet you!"); break;
							case 3: Console.WriteLine("Greetings! I am " + name + " Good to meet you!"); break;
						}					
					break;
					case "4":
						Console.WriteLine(name+" got tired, time to return home!");
						return;
				}
			}
		}

		private void PrintMenu () {
			Console.WriteLine("\nWhat would " + name + " like to do?"+
				"\n 1- Check the stores"+
				"\n 2- Check cash"+
				"\n 3- Greate people"+
				"\n 4- Return home"
				);

		}

		public void charge (int charged) {
			cash -= charged;
		}
	}
}