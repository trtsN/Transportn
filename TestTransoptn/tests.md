# План тестирования Transportn
Программа, решающая транспортную задачу для матрицы 3 на 3.
## Модульные тесты: 
### Тест 1:
Входные данные: матрица 3 на 3 с значениями int.MaxValue, 1, 2, int.MaxValue, 1, 3, int.MaxValue, 2, 3.
Ожидаемый результат: истина
public void GetCostPositive()
        {

            Dictionary<int, int> resultTest = new Dictionary<int, int>()
            {
                [0] = 1,
                [1] = 2,
                [2] = 0
            };

            int n = 3;
            int[,] priceTest = new int[n, n];

            priceTest[0, 0] = int.MaxValue;
            priceTest[1, 1] = int.MaxValue;
            priceTest[2, 2] = int.MaxValue;

            priceTest[0, 1] = 1;
            priceTest[0, 2] = 2;

            priceTest[1, 0] = 1;
            priceTest[1, 2] = 3;

            priceTest[2, 0] = 2;
            priceTest[2, 1] = 3;

            int test = 6;
            int result = Program.GetCost(0, resultTest, priceTest);

            bool f = (test == result);

            Assert.IsTrue(f);
        }

### Тест 2:
Входные данные: матрица 3 на 3 с значениями int.MaxValue, 1, 2, int.MaxValue, 1, 3, int.MaxValue, 2, 3.
Ожидаемый результат: не равные
public void GetCostNegative()
        {

            Dictionary<int, int> resultTest = new Dictionary<int, int>()
            {
                [0] = 1,
                [1] = 2,
                [2] = 0
            };

            int n = 3;
            int[,] priceTest = new int[n, n];

            priceTest[0, 0] = int.MaxValue;
            priceTest[1, 1] = int.MaxValue;
            priceTest[2, 2] = int.MaxValue;

            priceTest[0, 1] = 1;
            priceTest[0, 2] = 2;

            priceTest[1, 0] = 1;
            priceTest[1, 2] = 3;

            priceTest[2, 0] = 2;
            priceTest[2, 1] = 3;

            int test = 61;
            int result = Program.GetCost(0, resultTest, priceTest);
            Assert.AreNotEqual(test, result);
        }
## Интеграционные тесты:
### Тест 1:
Входные данные: матрица 3 на 3 с значениями int.MaxValue, 1, 2, int.MaxValue, 1, 3, int.MaxValue, 2, 3.
Ожидаемый результат: 5
    public void BestZeroesPositiveTest()
        {
            int n = 3;
            int[,] priceTest = new int[n, n];

            priceTest[0, 0] = int.MaxValue;
            priceTest[1, 1] = int.MaxValue;
            priceTest[2, 2] = int.MaxValue;

            priceTest[0, 1] = 1;
            priceTest[0, 2] = 2;

            priceTest[1, 0] = 1;
            priceTest[1, 2] = 3;

            priceTest[2, 0] = 2;
            priceTest[2, 1] = 3;

            int i = 5;
            int j = sum_place(priceTest, 0, 1);

            Assert.AreEqual(i, j);
        }
### Тест 2:
Входные данные: матрица 3 на 3 с значениями int.MaxValue, 1, 2, int.MaxValue, 1, 3, int.MaxValue, 2, 3.
Ожидаемый результат: сумма по матрице
public void BestZeroesNegativeTest()
        {
            int n = 3;
            int[,] priceTest = new int[n, n];

            priceTest[0, 0] = int.MaxValue;
            priceTest[1, 1] = int.MaxValue;
            priceTest[2, 2] = int.MaxValue;

            priceTest[0, 1] = 1;
            priceTest[0, 2] = 2;

            priceTest[1, 0] = 1;
            priceTest[1, 2] = 3;

            priceTest[2, 0] = 2;
            priceTest[2, 1] = 3;

            int i = 1;
            int j = sum_place(priceTest, 0, 1);

            Assert.AreNotEqual(i, j);
        }
