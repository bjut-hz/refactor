using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor.Entity {
	public class Movie {
		public const int CHILDREDNS = 2;
		public const int REGULAR = 0;
		public const int NEW_RELEASE = 1;

		public string Title { get; set; }
		public int PriceCode { get; set; }

		public Movie( string _title, int _price_code ) {
			Title = _title;
			PriceCode = _price_code;
		}
	}

	public class Rental {
		public Movie Movie { get; set; }
		public int DaysRented { get; set; }

		public Rental( Movie _movie, int _days_rented ) {
			this.Movie = _movie;
			this.DaysRented = _days_rented;
		}
	}

	public class Customer {
		public string Name { get; set; }

		private List<Rental> _rentals;

		public Customer(string _name ) {
			this.Name = _name;
			_rentals = new List<Rental>();
		}

		public void AddRental( Rental _rental ) {
			this._rentals.Add( _rental );
		}

		public string Statement() {
			double total_amount = 0;
			int frequent_renter_points = 0;

			string result = "Rental Record for " + Name + "\n";
			
			foreach( Rental each in _rentals ) {
				double this_amount = 0;

				switch( each.Movie.PriceCode ) {
					case Movie.REGULAR:
						this_amount += 2;
						if( each.DaysRented > 2 ) {
							this_amount += ( each.DaysRented - 2 ) * 1.5;
						}
						break;
					case Movie.NEW_RELEASE:
						this_amount += each.DaysRented * 3;
						break;
					case Movie.CHILDREDNS:
						this_amount += 1.5;
						if( each.DaysRented > 3 ) {
							this_amount += ( each.DaysRented - 3 ) * 1.5;
						}
						break;
				}

				frequent_renter_points++;

				if( each.Movie.PriceCode == Movie.NEW_RELEASE && (each.DaysRented > 1)) {
					frequent_renter_points++;
				}

				result += "\t" + each.Movie.Title + "\t" + this_amount.ToString() + "\n";
				total_amount += this_amount;
			}

			result += "Amount owed is " + total_amount.ToString() + "\n";
			result += "You earned " + frequent_renter_points.ToString() + " frequent renter points";
			return result;
		}
		
	}
}
