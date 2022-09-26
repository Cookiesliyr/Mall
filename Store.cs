using Mall;
using System;

// this example were made to showcase different aspect of the class that we learn in the lecture
namespace Shops {

	// this class represent stores in a mall with services that cost money and time,
	// assuming we are dealing with only one mall
	public class Store {
		// =======================
		// Todo: Problem 5: genereat new name in the store list and test the results, how does it work?
		// =======================

		// static means it belong to the class, the list of names are used to generate random stores for testing
		protected static List<string> GeneratedStoreNames = new List<string> { "KFC", "Ford", "Apple", "Cafea", "IceLord", "Computer Shop", "Mac", "AFD" };
		private static List<Store> StoresList = new List<Store>();	// by making the list private we can only use the stores by other functions

		private string StoreName;									// by making fields private other classes can't adjust them, to protect the programmer or others from that
		private List<Service> ServicesList = new List<Service>(); 
		public bool[] Seats;										// false for not used

		// because we need to record the new store in the list we made the constructor private, private constructor can only be used in the class
		private Store (string name, int seatsNum) {
			StoreName=name;
			Seats = new bool[seatsNum];
		}

		public static void CreateStore (string name, int seatsNum) {
			StoresList.Add(new Store(name, seatsNum));
		}

		public static void GenerateStores (int StoreNum, int ServicesNum = 4) {
			for (int i = 0; i < StoreNum; i++) {
				Store TempStore = new Store(GeneratedStoreNames[new Random().Next(0, GeneratedStoreNames.Count)], new Random().Next(2, 10));
				for (int j = 0; j<ServicesNum; j++) TempStore.ServicesList.Add(Service.GenerateService());
				StoresList.Add(TempStore);
			}
		}

		public static void CreateServeice (int StoreNum, string id, int cost, int time) {
			StoresList[StoreNum].ServicesList.Add(new Service(id, cost, time)); 
		}

		public static void GenerateService (int StoreNum, int CostMin, int CostMax, int WaitTimeMin, int WaitTimeMax) {
			StoresList[StoreNum].ServicesList.Add(Service.GenerateService(CostMin, CostMax, WaitTimeMin, WaitTimeMax));
		}

		public static int PrintStores () { 
			for (int i = 0; i < StoresList.Count; i++) 
				Console.WriteLine((i+1) + "- " + StoresList[i].StoreName);
			return StoresList.Count;
		}

		public static int StoresCount () { return StoresList.Count; }

		private void PrintServices () { 
			for (int i = 0; i < ServicesList.Count; i++) 
				Console.WriteLine((i+1) + "- " + ServicesList[i]);
		}

		// =======================
		// Todo: Problem 5: find why Customer name prints incorrectly
		// =======================
		public static void EnterStore (int storeNum, Customer customer) {
			Store TempStore = StoresList[storeNum];
			int TempSeat = -1; // to memorize which seat did this customer take, -1 means didn't find one
			
			if ((TempSeat = TempStore.CheckService(customer)) == -1 ) { 
				Console.WriteLine("No seats avilable!" + customer + " left the building."); 
				return; 
			}

			TempStore.PrintServices();

			Console.WriteLine("Which service would you like to youse, " + customer + " ?");

			int intVal;
			do {
				string input = Console.ReadLine();
				intVal = 0;
				if (int.TryParse(input, out intVal)) 
					TempStore.ServicesList[intVal].Serve(customer);
				
			} while (intVal!=0);
			TempStore.Seats[TempSeat]=false;	// to free the seat for future customer
		}

		public int CheckService (Customer x) {
			for (int i = 0; i<=Seats.Length; i++)
				if (Seats[i]==false) {
					Seats[i]=true;
					Console.WriteLine( "Customer " + x + " found Seat " + i );
					return i;
				}
			
			return -1;
		}

		// This is an inner or private class, you can create instance of it only in the holder class (the store in this case)
		private class Service {
			protected static List<string> GeneratedServiceName = new List<string> { "Item A", "Item B", "Item C", "Burger", "Cheese Burger", "Gold", "Sealed Box"};
			string id;
			int cost, time;

			public Service (string id, int cost, int time) {
				this.id=id;
				this.cost=cost;
				this.time=time;
			}

			public static Service GenerateService (int CostMin = 10, int CostMax = 100, int TimeMin = 1, int TimeMax = 5) {
				Random RNG = new Random();
				return new Service(GeneratedServiceName[RNG.Next(0, GeneratedServiceName.Count)], RNG.Next(CostMin, CostMax), RNG.Next(TimeMin, TimeMax));
			}

			public void Serve (Customer cus) {
				Console.WriteLine(cus.name + " payed "+cost);
				cus.charge(cost);
			}

			public override string ToString () {
				return id + " : " + cost + "$, takes: " + time + " minutes";
			}
		}
	}
}
