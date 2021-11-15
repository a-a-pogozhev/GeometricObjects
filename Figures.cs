using System;
using System.Collections.Generic;

namespace Figures
{
	public abstract class Component			// абстрактный класс-родитель для групп, подгрупп, фигур
	{
		public string sName { get; set; }

		public Component(string sName)
		{
			this.sName = sName;
		}
		public virtual float Perimeter()    // абстрактный метод для получения периметра
		{ 
			return 0; 
		}		
		public virtual float Area()         // абстрактный метод для получения площади
		{ 
			return 0; 
		}       
		public virtual float SurfaceArea()  // абстрактный метод для получения площади поверхности плоских фигур и тел
		{ 
			return 0; 
		}		
		public virtual float Volume()       // абстрактный метод для получения объёма геометрических тел
		{ 
			return 0; 
		}
		public virtual float CommonMethod()       // абстрактный метод, зарезервирован для переопределения 
		{ 
			return 0; 
		}
		public virtual void Scale(float fMultiplier)    // абстрактный метод изменения свойств объектов с помощью множителя
		{ }
		public virtual void Add(Component component)    // абстрактный метод добавления групп, подгрупп, фигур и тел
		{ }
		public virtual void Remove(Component component) // абстрактный метод удаления групп, подгрупп, фигур и тел
		{ }
		public virtual float Measure(float fMeasure, float fMultiplier)    // абстрактный метод расчёта меры и применения множителя
		{ 
			return 0;	
		}
	}
	public class Group : Component
	{
		public static float fParam = 0;                             // статическая переменная класса для хранения меры
		public List<Component> components = new List<Component>();	// создание экземпляра группы или подгруппы
		public Group(string sName) : base(sName)
		{ }
		public override void Add(Component component)
		{
			components.Add(component);
		}

		public override void Remove(Component component)
		{
			components.Remove(component);
		}
		public override float Measure(float fMeasure, float fMultiplier)
		{
			Console.WriteLine("Группа: " + sName);
			for (int i = 0; i < components.Count; i++)
			{
				switch (fMeasure)			// выбор метода для расчёта меры
				{
					case 0:                 // ко всем экземплярам будет применён расчёт меры "Периметр фигуры или основания тела" 
						Console.WriteLine("Элемент группы:	" + components[i].ToString() + "	" + components[i].sName);
						components[i].Scale(fMultiplier);
						fParam += components[i].Perimeter();
						Console.WriteLine("Периметр элемента:	" + components[i].Perimeter().ToString());
						break;
					case 1:                 // ко всем экземплярам будет применён расчёт меры "Площадь, занимаемая фигурой или телом" 
						Console.WriteLine("Элемент группы:	" + components[i].ToString() + "	" + components[i].sName);
						components[i].Scale(fMultiplier);
						fParam += components[i].Area();
						Console.WriteLine("Площадь элемента:	" + components[i].Area().ToString());
						break;
					case 2:                 // ко всем экземплярам будет применён расчёт меры "Площадь фигуры или всей поверхности тела" 
						Console.WriteLine("Элемент группы:	" + components[i].ToString() + "	" + components[i].sName);
						components[i].Scale(fMultiplier);
						fParam += components[i].SurfaceArea();
						Console.WriteLine("Площадь поверхности элемента:	" + components[i].SurfaceArea().ToString());
						break;
					case 3:                 // ко всем телам будет применён расчёт меры "Объём тела" 
						Console.WriteLine("Элемент группы:	" + components[i].ToString() + "	" + components[i].sName);
						components[i].Scale(fMultiplier);
						fParam += components[i].Volume();
						Console.WriteLine("Объём элемента:	" + components[i].Volume().ToString());
						break;
					default:                // ко всем экземплярам будет применён зарезервированный метод
						Console.WriteLine("Элемент группы:	" + components[i].ToString() + "	" + components[i].sName);
						components[i].Scale(fMultiplier);
						fParam += components[i].CommonMethod();
						Console.WriteLine("Зарезервированная мера:	" + components[i].CommonMethod().ToString());
						break;
				}
			}
			Console.WriteLine("Общая мера элементов:	" + fParam + "\n");
			return fParam;
		}
	}

	public class Rectangle : Component                // производный класс Прямоугольника
	{
		public float Length { get; set; }
		public float Width { get; set; }
		public Rectangle(float fLength, float fWidth, string sName) : base(sName)
		{
			this.Length = fLength;
			this.Width = fWidth;
			this.sName = sName;
		}
		public override float Perimeter()   // переопределение получения периметра
		{	
			return (Length + Width) * 2;		
		}
		public override float Area()        // переопрелеление получения площади
		{	
			return Length * Width;		
		}
		public override float SurfaceArea()        // переопрелеление получения площади поверхности
		{	
			return Length * Width;	
		}
		public override void Scale(float fMultiplier) 
		{
			Length *= fMultiplier;
			Width *= fMultiplier;
		}
	}

	public class Parallelepiped : Rectangle                // производный класс Параллелепипед
	{
		public float Height { get; set; }
		public Parallelepiped(float fLength, float fWidth, float fHeight, string sName) : base(fLength, fWidth, sName)
		{
			this.Height = fHeight;
		}
		public override float Volume()       // переопределение получения объёма
		{	
			return Length * Width * Height; 
		}
		public override float SurfaceArea()   // переопределение получения площади поверхности
		{	
			return (Length * Width + Length * Height + Width * Height) * 2; 
		}
		public override void Scale(float fMultiplier)
		{
			Length *= fMultiplier;
			Width *= fMultiplier;
			Height *= fMultiplier;
		}
	}

	public class Circle : Component                // производный класс Круг
	{
		public float Diameter { get; set; }
		public const float fPi = 3.1415926535F;
		public Circle(float fDiameter, string sName) : base(sName)
		{
			this.Diameter = fDiameter;
		}
		public override float Perimeter()   // переопределение получения периметра
		{	
			return Diameter * fPi;		
		}
		public override float Area()        // переопрелеление получения площади
		{	
			return (fPi * (float)Math.Pow(Diameter, 2)) / 4;		
		}
		public override float SurfaceArea()        // переопрелеление получения площади поверхности
		{	
			return (fPi * (float)Math.Pow(Diameter, 2)) / 4;		
		}
		public override void Scale(float fMultiplier)
		{
			Diameter *= fMultiplier;
		}
	}

	public class Sphere : Circle                // производный класс Сфера
	{
		public Sphere(float fDiameter, string sName) : base(fDiameter, sName)
		{		
			this.Diameter = fDiameter;		
		}
		public override float Volume()			// переопределение получения объёма
		{	
			return  fPi * (float)Math.Pow(Diameter, 3) / 6;		
		}
		public override float SurfaceArea()     // переопрелеление получения площади поверхности
		{	
			return fPi * (float)Math.Pow(Diameter, 2);       
		}
		public override void Scale(float fMultiplier)
		{
			Diameter *= fMultiplier;
		}

	}

}

