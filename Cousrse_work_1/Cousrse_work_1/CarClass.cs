using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cousrse_work_1
{
    public class CarClass
    {
        public int CarID { get; set; } //Номер записи автомобиля
        public string CarBrand { get; set; } //Название марки автомобиля
        public string CarModel { get; set; } //Название модели автомобиля
        public int Year { get; set; } //Год выпуска автомобиля
        public string Color { get; set; } //Цвет автомобиля
        public long Price { get; set; } //Цена автомобиля

        public CarClass(int CarID, string CarBrand, string CarModel, int Year, string Color, long Price)
        {
            this.CarID = CarID;
            this.CarBrand = CarBrand;
            this.CarModel = CarModel;
            this.Year = Year;
            this.Color = Color;
            this.Price = Price;

        }

        public CarClass() {
        }

        //Метод возвращает информацию о машине
        public string Write()
        {
            return String.Format("Машина с номером: {0}; марка: {1}; модель: {2}; год выпуска: {3}; цвет: {4}; цена: {5}.", this.CarID, this.CarBrand, this.CarModel, this.Year, this.Color, this.Price);
        }

    }
}
