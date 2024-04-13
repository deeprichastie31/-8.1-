using System.ComponentModel.Design;
using System.Data.Common;
using System.Runtime.CompilerServices;
/*
что умеет делать данный класс и каково будет его поведение?
В его поведении, т.е в интерфейсе описаны следующие действия: Перевод в радианы, вывод на экран, получение синуса, сравнение через виртуалочку
в самом классе описаны остальные методы:
1) конструктор, который передает полученные извне значения в свойства; 
2) приватный перевод в углы, который не нужен пользователю, это будет делаться автоматически;
3) Изменение значения угла;
4) перезаписанное сравнение углов;
5) 
 */
Console.WriteLine("Сколько углов вы хотите создать?");
int k = int.Parse(Console.ReadLine());
string[] countOfangles = new string[k];
double[] valueAngle = new double[k];
for (int i = 0; i < k; i++)
{
    countOfangles[i] = $"angle_{i + 1}"; // генерация углов

}
bool marker = true;
while (marker)
{
    while (k != 0)
    {
        string angleu;
        for (int i = 0; i < countOfangles.Length; i++)
        {
            Console.WriteLine("Введите значение градусов: ");
            double degree = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите значение минут: ");
            int minutes = int.Parse(Console.ReadLine());
            angleu = countOfangles[i];
            Angle angles = new Angle(degree, minutes, angleu);
            valueAngle[i] = angles.ToDegree();
            angles.Print();
            k--;
        }
    }
    Console.WriteLine("\r\r\r\rМЕНЮ\n 1) Изменить значение угла \n 2) Cравнить углы\n 3) выйти из программы");
    int action = int.Parse(Console.ReadLine());
    switch(action) 
    {
        case 2:
            Console.WriteLine("Какие углы вы хотите сравнить?");
            int firstDegree = int.Parse(Console.ReadLine());
            int secondDegree = int.Parse(Console.ReadLine());
            Angle.Compare(valueAngle[firstDegree - 1], valueAngle[secondDegree - 1]);
            break;
        case 1:
            Console.WriteLine("Выберите угол, с которым вы хотите работать");
            int Degree = int.Parse(Console.ReadLine());
            Angle change = new Angle(valueAngle[Degree - 1], 0, countOfangles[Degree - 1]);
            
            Console.WriteLine("Что вы хотите сделать с углом?");
            string actions = Console.ReadLine();
            valueAngle[Degree - 1] = change.ChangeValue(actions);
            break;
        case 3:
            Console.WriteLine("Bye!");
            marker = false;
            break;
    }
}

interface Iangle
{
    protected double TransferToRadian(); // перевод угла в радианы
    void Print(); // метод вывода
    double GetSinus(); // Получение синуса
    void Compare(double degree, double degree_2); // сравнение углов 

}
abstract class angle : Iangle // базовый абстрактный класс, который наследует поведение основного класса через интерфейс
{
    protected double degree;
    protected int minutes;
    protected double rad; // причем радианы я передавать не могу, поэтому я не буду сувать его в конструктор, а просто помечу его protected 
    protected double sin;
    public double GetSinus() // Получает синус
    {
        sin = Math.Sin(rad);
        return sin;
    }
     public double TransferToRadian() // получает радианы
    {
        rad = degree * Math.PI / 180;
        return rad;
    }
    public void Compare(double degree, double degree_2)
    {
    }
    public abstract void Print(); // абстракатные классы в uml 
}
// по сути выше, я расписал полное поведение объекта угол
class Angle : angle
{
    public Angle(double degree, int minute, string angle) : base() // конструктор имеется
    {
        Console.WriteLine(angle);
        Degree = degree; // Чтобы конструктор нормально работал со свойствами, необходимо свойсвту присваивать значение передаваемой извне переменной. 
        Minute = minute;
    }
    public double Degree // приват, чтобы была возможность работать с переменной только через параметры экземпляра
    {
        get { return degree; }
        set 
        {
            if (value < 0)
                value = -value;
            degree = value;
        }
    }
    public int Minute // приват, чтобы была возможность работать с переменной только через параметры экземпляра
    {
        get { return minutes; }
        set
        {
            if (value > 60)
            {
                while (value > 60)
                {
                    Console.WriteLine("Минуты не могут быть больше 60\n Введите минуты в соответсвии с ограничением");
                    value = int.Parse(Console.ReadLine());
                }
            }
            if (value < 0) // если они отрицательны, то переводит в положительное русло
            {
                value = -value;
            }
            minutes = value;
        }
    }
    public double ToDegree() // перевод минут в углы. Сделал приватным, потому что оператор перевода минут в углы будет автоматическми, независимым от самого пользователя 
    {
        degree = degree + minutes / 60;
        if (degree > 360)
        {
            do
            {
                degree -= 360; // если угол больше 360, чтобы сборсить его
            } while (degree >= 360);
        }
        return degree; // нам, так думаю, нужны только градусы
    }
    public double ChangeValue(string action)
    {
        int value;
        switch (action)
        {
            case "minus" or "-":
                Console.WriteLine("На какое значение вы хотите уменьшить угол?");
                value = int.Parse(Console.ReadLine());
                Degree -= value;
                Print();
                break;
            case "plus" or "+":
                Console.WriteLine("На какое значение вы хотите увеличить угол?");
                value = int.Parse(Console.ReadLine());
                Degree += value;
                Print();
                break;
        }
        return degree;
    }
    public static void Compare(double degree, double degree_2)
    {
        if (degree < degree_2)
            Console.WriteLine($"{degree} угол < {degree_2}");
        else if (degree > degree_2)
            Console.WriteLine($"{degree} > {degree_2}");
        else Console.WriteLine("Углы равны");
    }
    public override void Print()
    {
        Console.WriteLine("Угол: " + ToDegree() + "\n" + "Радианы: " + TransferToRadian() + " " + "\nsin: " + GetSinus()+"\n");
    }
}