using System;
using System.Text.Json.Serialization;

namespace scraperel.model
{
	public class MenuItem : IEquatable<MenuItem>
	{
		[JsonIgnore]
		public int Id { get; set; }
		public string MenuTitle { get; set; }
		public string MenuDescription { get; set; }
		public string MenuSectionTitle { get; set; }
		public string DishName { get; set; }
		public string DishDescription { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return DishName;
		}

		/// <inheritdoc />
		public bool Equals(MenuItem other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return DishName == other.DishName;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((MenuItem) obj);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return (DishName != null ? DishName.GetHashCode() : 0);
		}
	}
}