using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ChickenLesson
{
    internal class Program
    {
            enum Chiken : byte
            {
                Tom,
                Jerry,
                Margo
            }
            enum TimeOfDay : byte
            {
                Morning,
                Noon,
                Evening,
                Night
            }

        static void Main(string[] args)
        {
            //2. немного развлекательное, но на кодирование: 
            //Имеется 1 курица в клетке. 
            //Курицу необходимо кормить зерном и после она высиживает яйцо. 
            //Возможные действия за один ход: 
            //- покормить курицу 
            //- забрать яйцо 
            //- ничего не делать 
            //Если курица не накормлена, то она умирает. За раз положить допускает 3 - 5 зерен (на вашу фантазию. 
            //При условии если курица накормлена, то яйцо высиживается в этот ход и только одно 

            Console.WriteLine("Приветствуем вас в игре \"Куриная ферма\"");
            Console.WriteLine("Вам предстоит играть за хозяина 3 куриц, каждая сидит в своей клетке");
            Console.WriteLine("Правила игры:");
            Console.WriteLine("В одних сутках есть - утро, день, вечер, ночь");
            Console.WriteLine("Возможные действия за одно время суток:");
            Console.WriteLine("- покормить курицу\n- забрать яйцо\n- по спать\n- по есть");
            Console.WriteLine("У курицы в день должно быть минимум 3 зерна, иначе они съедят вас");
            Console.WriteLine("На то чтобы покормить курицу уйдёт 1 время суток, \nтак как они будут вас атаковать как только вы зайдёте в их клетку чтобы покормить");
            Console.WriteLine("В сутки вам нужно съесть минимум 4 яйца в день чтобы выжить");
            Console.WriteLine("Вам нужно спать хотя бы раз в двое суток, иначе вы умрёте");
            Console.WriteLine("Выживите на ферме с бешенными курицами и постарайтесь жить счастливо, удачи вас, а я по тапкам отсюда");


            sbyte[] cornOfChicken = new sbyte[3] { 0, 0, 0 };
            sbyte[] eggsChicken = new sbyte[3] { 0, 0, 0 };
            byte homeEggs = 0;
            byte timeDaysWithoutSleep = 0;
            uint countEggs = 0;
            uint day = 0; //счётчик дней 
            while (true)
            {
                Console.WriteLine($"\nЭто ваш {++day}-й день на ферме");
                byte ateEggs = 0;
                for (byte timeOfDay = 0; timeOfDay < 4; timeOfDay++)
                {
                    timeDaysWithoutSleep++;
                    Console.WriteLine((TimeOfDay)timeOfDay);
                    Console.WriteLine("Выберите номер действия:\n1 - покормить курицу\n2 - забрать яйцо\n3 - по спать\n4 - по есть");
                    string action = Console.ReadLine();
                    byte numberAction = 0;
                    while (!byte.TryParse(action, out numberAction) || numberAction < 1 || numberAction > 4)
                    {
                        Console.WriteLine("Пожалуйста, введите корректный номер действия");
                        action = Console.ReadLine();
                    }
                    switch (numberAction)
                    {
                        case 1:
                            //пользователь должен выбрать курицу которую хочет покормить 
                            Console.WriteLine("Выберите номер курицы. Есть курицы под номером (0,1,2). Какую из них хотите покормить? ");
                            byte chicken = byte.Parse(Console.ReadLine());
                            //пользователь должен выбрать сколько зерна он хочет положить курице 
                            Console.WriteLine("Сколько зерна хотите положить?(можно положить до 5 зёрен)");
                            cornOfChicken [chicken] += sbyte.Parse(Console.ReadLine());
                            break;
                        case 2:
                            //пользователь должен выбрать у какой курицы хочет забрать яйца (они отправляются на склад)
                            Console.WriteLine("Выберите курицу (0,1,2), у которой хотите забрать яйца");
                            chicken = byte.Parse(Console.ReadLine());
                            homeEggs += (byte) eggsChicken[chicken];
                            eggsChicken[chicken] = 0;
                            break;
                        case 3:
                            //счётчик недосыпа обнуляется 
                            timeDaysWithoutSleep = 0;
                            break;
                        case 4:
                            //пользователь выбирает сколько яиц он хочет съесть (число должно быть меньше, либо равно имеющимся на складе) 
                            Console.WriteLine($"Сколько яиц вы хотите съесть? На складе сейчас {homeEggs} яиц");
                            byte eatEggs = byte.Parse(Console.ReadLine());
                            //количество съеденных в день яиц увеличивается 
                            countEggs += eatEggs;
                            homeEggs -= eatEggs;
                            break;
                    }
                }
                //Количество еды у каждой курицы уменьшается на 3 
                cornOfChicken[0] -= 3; cornOfChicken[1] -= 3; cornOfChicken[2] -=3;

                //Если счётчик недосыпа больше либо равно, чем 8, то заканчиваем игру 
                //Если у одной из куриц меньше 0 зёрен, то заканчиваем игру 
                //Если пользователь съел меньше 4 яиц, то заканчиваем игру 
                if (  timeDaysWithoutSleep >= 8 || cornOfChicken[0] < 0 || cornOfChicken[1] < 0 || cornOfChicken[2] < 0 || ateEggs < 4 )
                   
                {
                   
                
                        Console.WriteLine($"\nВы успешно выживали на куриной ферме {day} дней, но к сожалению вы умерли");
                    break;
                }
            }

        }
    }
}
      