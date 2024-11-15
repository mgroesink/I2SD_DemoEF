namespace Overloading.Models
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }

        public Car(string make, string model, int? year)
        {
            Make = make;
            Model = model;
            Year = year;
        }

        public Car(string make, string model)
        {
            Make = make;
            Model = model;
            Year = 0;
        }

        public Car(string make)
        {
            Make = make;
            Model = "Unknown";
            Year = 0;
        }

        public Car()
        {
            Make = "Unknown";
            Model = "Unknown";
            Year = 0;
        }
    }
}
