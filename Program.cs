using System;
using Figures;
using System.Threading;

namespace GeometricObjects
{
	class Program
	{
		static void Main(string[] args)
		{
			// тест №1 добавляем основную группу и подгруппу. Расчитываем периметр без множителя
			Console.WriteLine("тест № 1 : Добавляем основную группу, подгруппу и экземпляры." +
				" Расчитываем периметр без массштабирующего свойства множителя \n");
			Component Group_1 = new Group("Группа_1");					// определяем группу фигур
			Component Subgroup_1 = new Group("Подгруппа_1");			// определяем подгруппу фигур
			Component Rect_1 = new Rectangle(5, 3, "Прямоугольник_1");	// создаём экземпляры фигур
			Component Rect_2 = new Rectangle(5, 10, "Прямоугольник_2");
			Component Par_1 = new Parallelepiped(5, 5, 2, "Параллелепипед_1");
			Component Par_2 = new Parallelepiped(10, 5, 2, "Параллелепипед_2");
			Component Circ_1 = new Circle(5, "Круг_1");
			Component Sph_1 = new Sphere(5, "Сфера_1");
			Component Cyl_1 = new Cylinder(10, 5, "Цилиндр_1");
			Subgroup_1.Add(Rect_1);						// добавляем экземпляры фигур в подгруппу
			Subgroup_1.Add(Rect_2);
			Subgroup_1.Add(Par_1);			
			Group_1.Add(Subgroup_1);                    // добавляем подгруппу в группу
			Group_1.Add(Par_2);                         // добавляем экземпляры фигур в основную группу
			Group_1.Add(Circ_1);
			Group_1.Add(Sph_1);
			Group_1.Add(Cyl_1);
			// вызов метода расчёта меры для группы, 0 - расчёт периметра, 1 - множитель для свойств экземпляров
			Group_1.Measure(0,1);
			// вызов метода расчёта меры для подгруппы, 0 - расчёт периметра, 1 - множитель для свойств экземпляров
			Subgroup_1.Measure(0,1);                      
			Group.fParam = 0;                           // обнуление статической переменной класса, которая накапливает меру

			// тест №2 Расчитываем периметр с массштабирующем свойства множителем
			Latency();                          // задержка перед нажатием кнопки
			Console.WriteLine("тест № 2 : Расчитываем периметр с массштабирующем свойства множителем \n");
			Group_1.Measure(0, 2);              // 2 - множитель для свойств экземпляров
			Subgroup_1.Measure(0, 2);           // 2 - множитель для свойств экземпляров
			Group.fParam = 0;

			// тест №3 Расчитываем площадь фигур и площадь всей поверхности тел отдельно для Подгруппы
			Latency();
			Console.WriteLine("тест № 3 : Расчитываем площадь под фигурами и телами \n");
			Group_1.Measure(1, 1);
			Subgroup_1.Measure(1, 1);
			Group.fParam = 0;

			// тест №4 Расчитываем площадь фигур и площадь всей поверхности тел отдельно для Подгруппы 
			Latency();
			Console.WriteLine("тест № 4 : Расчитываем площадь фигур и площадь всей поверхности тел отдельно для Подгруппы \n");
			Subgroup_1.Measure(2, 1);
			Group.fParam = 0;

			// тест №5 Удаляем из Группы Параллелепипед_2. Расчитываем объём тел отдельно для Группы
			Latency();
			Console.WriteLine("тест № 5 : Удаляем из Группы Параллелепипед_2. Расчитываем объём тел отдельно для Группы \n");
			Group_1.Remove(Par_2);
			Group_1.Measure(3, 1);
			Group.fParam = 0;
			Console.WriteLine("\nТесты завершены. Нажмите любую кнопку. \n");
			Console.ReadKey();
		}
		public static void Latency()	// задержка перед нажатием кнопки 
		{
			Thread.Sleep(1000);
			Console.WriteLine("Нажмите любую кнопку чтобы началь следующий тест \n");
			Console.ReadKey();
		}
	}
	public class Cylinder : Circle		// класс - наследник, определённый в пользовательской программе
	{
		float fPerimeter; 
		public float Height { get; set; }
		public Cylinder(float fDiameter, float fHeight, string name) : base(fDiameter, name)
		{
			this.Height = fHeight;
		}
		public override float Volume()         // переопределение получения объёма
		{
			fPerimeter = (Diameter / 2);
			return fPi * (float)Math.Pow(fPerimeter, 2) * Height; 
		}
		public override float SurfaceArea()    // переопределение получения площади поверхности
		{ 
			return 2 * fPi * (float)Math.Pow(fPerimeter, 2) + 2 * fPi * fPerimeter * Height; 
		}
		public override void Scale(float fMultiplier)
		{
			Diameter *= fMultiplier;
			Height *= fMultiplier;
		}
	}
}
