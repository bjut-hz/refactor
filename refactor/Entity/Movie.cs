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

		private Price _price;

		public string Title { get; private set; }

		public Movie( string title, int priceCode ) {
			Title = title;
			SetPriceCode( priceCode );
		}

		public double GetCharge( int dayRented ) {
			return _price.GetCharge( dayRented );
		}

		public void SetPriceCode( int arg ) {
			switch( arg ) {
				case REGULAR:
					_price = new RegularPrice();
					break;
				case CHILDRENS:
					_price = new ChildrensPrice();
					break;
				case NEW_RELEASE:
					_price = new NewReleasePrice();
					break;
				default:
					break;
			}
		}


		public int GetFrequentRenterPoints( int dayRented ) {
			return _price.GetFrequentRenterPoints( dayRented );
		}
	}

	public abstract class Price {
		public abstract double GetCharge( int dayRented );
		public virtual int GetFrequentRenterPoints( int dayRented ) {
			return 1;
		}

		public abstract int GetPriceCode();
	}

	public class RegularPrice : Price {
		public override double GetCharge( int dayRented ) {
			double result = 0;
			result += 2;
			if( dayRented > 2 ) {
				result += ( dayRented - 2 ) * 1.5;
			}
			return result;
		}

		public override int GetFrequentRenterPoints( int dayRented ) {
			return base.GetFrequentRenterPoints( dayRented );
		}

		public override int GetPriceCode() {
			return Movie.REGULAR;
		}
	}

	public class ChildrensPrice : Price {
		public override double GetCharge( int dayRented ) {
			double result = 0;
			result += 1.5;
			if( dayRented > 3 ) {
				result += ( dayRented - 3 ) * 1.5;
			}
			return result;
		}

		public override int GetFrequentRenterPoints( int dayRented ) {
			return base.GetFrequentRenterPoints( dayRented );
		}

		public override int GetPriceCode() {
			return Movie.CHILDRENS;
		}
	}

	public class NewReleasePrice : Price {
		public override double GetCharge( int dayRented ) {
			return dayRented * 3;
		}

		public override int GetFrequentRenterPoints( int dayRented ) {
			if( dayRented > 1 ) {
				return 2;
			} else {
				return 1;
			}
		}

		public override int GetPriceCode() {
			return Movie.NEW_RELEASE;
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

		public Customer( string name ) {
			this.Name = name;
			_rentals = new List<Rental>();
		}

		public void AddRental( Rental rental ) {
			this._rentals.Add( rental );
		}

		private double GetTotalCharge() {
			return _rentals.Sum( each => each.GetCharge() );
		}

		private int GetTotalFrequentRenterPoints() {
			return _rentals.Sum( each => each.GetFrequentRenterPoints() );
		}

		public string Statement() {
			string result = "Rental Record for " + Name + "\n";

			result = _rentals.Aggregate( result, ( current, each ) => current + ( "\t" + each.Movie.Title + "\t" + each.GetCharge() + "\n" ) );

			result += "Amount owed is " + GetTotalCharge() + "\n";
			result += "You earned " + GetTotalFrequentRenterPoints() + " frequent renter points";
			return result;
		}

	}
}
