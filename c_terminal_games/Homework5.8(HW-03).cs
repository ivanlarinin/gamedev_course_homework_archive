// A homework for the Skillfactory course. A fighting game utilising If/Else/While/Switch logic.

using System;

namespace ConsoleFightingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Объявление переменных
            int playerHealth = 100;
            int playerEnergy = 50;
            int enemyHealth = 120;
            int enemyEnergy = 60;

            Console.WriteLine("Добро пожаловать в консольный файтинг!");
            Console.WriteLine("Твоя задача - победить Вирус!");
            Console.WriteLine();

            while (true)
            {
                // Отображение статов и скиллов
                Console.Clear();
                Console.WriteLine($"Жизни: {playerHealth}        Жизни вируса: {enemyHealth}");
                Console.WriteLine($"Энергия: {playerEnergy}        Энергия вируса: {enemyEnergy}");
                Console.WriteLine();
                Console.WriteLine("Твои действия:");
                Console.WriteLine("1. Почистить папку Temp (20 урона, -10 энергии)");
                Console.WriteLine("2. Использовать Касперского (30 урона, -40 энергии)");
                Console.WriteLine("3. Выпить кофе (+20 энергии)");
                Console.WriteLine("4. Заказать доставку пиццы (+30 жизни, -20 энергии)");
                Console.WriteLine();

                // Определение победы или поражения
                if (playerHealth <= 0)
                {
                    Console.WriteLine("Вирус выиграл! Ты проиграл.");
                    break;
                }

                if (enemyHealth <= 0)
                {
                    Console.WriteLine("Ты выиграл! Вирус повержен!");
                    break;
                }

                // Получение действия от игрока
                Console.Write("Выбери действие (1-4): ");
                string playerInput = Console.ReadLine();
                int action;
                while (!int.TryParse(playerInput, out action) || action < 1 || action > 4)
                {
                    Console.Write("Неверный ввод. Выбери действие (1-4): ");
                    playerInput = Console.ReadLine();
                }

                // Описание работы скиллов игрока
                switch (action)
                {
                    case 1: // Почистить папку Temp
                        if (playerEnergy >= 10)
                        {
                            enemyHealth -= 20;
                            playerEnergy -= 10;
                            Console.WriteLine("Ты почистил папку Temp и нанес 20 урона Вирусу!");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно энергии. Ты пропустил этот ход!");
                        }
                        break;
                    case 2: // Использовать Касперского
                        if (playerEnergy >= 40)
                        {
                            enemyHealth -= 30;
                            playerEnergy -= 40;
                            Console.WriteLine("Ты использовал Касперского и нанес 30 урона Вирусу!");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно энергии. Ты пропустил этот ход!");
                        }
                        break;
                    case 3: // Выпить кофе
                        playerEnergy += 20;
                        Console.WriteLine("Ты выпил кофе и восстановил 20 энергии!");
                        break;
                    case 4: // Заказать доставку пиццы
                        if (playerEnergy >= 20)
                        {
                            playerHealth += 30;
                            playerEnergy -= 20;
                            Console.WriteLine("Ты заказал пиццу и восстановил 30 здоровья!");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно энергии для заказа пиццы. Ты пропустил этот ход!");
                        }
                        break;
                }

                // Задержка для чтения сообщения игроком
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();

                // Получение действия от противника (простая логика)
                Random rnd = new Random();
                int enemyAction = rnd.Next(1, 3); // 1 - атака, 2 - восстановление энергии

                // Описание работы скиллов противника
                switch (enemyAction)
                {
                    case 1: // Атака
                        int damageDealt = 0;
                        if (enemyEnergy >= 15) // Простая атака
                        {
                            damageDealt = 20;
                            playerHealth -= damageDealt;
                            enemyEnergy -= 15;
                            Console.WriteLine($"Вирус атаковал и нанес тебе {damageDealt} урона!");
                        }
                        else if (enemyEnergy >= 5) // Слабая атака
                        {
                            damageDealt = 10;
                            playerHealth -= damageDealt;
                            enemyEnergy -= 5;
                            Console.WriteLine($"Вирус провел слабую атаку и нанес тебе {damageDealt} урона!");
                        }
                        else
                        {
                            Console.WriteLine("Вирус отдыхает и ничего не делает.");
                        }
                        break;
                    case 2: // Восстановление энергии
                        enemyEnergy += 25;
                        Console.WriteLine("Вирус восстановил 25 энергии.");
                        break;
                }

                // Задержка для чтения сообщения игроком
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
            }

            Console.WriteLine("Игра окончена.");
            Console.ReadKey();
        }
    }
}
