using System;
using System.IO;

namespace RenamingTheFiles
{
    class Program
    {
        // Алфавит, из которого составляются названия файлов
        private const string Alphabet = "zxcvbnmasdfghjklqwertyuiop1234567890ZXCVBNMASDFGHJKLQWERTTYUIOP";
        // Длина новых имен файлов
        private const int FilenameLength = 12;
        // Название результирующей папки
        private const string ResultDir = "\\result";
        // Рандомизатор
        private static readonly Random Rnd = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Нажать Enter чтобы начать.");
            Console.ReadLine();

            var currentPath = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentPath, "*", SearchOption.AllDirectories);
            Directory.CreateDirectory(currentPath + ResultDir);
            
            var currentExeFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            currentExeFileName = currentExeFileName.Substring(currentExeFileName.LastIndexOf("\\") + 1);

            foreach (var file in files)
            {
                var fileName = file.Substring(file.LastIndexOf("\\") + 1);
                // Пропускаем переименовывание и копирование в папку с результатом выполняемого файла
                if (fileName.Equals(currentExeFileName)) { continue; }
                var resultPath = currentPath + ResultDir;
                var finalName = MakeName(FilenameLength);
                var fileExtension = "";
                var lastIndexOfPoint = file.LastIndexOf(".");
                if (lastIndexOfPoint != -1)
                {
                    fileExtension = file.Substring(lastIndexOfPoint);
                }
                while (File.Exists(finalName))
                {
                    Console.WriteLine("Обнаружен повтор. Генерирую другое имя.");
                    finalName = MakeName(FilenameLength);
                }
                File.Copy(file, resultPath + "\\" + MakeName(FilenameLength) + fileExtension);
            }

            Console.WriteLine("Готово.");
            Console.ReadLine();
        }

        private static string MakeName(int length)
        {
            var name = "";
            for (var i = 0; i < length; i++)
            {
                name += Alphabet[Rnd.Next(Alphabet.Length - 1)];
            }
            return name;
        }
    }
}
