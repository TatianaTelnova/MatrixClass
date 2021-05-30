using System;
using System.Collections.Generic;

namespace Pr2_Matrix
{
    class Matrix
    {
        private int rows;
        private int cols;
        private double[,] initdata;

        public Matrix(int nRows, int nCols)
        {
            rows = nRows;
            cols = nCols;
            initdata = new double[rows, cols];
        }
        public Matrix(double[,] initData)
        {
            initdata = initData;
            rows = initData.GetLength(0);
            cols = initData.GetLength(1);
        }
        public double this[int i, int j]
        {
            get { return initdata[i, j]; }
            set { initdata[i, j] = value; }
        }
        public int Rows
        {
            get { return rows; }
        }
        public int Columns
        {
            get { return cols; }
        }
       // размер квадратной матрицы 
        public int? Size
        {
            get 
            {
                if (cols == rows)
                    return rows;
                else
                    return null;
            }
        }
        // Является ли матрица квадратной
        public bool IsSquared
        {
            get
            {
                if (cols == rows)
                    return true;
                else
                    return false;
            }            
        }
        // Является ли матрица нулевой
        public bool IsEmpty 
        {
            get 
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (initdata[i, j] != 0)
                        {
                            return false;
                        }
                    }                    
                }
                return true;
            }
        }
       // Является ли матрица единичной
        public bool IsUnity 
        {
            get
            {
                if (IsSquared)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if ((i == j && initdata[i, j] != 1) || (i != j && initdata[i, j] != 0))
                                return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }
        // Является ли матрица диагональной
        public bool IsDiagonal 
        {
            get
            {
                if (IsSquared)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (i != j && initdata[i, j] != 0)
                                return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }
        // Является ли матрица симметричной
        public bool IsSymmetric 
        {
            get
            {
                if (IsSquared)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = i; j < cols; j++)
                        {
                            if (initdata[i, j] != initdata[j, i])
                                return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.Rows == m2.Rows && m1.Columns == m2.Columns)
                return Matrix.Add(m1, m2);
            else
                throw new Exception("Матрицы разной размерности. Невозможно выполить действие!");
        }        
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.Rows == m2.Rows && m1.Columns == m2.Columns)
                return Matrix.Sub(m1, m2);
            else
                throw new Exception("Матрицы разной размерности. Невозможно выполить действие!");
        }
        public static Matrix operator *(Matrix m1, double d)
        {
            return Matrix.MultNum(m1, d);
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns == m2.Rows)
                return Matrix.Mult(m1, m2);
            else
                throw new Exception("Число колонок первой матрицы не совпадает с числом строк второй матрицы. Невозможно выполить действие!");
        }
        public static Matrix Add(Matrix m1, Matrix m2)
        {
            Matrix resMatr = new Matrix(m1.Rows, m2.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    resMatr[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return resMatr;
        }
        public static Matrix Sub(Matrix m1, Matrix m2)
        {
            Matrix resMatr = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    resMatr[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return resMatr;
        }
        public static Matrix MultNum(Matrix m1, double d)
        {
            Matrix resMatr = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    resMatr[i, j] = m1[i, j] * d;
                }
            }
            return resMatr;
        }
        public static Matrix Mult(Matrix m1, Matrix m2)
        {
            Matrix resMatr = new Matrix(m1.Rows, m2.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Columns; j++)
                {
                    //resMatr[i, j] = 0;
                    for (int k = 0; k < m1.Columns; k++)
                    {
                        resMatr[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return resMatr;            
        }

        public static explicit operator Matrix(double[,] arr)
        {
            Matrix resMatr = new Matrix(arr);
            return resMatr;
        }
        public Matrix Transpose() 
        {
            Matrix resMatr = new Matrix(cols, rows);
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    resMatr[i, j] = initdata[j, i];
                }
            }
            return resMatr;
        }
        public double? Trace() 
        {
            // если квадратная
            if (IsSquared)
            {
                double res = 0;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = i; j < cols; j++)
                    {
                        if (i == j)
                            res += initdata[i, j];
                    }
                }
                return res;
            }
            return null;
        }
        public override string ToString() 
        {
            string res = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    res += initdata[i, j] + " ";
                }
                res = res.Substring(0, res.Length - 1);
                res += "; ";
            }
            res = res.Replace(',', '.');
            res = res.Replace(';', ',');
            return res.Substring(0, res.Length - 2);
        }
        // создание единичной матрицы
        public static Matrix GetUnity(int Size) 
        {
            Matrix resMatr = new Matrix(Size, Size);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i == j)
                        resMatr[i, j] = 1;
                    else
                        resMatr[i, j] = 0;
                }
            }
            return resMatr;

        }
        // создание нулевой матрицы
        public static Matrix GetEmpty(int Size) 
        {
            Matrix resMatr = new Matrix(Size, Size);
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    resMatr[i, j] = 0;
                }
            }
            return resMatr;
        }
        public static Matrix Parse(string s) 
        {
            string[] newstr1;
            string[] newstr2;
            double[,] values;

            newstr1 = s.Split(',');

            for (int i = 0; i < newstr1.Length - 1; i++)
            {
                if (newstr1[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != newstr1[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length)
                {
                    throw new FormatException("Неверный формат!");
                }
            }

            values = new double[newstr1.Length, newstr1[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length];
            for (int i = 0; i < newstr1.Length; i++)
            {
                newstr2 = newstr1[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < newstr2.Length; j++)
                {
                    try
                    {
                        values[i, j] = Convert.ToDouble(newstr2[j].Replace(".", ","));
                    }
                    catch (Exception)
                    {
                        throw new FormatException("Неверный формат!");
                    }
                }
            }
            Matrix resMatr = new Matrix(values);
            return resMatr;
        }
        public static bool TryParse(string s, out Matrix m) 
        {
            try
            {
                m = Parse(s);
                return true;
            }
            catch (Exception)
            {
                m = null;
                return false;
            }
        }
    }

    class Program
    {
        public static Dictionary<string, Matrix> allMatrix = new Dictionary<string, Matrix>();
        static void Main()
        {
            Console.Clear();
            Console.Write("Работа с матрицами\n" +
                          "-------------------------------\n" +
                          "    1 – Ввод матрицы\n" +
                          "    2 - Операции\n" +
                          "    3 – Вывод результатов\n" +
                          "    0 - Выход\n" +
                          "-------------------------------\n");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': InputMatrix(); break;
                    case '2': Operations(); break;
                    case '3': OutputMatrix(); break;
                    case '0': Environment.Exit(0); break;
                    default: break;
                }
            }
        }

        public static void InputMatrix()
        {
            Console.Clear();
            Console.Write("Ввод матрицы\n" +
                          "-------------------------------\n" +
                          "    1 - единичная матрица\n" +
                          "    2 - нулевая матрица\n" +
                          "    3 - произвольная матрица\n" +
                          "    0 - вернуться в главное меню\n" +
                          "-------------------------------\n");

            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': UnityMatrix(); break;
                    case '2': EmptyMatrix(); break;
                    case '3': OtherMatrix(); break;
                    case '0': Environment.Exit(0); break;
                    default: break;
                }
            }
        }

        public static void UnityMatrix()
        {
            Console.Clear();
            Console.Write("Единичная матрица\n" +
                          "-------------------------------\n");
            Console.Write("Название: ");
            string name = Console.ReadLine();
            while (allMatrix.ContainsKey(name))
            {
                Console.WriteLine("Имя уже занято. Введите новое имя");
                Console.Write("Название: ");
                name = Console.ReadLine();
            }
            Console.Write("Размер: ");
            int size = int.Parse(Console.ReadLine());
            while (size < 1)
            {
                Console.WriteLine("Размер должен быть большим нуля. Введите новое значение");
                Console.Write("Размер: ");
                size = int.Parse(Console.ReadLine());
            }
            Matrix mtrx = Matrix.GetUnity(size);
            allMatrix.Add(name, mtrx);
            Console.WriteLine("Матрица размера " + mtrx.Rows + "x" + mtrx.Columns + " создана");
            Console.WriteLine("-------------------------------\n" +
                              "Для перехода в главное меню нажмите 0");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': Main(); break;
                    default: break;
                }
            }
        }

        public static void EmptyMatrix()
        {
            Console.Clear();
            Console.Write("Нулевая матрица\n" +
                          "-------------------------------\n");
            Console.Write("Название: ");
            string name = Console.ReadLine();
            while (allMatrix.ContainsKey(name))
            {
                Console.WriteLine("Имя уже занято. Введите новое имя");
                Console.Write("Название: ");
                name = Console.ReadLine();
            }
            Console.Write("Размер: ");
            int size = int.Parse(Console.ReadLine());
            while (size < 1)
            {
                Console.WriteLine("Размер должен быть большим нуля. Введите новое значение");
                Console.Write("Размер: ");
                size = int.Parse(Console.ReadLine());
            }
            Matrix mtrx = Matrix.GetEmpty(size);
            allMatrix.Add(name, mtrx);
            Console.WriteLine("Матрица размера " + mtrx.Rows + "x" + mtrx.Columns + " создана");
            Console.WriteLine("-------------------------------\n" +
                              "Для перехода в главное меню нажмите 0");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': Main(); break;
                    default: break;
                }
            }
        }

        public static void OtherMatrix()
        {
            Console.Clear();
            Console.Write("Произвольная матрица\n" +
                          "-------------------------------\n");
            Console.Write("Название: ");
            string name = Console.ReadLine();
            while (allMatrix.ContainsKey(name))
            {
                Console.WriteLine("Имя уже занято. Введите новое имя");
                Console.Write("Название: ");
                name = Console.ReadLine();
            }
            Console.Write("Значения: ");
            string input = Console.ReadLine();
            Matrix mtrx = Matrix.Parse(input);
            allMatrix.Add(name, mtrx);
            Console.WriteLine("Матрица размера " + mtrx.Rows + "x" + mtrx.Columns + " создана");
            Console.WriteLine("-------------------------------\n" +
                              "Для перехода в главное меню нажмите 0");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': Main(); break;
                    default: break;
                }
            }
        }        

        public static void Operations()
        {
            Console.Clear();
            Console.WriteLine("Операции\n" +
                              "-------------------------------\n" +
                              "    1 - сложение матриц\n" +
                              "    2 - вычитание матриц\n" +
                              "    3 - умножение на число\n" +
                              "    4 - умножение на матрицу\n" +
                              "    0 - вернуться в главное меню\n" +
                              "-------------------------------");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': AddMatrix(); break;
                    case '2': SubMatrix(); break;
                    case '3': MultNumMatrix(); break;
                    case '4': MultMatrix(); break;
                    case '0': Main(); break;
                    default: break;
                }
            }
        }

        public static void AddMatrix()
        {
            Console.Clear();
            Console.Write("Сложение матриц\n" +
                          "-------------------------------\n");
            if (allMatrix.Count < 2)
            {
                Console.WriteLine("Недостаточно матриц для выполнения операции");
            }
            else
            {
                Console.WriteLine("Введите первую матрицу");
                Console.Write("Название: ");
                string name = Console.ReadLine();
                while (!allMatrix.ContainsKey(name))
                {
                    Console.WriteLine("Матрицы не существует. Введите другое имя");
                    Console.Write("Название: ");
                    name = Console.ReadLine();
                }
                Matrix mtrx1 = allMatrix[name];
                Console.WriteLine("Введите вторую матрицу");
                Console.Write("Название: ");
                name = Console.ReadLine();
                while (!allMatrix.ContainsKey(name))
                {
                    Console.WriteLine("Матрицы не существует. Введите другое имя");
                    Console.Write("Название: ");
                    name = Console.ReadLine();
                }
                Matrix mtrx2 = allMatrix[name];
                Matrix resMatr = mtrx1 + mtrx2;
                Console.Write("Введите название матрицы-результата: ");
                string resName = Console.ReadLine();
                while (allMatrix.ContainsKey(resName))
                {
                    Console.WriteLine("Имя уже занято. Введите новое имя");
                    Console.Write("Название: ");
                    resName = Console.ReadLine();
                }
                allMatrix.Add(resName, resMatr);
                Console.WriteLine("-------------------------------\n" +
                              "Результат:\n" + 
                              resName + " =");
                for (int i = 0; i < resMatr.Rows; i++)
                {
                    for (int j = 0; j < resMatr.Columns; j++)
                    {
                        Console.Write(resMatr[i, j].ToString().Replace(",", ".") + " ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("-------------------------------\n" +
                              "Для перехода в главное меню нажмите 0");
        }
        public static void SubMatrix()
        {
            Console.Clear();
            Console.Write("Вычитание матриц\n" +
                          "-------------------------------\n");
            if (allMatrix.Count < 2)
            {
                Console.WriteLine("Недостаточно матриц для выполнения операции");
            }
            else
            {
                Console.WriteLine("Введите первую матрицу");
                Console.Write("Название: ");
                string name = Console.ReadLine();
                while (!allMatrix.ContainsKey(name))
                {
                    Console.WriteLine("Матрицы не существует. Введите другое имя");
                    Console.Write("Название: ");
                    name = Console.ReadLine();
                }
                Matrix mtrx1 = allMatrix[name];
                Console.WriteLine("Введите вторую матрицу");
                Console.Write("Название: ");
                name = Console.ReadLine();
                while (!allMatrix.ContainsKey(name))
                {
                    Console.WriteLine("Матрицы не существует. Введите другое имя");
                    Console.Write("Название: ");
                    name = Console.ReadLine();
                }
                Matrix mtrx2 = allMatrix[name];
                Matrix resMatr = mtrx1 - mtrx2;
                Console.Write("Введите название матрицы-результата: ");
                string resName = Console.ReadLine();
                while (allMatrix.ContainsKey(resName))
                {
                    Console.WriteLine("Имя уже занято. Введите новое имя");
                    Console.Write("Название: ");
                    resName = Console.ReadLine();
                }
                allMatrix.Add(resName, resMatr);
                Console.WriteLine("-------------------------------\n" +
                              "Результат:\n" +
                              resName + " =");
                for (int i = 0; i < resMatr.Rows; i++)
                {
                    for (int j = 0; j < resMatr.Columns; j++)
                    {
                        Console.Write(resMatr[i, j].ToString().Replace(",", ".") + " ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("-------------------------------\n" +
                              "Для перехода в главное меню нажмите 0");
        }
        public static void MultNumMatrix()
        {
            Console.Clear();
            Console.Write("Умножение на число\n" +
                          "-------------------------------\n");
            if (allMatrix.Count < 1)
            {
                Console.WriteLine("Недостаточно матриц для выполнения операции");
            }
            else
            {
                Console.WriteLine("Введите матрицу");
                Console.Write("Название: ");
                string name = Console.ReadLine();
                while (!allMatrix.ContainsKey(name))
                {
                    Console.WriteLine("Матрицы не существует. Введите другое имя");
                    Console.Write("Название: ");
                    name = Console.ReadLine();
                }
                Matrix mtrx1 = allMatrix[name];
                Console.Write("Введите число: ");
                double d = double.Parse(Console.ReadLine());
                Matrix resMatr = mtrx1 * d;
                Console.Write("Введите название матрицы-результата: ");
                string resName = Console.ReadLine();
                while (allMatrix.ContainsKey(resName))
                {
                    Console.WriteLine("Имя уже занято. Введите новое имя");
                    Console.Write("Название: ");
                    resName = Console.ReadLine();
                }
                allMatrix.Add(resName, resMatr);
                Console.WriteLine("-------------------------------\n" +
                              "Результат:\n" +
                              resName + " =");
                for (int i = 0; i < resMatr.Rows; i++)
                {
                    for (int j = 0; j < resMatr.Columns; j++)
                    {
                        Console.Write(resMatr[i, j].ToString().Replace(",", ".") + " ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("-------------------------------\n" +
                              "Для перехода в главное меню нажмите 0");
        }
        public static void MultMatrix()
        {
            Console.Clear();
            Console.Write("Умножение на матрицу\n" +
                          "-------------------------------\n");
            if (allMatrix.Count < 2)
            {
                Console.WriteLine("Недостаточно матриц для выполнения операции");
            }
            else
            {
                Console.WriteLine("Введите первую матрицу");
                Console.Write("Название: ");
                string name = Console.ReadLine();
                while (!allMatrix.ContainsKey(name))
                {
                    Console.WriteLine("Матрицы не существует. Введите другое имя");
                    Console.Write("Название: ");
                    name = Console.ReadLine();
                }
                Matrix mtrx1 = allMatrix[name];
                Console.WriteLine("Введите вторую матрицу");
                Console.Write("Название: ");
                name = Console.ReadLine();
                while (!allMatrix.ContainsKey(name))
                {
                    Console.WriteLine("Матрицы не существует. Введите другое имя");
                    Console.Write("Название: ");
                    name = Console.ReadLine();
                }
                Matrix mtrx2 = allMatrix[name];
                Matrix resMatr = mtrx1 * mtrx2;
                Console.Write("Введите название матрицы-результата: ");
                string resName = Console.ReadLine();
                while (allMatrix.ContainsKey(resName))
                {
                    Console.WriteLine("Имя уже занято. Введите новое имя");
                    Console.Write("Название: ");
                    resName = Console.ReadLine();
                }
                allMatrix.Add(resName, resMatr);
                Console.WriteLine("-------------------------------\n" +
                              "Результат:\n" +
                              resName + " =");
                for (int i = 0; i < resMatr.Rows; i++)
                {
                    for (int j = 0; j < resMatr.Columns; j++)
                    {
                        Console.Write(resMatr[i, j].ToString().Replace(",", ".") + " ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("-------------------------------\n" +
                              "Для перехода в главное меню нажмите 0");
        }

        public static void OutputMatrix()
        {
            Console.Clear();
            Console.Write("Вывод результатов\n" +
                          "-------------------------------\n" +
                          "    1 - все матрицы\n" +
                          "    2 - свойства матрицы\n" +
                          "    0 - вернуться в главное меню\n" +
                          "-------------------------------\n");                          
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': PrintAllMatrix(); break;
                    case '2': Properties(); break;                    
                    case '0': Main(); break;
                    default: break;
                }
            }
        }

        public static void PrintAllMatrix()
        {
            Console.Clear();
            Console.Write("Все матрицы\n" +
                          "-------------------------------\n");
            if (allMatrix.Count < 1)
            {
                Console.WriteLine("Недостаточно матриц для выполнения операции");
            }
            else
            {
                foreach (KeyValuePair<string, Matrix> m in allMatrix)
                {
                    Console.WriteLine(m.Key + " =");
                    for (int i = 0; i < m.Value.Rows; i++)
                    {
                        for (int j = 0; j < m.Value.Columns; j++)
                        {
                            Console.Write(m.Value[i, j].ToString().Replace(",", ".") + " ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }                
            Console.WriteLine("-------------------------------\n" +
                              "Для перехода в главное меню нажмите 0");
        }

        public static void Properties()
        {
            Console.Clear();
            Console.Write("Свойства матрицы\n" +                              
                          "-------------------------------\n");
            if (allMatrix.Count < 1)
            {
                Console.WriteLine("Недостаточно матриц для выполнения операции");
            }
            else
            {
                Console.Write("Название: ");
                string name = Console.ReadLine();
                while (!allMatrix.ContainsKey(name))
                {
                    Console.WriteLine("Матрицы не существует. Введите другое имя");
                    Console.Write("Название: ");
                    name = Console.ReadLine();
                }
                Matrix mtrx = allMatrix[name];

                Console.Write("-------------------------------\n" +
                              "Значения:\n");
                for (int i = 0; i < mtrx.Rows; i++)
                {
                    for (int j = 0; j < mtrx.Columns; j++)
                    {
                        Console.Write(mtrx[i, j].ToString().Replace(",", ".") + " ");
                    }
                    Console.WriteLine();
                }
                Console.Write("-------------------------------\n" +
                              "Квадратная: " + mtrx.IsSquared + "\n");
                if (mtrx.IsSquared)
                {
                    Console.Write("Размер: " + mtrx.Size + "x" + mtrx.Size + "\n" +
                                  "След: " + mtrx.Trace() + "\n");
                }
                Console.Write("Нулевая: " + mtrx.IsEmpty + "\n" +
                              "Единичная: " + mtrx.IsUnity + "\n" +
                              "Диагональная: " + mtrx.IsDiagonal + "\n" +
                              "Симметричная: " + mtrx.IsSymmetric + "\n" +
                              "Строка: " + mtrx.ToString() + "\n" +
                              "Транспонированная матрица:\n");
                Matrix resMatr = mtrx.Transpose();
                for (int i = 0; i < resMatr.Rows; i++)
                {
                    for (int j = 0; j < resMatr.Columns; j++)
                    {
                        Console.Write(resMatr[i, j].ToString().Replace(",", ".") + " ");
                    }
                    Console.WriteLine();
                }
            }                
            Console.WriteLine("-------------------------------\n" +
                              "Для перехода в главное меню нажмите 0");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': Main(); break;
                    default: break;
                }
            }
        }
    }
}
