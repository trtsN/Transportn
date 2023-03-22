using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

 // транспортная задача
namespace Transportn
{
    public class Program
    {
        static int it; //для поиска клетки, куда надо поставить бесконечность
        static int const_pr; //константа приведения 
        static int[,] price; //cтоимости проезда
        static Dictionary<int, int> paths; //для результата

        static void Main()
        {
            //string[] in_txt = File.ReadAllLines("in.txt");//in.txt
            Console.Write("Введите название файла: ");
            string g = Console.ReadLine();

            if (g != "in.txt")
            
                Console.WriteLine("Неккоректное имя файла");
                return;
            }

            string[] in_txt = File.ReadAllLines(g);
            //константа приведения приводится к максимальному элементу, чтобы после найти наименьший путь
            const_pr = int.MaxValue;

            //сколько городов для посещения
            string n_old = in_txt[0];
            int n = int.Parse(n_old);

            price = new int[n, n];

            //читаем матрицу путей из файла
            for (int i = 0; i < n; i++)
            {
                var line = new int[n];
                line = in_txt[i + 1].Split(' ').Select(int.Parse).ToArray();

                for (int j = 0; j < n; j++)
                {
                    price[i, j] = line[j];

                    if (line[j] == -1) //а тут мы смотрим, где стоят в оригинале бесконечность
                    {
                        price[i, j] = int.MaxValue;
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((i == j) && (price[i, j] != int.MaxValue))
                    {
                        price[i, j] = int.MaxValue;
                    }
                }
            }

            //int[,] priceTest = new int[n, n];

            //priceTest[0, 0] = int.MaxValue;
            //priceTest[1, 1] = int.MaxValue;
            //priceTest[2, 2] = int.MaxValue;

            //priceTest[0, 1] = 1;
            //priceTest[0, 2] = 2;

            //priceTest[1, 0] = 1;
            //priceTest[1, 2] = 3;

            //priceTest[2, 0] = 2;
            //priceTest[2, 1] = 3;

            //price = priceTest;

            var prices = (int[,])price.Clone();
            it = 0;

            // алгоритм Литтла , метод ветвей и границ
            Little(prices, new Dictionary<int, int>(), 0);

            // результат
            Console.WriteLine($"Итоговая константа приведения (итоговая стоимость пути) = {const_pr}");

            int start = 0;

            Console.WriteLine($"Итоговый путь:");

            while (true)
            {
                Console.WriteLine($"{(start + 1)} {(paths[start] + 1)}");

                // переход в следующую вершину
                start = paths[start];

                // если вернулись в начало, то прекратить
                if (start == 0)
                {
                    break;
                }
            }

            //
            // Метод ветвей и границ
            // path - путь, H - нижняя граница
            //
            static void Little(int[,] prices, Dictionary<int, int> path, int H)
            {
                var final_points = new List<int>();
                int length_matrix = 0;

                for (int i = 0; i < prices.GetLength(0); i++)
                {
                    for (int j = 0; j < prices.GetLength(1); j++)
                    {
                        if (prices[i, j] != int.MaxValue)
                        {
                            length_matrix = length_matrix + 1;
                            final_points.Add(prices.GetLength(0) * i + j); // добавляем в финальные точки все точки
                                                                           // Console.WriteLine($"{i} + {j}");
                        }
                    }
                }

                // если матрица 2x2
                if (length_matrix == 2)
                {
                    var result = path;

                    // берем последний 2 пути и добавляем в результат
                    var row1 = final_points[0] / prices.GetLength(0);
                    var col1 = final_points[0] - prices.GetLength(0) * row1;

                    var row2 = final_points[1] / prices.GetLength(0);
                    var col2 = final_points[1] - prices.GetLength(0) * row2;

                    result.Add(row1, col1);
                    result.Add(row2, col2);

                    // сравнение пути с минимальным
                    prov(result, 0);

                    return;
                }

                // сумма всех вычтенных значений
                int minus_sum = 0;

                // массивы с минимальными элементами строк и столбцов
                var min_row = new int[prices.GetLength(0)];
                var min_column = new int[prices.GetLength(0)];

                for (int k = 0; k < prices.GetLength(0); k++)
                {
                    min_row[k] = int.MaxValue;
                    min_column[k] = int.MaxValue;
                }

                // обход всей матрицы
                for (int i = 0; i < prices.GetLength(0); i++)
                {
                    // поиск минимального элемента в строке (u)
                    for (int j = 0; j < prices.GetLength(0); j++)
                    {
                        if (prices[i, j] < min_row[i])
                        {
                            min_row[i] = prices[i, j];
                        }
                    }

                    for (int j = 0; j < prices.GetLength(0); ++j)
                    {
                        // вычитание минимальных элементов из всех элементов строки, кроме бесконечностей
                        if (prices[i, j] != int.MaxValue)
                        {
                            prices[i, j] -= min_row[i];
                        }

                        // поиск минимального элемента в столбце после вычитания строк (v)
                        if ((prices[i, j] < min_column[j]))
                        {
                            min_column[j] = prices[i, j];
                        }
                    }
                }

                // вычитание минимальных элементов из всех элементов столбца, кроме бесконечностей
                for (int j = 0; j < prices.GetLength(0); ++j)
                {
                    for (int i = 0; i < prices.GetLength(0); i++)
                    {
                        if (prices[i, j] != int.MaxValue)
                        {
                            prices[i, j] -= min_column[j];
                        }
                    }
                }

                // суммирование u значений
                foreach (var min in min_row)
                {
                    if (min != int.MaxValue)
                    {
                        minus_sum += min;
                    }
                }

                // суммирование v значений
                foreach (var min in min_column)
                {
                    if (min != int.MaxValue)
                    {
                        minus_sum += min;
                    }
                }

                // увеличение нижней границы
                H += minus_sum;

                // сравнение верхней и нижней границ (получившейся и старой)
                if (H >= const_pr)
                {
                    return;
                }

                //Metod zero = new Metod();
                //находим элемент с наибольшей нулевой степенью
                List<int> zeroes = Metod.best_zeroes(prices);

                if (zeroes.Count == 0)
                {
                    return;
                }

                // новая матрица
                int[,] new_matrix = (int[,])prices.Clone();

                int row = zeroes[0] / prices.GetLength(0);
                int col = zeroes[0] - prices.GetLength(0) * row;

                zeroes.RemoveAt(0);

                // из матрицы удаляются строка и столбец, соответствующие вершинам ребра
                for (int j = 0; j < prices.GetLength(0); j++)
                {
                    new_matrix[row, j] = int.MaxValue;
                }

                for (int i = 0; i < prices.GetLength(0); i++)
                {
                    new_matrix[i, col] = int.MaxValue;
                }

                it++;

                // не допускается образование цикла
                // массивы с информацией о том, в каких столбцах и строках содержится бесконечность
                var infRow = new int[prices.GetLength(0)];
                var infColumn = new int[prices.GetLength(0)];

                // обход всей матрицы, нахождение количества бесконечностей
                for (int i = 0; i < prices.GetLength(0); i++)
                {
                    for (int j = 0; j < prices.GetLength(0); j++)
                    {
                        if (new_matrix[i, j] == int.MaxValue)
                        {
                            infRow[i]++;
                            infColumn[j]++;
                        }
                    }
                }

                int r = 0; int c = 0;

                //Console.WriteLine($" iteration = {it}");//число смен табличек

                for (int k = 0; k < prices.GetLength(0); k++)
                {
                    //Console.WriteLine($" row = {infRow[k]}, Column = {infColumn[k]}");
                    //если 3.4, то в новую бесконечность 4.3
                    if (infRow[k] == it)
                    {
                        r = k;
                    }

                    if (infColumn[k] == it)
                    {
                        c = k;
                    }
                }
                //Console.WriteLine($" r = {r}, c = {c}");
                new_matrix[r, c] = int.MaxValue;

                //добавление в путь ребра
                Dictionary<int, int> new_path = new Dictionary<int, int>(path);
                new_path.Add(row, col);

                // обработка множества, содержащего ребро edge
                Little(new_matrix, new_path, H);

                // переход к множеству, не содержащему ребро edge
                // снова копирование матрицы текущего шага

                // кого скопировать, куда скопировать, сколько скопировать
                Array.Copy(prices, new_matrix, prices.Length);

                // добавление бесконечности на место ребра
                new_matrix[row, col] = int.MaxValue;

                it--;

                // обработка множества, не сожержащего ребро edge
                Little(new_matrix, path, H);
            }
            //
            // сравнение пути с минимальным
            //
        }
        public static bool prov(Dictionary<int, int> result, int current)
        {
            if (current == 0)
                current = GetCost(0, result, price);

            // сравнение рекорда со стоимостью текущего пути
            if (current > const_pr)
            {
                return false;
            }

            // сохранение стоимости и пути
            const_pr = current;
            paths = new Dictionary<int, int>(result);

            return true;
        }

        public static int GetCost(int cost, Dictionary<int, int> result, int[,] price)
        {
            //int cost = 0;

            foreach (var edge in result)
            {
                //Console.WriteLine(edge);
                cost += price[edge.Key, edge.Value];
            }

            return cost;
        }

    }

    public class Zeros
    {
        protected static int sum_place(int[,] price, int r, int c)
        {
            int row_min, column_min;
            row_min = int.MaxValue;
            column_min = int.MaxValue;

            // обход строки и столбца
            for (int i = 1; i < price.GetLength(0); i++)
            {
                if (i != r)
                {
                    row_min = Math.Min(row_min, price[i, c]);
                }

                if (i != c)
                {
                    column_min = Math.Min(column_min, price[r, i]);
                }
            }

            int result = row_min + column_min;

            return result;
        }
    }

    public class Metod : Zeros
    {

        // ищем степень нуля
        // мин по столбцу и строке ( за исключением самого элемента) суммируются
        // нулевые степени 
        // у каждого элемента = 0 находим его степень
        // и после выбираем самое большое значение
        public static List<int> best_zeroes(int[,] price)
        {
            List<int> zeroes;
            // список координат нулевых элементов
            zeroes = new List<int>();

            // список их коэффициентов
            List<int> pow_zeroes = new List<int>();

            // максимальный коэффициент
            double max_pow_zeroes = 0;

            // поиск нулевых элементов
            for (int i = 0; i < price.GetLength(0); ++i)
            {
                for (int j = 0; j < price.GetLength(0); ++j)
                {
                    // если равен нулю
                    if (price[i, j] == 0)
                    {
                        // добавление в список координат
                        zeroes.Add(price.GetLength(0) * i + j);

                        // расчет коэффициента и добавление в список
                        pow_zeroes.Add(Zeros.sum_place(price, i, j));

                        // сравнение с максимальным
                        max_pow_zeroes = Math.Max(max_pow_zeroes, pow_zeroes[pow_zeroes.Count - 1]);
                    }
                }
            }

            int k = 0;

            // выискиваем максимальный и удаляем ненужное
            while (k < pow_zeroes.Count)
            {
                if (pow_zeroes[k] != max_pow_zeroes)
                {
                    pow_zeroes.RemoveAt(k);
                    zeroes.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            return zeroes;
        }
    }
}

