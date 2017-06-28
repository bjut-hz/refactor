using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor.Entity;

namespace refactor {
	class Program {
		static void Main( string[] args ) {
			Movie movie1 = new Movie( "X-man", Movie.REGULAR );
			Movie movie2 = new Movie( "Captain American", Movie.NEW_RELEASE );
			Movie movie3 = new Movie( "Mario", Movie.CHILDREDNS );

			Customer hz = new Customer( "bjut-hz" );
			Rental rental1 = new Rental( movie1, 7 );
			Rental rental2 = new Rental( movie2, 4 );
			Rental rental3 = new Rental( movie3, 20 );

			hz.AddRental( rental1 );
			hz.AddRental( rental2 );
			hz.AddRental( rental3 );

			Console.WriteLine( hz.Statement() );
			Console.ReadKey();

		}
	}
}
