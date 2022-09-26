using System;
using Shops;

namespace Mall {
	public class Program {
		public static void Main (string [] args){

			// =======================
			// Todo: Problem 1, 2, 3
			// 1: Create a new Customer and run its simulation
			// 2: Create 3 new stores
			// 3.0 optional: comment out the generated stores
			// 3: Generate services to two stores and create services manually to the third
			// =======================

			Store.GenerateStores(10);

			Customer Cookie = new Customer("Cookiesliyr", "123 ABC", 100);
			Customer Anyone = new Customer("Any", "", 90000);

			Cookie.RunSimulation();	

		}
	}

	

}