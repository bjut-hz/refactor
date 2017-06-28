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

		public double GetCharge() {
			double result = 0;

			switch( Movie.PriceCode ) {
				case Movie.REGULAR:
					result += 2;
					if( DaysRented > 2 ) {
						result += ( DaysRented - 2 ) * 1.5;
					}
					break;
				case Movie.NEW_RELEASE:
					result += DaysRented * 3;
					break;
				case Movie.CHILDREDNS:
					result += 1.5;
					if( DaysRented > 3 ) {
						result += ( DaysRented - 3 ) * 1.5;
					}
					break;
			}
			return result;
		}

		public int GetFrequentRenterPoints() {
			if( Movie.PriceCode == Movie.NEW_RELEASE && ( DaysRented > 1 ) ) {
				return 2;
			} else {
				return 1;
			}
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

		private double GetTotalCharge() {
			double result = 0;

			foreach( Rental each in _rentals ) {
				result += each.GetCharge();
			}

			return result;
		}

		private int GetTotalFrequentRenterPoints() {
			int result = 0;

			foreach( Rental each in _rentals ) {
				result += each.GetFrequentRenterPoints();
			}

			return result;
		}

		public string Statement() {
			string result = "Rental Record for " + Name + "\n";

			foreach( Rental each in _rentals ) {
				result += "\t" + each.Movie.Title + "\t" + each.GetCharge().ToString() + "\n";
			}

			result += "Amount owed is " + GetTotalCharge().ToString() + "\n";
			result += "You earned " + GetTotalFrequentRenterPoints().ToString() + " frequent renter points";
			return result;
		}
		
	}
}
