namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Project> projects = new List<Project>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню!");
                Console.WriteLine("1. Создать проект");
                Console.WriteLine("2. Удалить проект");
                Console.WriteLine("3. Посмотреть проекты");
                Console.WriteLine("6. Выход");

                var k = int.Parse(Console.ReadLine());

                if (k == 6)
                    break;

                switch (k)
                {
                    case 1:
                        bool validKey;
                        do
                        {
                            Console.Write("Введите ключ проекта: ");
                            string projectKey = Console.ReadLine();

                            validKey = true;

                            // Проверка на уникальность ключа
                            if (projects.Any(p => p.Key == projectKey))
                            {
                                Console.WriteLine("Проект с таким ключом уже существует. Пожалуйста, введите другой ключ.");
                                validKey = false;
                            }
                            else
                            {
                                Console.Write("Напишите бюджет проекта: ");
                                decimal budget = decimal.Parse(Console.ReadLine());

                                Console.Write("Напишите название проекта: ");
                                string projectTitle = Console.ReadLine();

                                var newProject = new Project(projectKey, budget, projectTitle);
                                projects.Add(newProject);

                                Console.WriteLine("Вы создали проект!");
                            }
                        } while (!validKey);
                        break;
                    case 2:
                        if (projects.Count == 0)
                        {
                            Console.WriteLine("Нет созданных проектов.");
                        }
                        else
                        {
                            Console.Write("Введите ключ проекта для удаления: ");
                            string keyToRemove = Console.ReadLine();

                            bool found = false;
                            foreach (var project in projects)
                            {
                                if (project.Key == keyToRemove)
                                {
                                    projects.Remove(project);
                                    Console.WriteLine("Проект удален.");
                                    found = true;
                                    break;
                                }
                            }

                            if (!found)
                                Console.WriteLine("Проект с таким ключом не найден.");
                        }
                        break;
                    case 3:
                        if (projects.Count == 0)
                        {
                            Console.WriteLine("Нет созданных проектов.");
                        }
                        else
                        {
                            int i = 1;
                            foreach (var project in projects)
                            {
                                Console.WriteLine($"{i}. {project.Key} - {project.Title}");
                                i++;
                            }


                            Console.Write("Введите номер проекта для просмотра деталей: ");
                            int choice;
                            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= projects.Count)
                            {
                                Console.WriteLine(projects[choice - 1]);
                                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Неверный ввод.");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Я вас не понимаю");
                        break;
                }
            }

            Console.ReadKey();

        }
    }

    public class Project
    {
        public string Key { get; set; }
        public decimal InitialCost { get; set; }
        public string Title { get; set; }

        public Project(string key, decimal initialCost, string title)
        {
            Key = key;
            InitialCost = initialCost;
            Title = title;
        }

        public override string ToString()
        {
            return $"Ключ проекта: {Key}\nБюджет проекта: {InitialCost}\nНазвание проекта: {Title}";
        }
    }


    public class Task
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public double HoursSpent { get; set; }
        public bool Billable { get; set; }

        public Task(int number, string description, DateTime dueDate, double hoursSpent, bool billable)
        {
            Number = number;
            Description = description;
            DueDate = dueDate;
            HoursSpent = hoursSpent;
            Billable = billable;
        }

        public override string ToString()
        {
            return $"Номер задачи: {Number}\nОписание: {Description}\nСрок исполнения: {DueDate:d}\nДата завершения: {(CloseDate.HasValue ? CloseDate.Value.ToShortDateString() : "Не завершена")}\nЗатраченное время (часы): {HoursSpent}\nОтдельно оплачивается заказчиком: {(Billable ? "Да" : "Нет")}";
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public Customer(string name, string contactPerson, string contactPhone, string contactEmail)
        {
            Name = name;
            ContactPerson = contactPerson;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
        }

        public override string ToString()
        {
            return $"Наименование клиента: {Name}\nКонтактное лицо: {ContactPerson}\nКонтактный телефон: {ContactPhone}\nКонтактный email: {ContactEmail}";
        }
    }

    public class Employee
    {
        public int Number { get; set; }
        public string FullName { get; set; }
        public int Rating { get; set; }

        public Employee(int number, string fullName, int rating)
        {
            Number = number;
            FullName = fullName;
            Rating = ValidateRating(rating);
        }

        private int ValidateRating(int rating)
        {
            return Math.Clamp(rating, 1, 5);
        }

        public override string ToString()
        {
            return $"Табельный номер: {Number}\nФ.И.О.: {FullName}\nРазряд: {Rating}";
        }
    }

    public class Position
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal BaseHourlyRate { get; set; }


        public Position(string code, string name, decimal baseHourlyRate)
        {
            Code = code;
            Name = name;
            BaseHourlyRate = baseHourlyRate;
        }

        public decimal CalculateHourlyRate(int rating)
        {
            decimal ratingBonus = (rating - 1) * 0.05m * BaseHourlyRate;
            return BaseHourlyRate + ratingBonus;
        }

        public override string ToString()
        {
            return $"Шифр: {Code}\nНазвание: {Name}\nБазовая почасовая ставка: {BaseHourlyRate:C}";
        }
    }

}


