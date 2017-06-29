using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor.Entity {
	public class Movie {
		public const int CHILDRENS = 2;
		public const int REGULAR = 0;
		public const int NEW_RELEASE = 1;

		public string Title { get; set; }

		private int PriceCode { get; }

		public Movie( string title, int priceCode ) {
			Title = title;
			PriceCode = priceCode;
		}

		public double GetCharge( int dayRented) {
			double result = 0;

			switch( PriceCode ) {
				case Movie.REGULAR:
					result += 2;
					if( dayRented > 2 ) {
						result += ( dayRented - 2 ) * 1.5;
					}
					break;
				case Movie.NEW_RELEASE:
					result += dayRented * 3;
					break;
				case Movie.CHILDRENS:
					result += 1.5;
					if( dayRented > 3 ) {
						result += ( dayRented - 3 ) * 1.5;
					}
					break;
				default:
					break;
			}
			return result;
		}

		public int GetFrequentRenterPoints( int dayRented ) {
			if( PriceCode == Movie.NEW_RELEASE && ( dayRented > 1 ) ) {
				return 2;
			} else {
				return 1;
			}
		}
	}

	public class Rental {
		public Movie Movie { get; }
		private int DaysRented { get; }

		public Rental( Movie movie, int daysRented ) {
			this.Movie = movie;
			this.DaysRented = daysRented;
		}

		public double GetCharge() {
			return Movie.GetCharge( DaysRented );
		}

		public int GetFrequentRenterPoints() {
			return Movie.GetFrequentRenterPoints( DaysRented );
		}
	}

	public class Customer {
		private string Name { get; set; }

		private readonly List<Rental> _rentals;

		public Customer(string name ) {
			this.Name = name;
			_rentals = new List<Rental>();
		}

		public void AddRental( Rental rental ) {
			this._rentals.Add( rental );
		}

		private double GetTotalCharge()
		{
			return _rentals.Sum(each => each.GetCharge());
		}

		private int GetTotalFrequentRenterPoints()
		{
			return _rentals.Sum(each => each.GetFrequentRenterPoints());
		}

		public string Statement() {
			string result = "Rental Record for " + Name + "\n";

			result = _rentals.Aggregate(result, (current, each) => current + ("\t" + each.Movie.Title + "\t" + each.GetCharge() + "\n"));

			result += "Amount owed is " + GetTotalCharge() + "\n";
			result += "You earned " + GetTotalFrequentRenterPoints() + " frequent renter points";
			return result;
		}
		
	}
}
