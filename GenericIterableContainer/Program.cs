using GenericIterableContainer;

namespace GenericIterableContainer
{
    class Program
    {
        static void Main()
        {
            try
            {
                // Создаем экземпляр стекового контейнера для целых чисел с максимальной вместимостью 5
                StackContainer<int> stack = new StackContainer<int>(5);

                // Добавляем элементы в стек
                stack.Push(3);
                stack.Push(1);
                stack.Push(2);

                // Сортируем элементы стека
                stack.Sort();

                // Используем цикл foreach для перебора и вывода элементов стека
                foreach (int element in stack)
                {
                    Console.WriteLine(element);
                }
            }
            catch (ContainerException ex)
            {
                // Выводим сообщение об исключении контейнера
                Console.WriteLine("Исключение контейнера: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Выводим сообщение об неожиданном исключении
                Console.WriteLine("Неожиданное исключение: " + ex.Message);
            }
        }
    }
}