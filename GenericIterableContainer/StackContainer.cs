using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericIterableContainer
{
    // Базовое исключение для контейнера
    public class ContainerException : Exception
    {
        public ContainerException(string message) : base(message)
        {
        }
    }

    // Исключение, связанное с объемом контейнера
    public class ContainerVolumeException : ContainerException
    {
        public ContainerVolumeException(string message) : base(message)
        {
        }
    }

    // Исключение при переполнении контейнера
    public class ContainerOverflowException : ContainerVolumeException
    {
        public ContainerOverflowException(string message) : base(message)
        {
        }
    }

    // Исключение при недостатке элементов в контейнере
    public class ContainerUnderflowException : ContainerVolumeException
    {
        public ContainerUnderflowException(string message) : base(message)
        {
        }
    }

    // Исключение при некорректном индексе
    public class InvalidIndexException : ContainerException
    {
        public InvalidIndexException(string message) : base(message)
        {
        }
    }

    // Обобщенный контейнерный класс, реализующий интерфейс IEnumerable
    // и ограниченный типом T, который должен реализовывать интерфейс IComparable<T>
    //Этот класс реализует интерфейс IEnumerable<T>, что позволяет использовать цикл foreach для обхода элементов контейнера.
    //Ограничение where T : IComparable<T> указывает, что тип T должен реализовывать интерфейс IComparable<T>,
    //что необходимо для использования метода сортировки.
    public class StackContainer<T> : IEnumerable<T> where T : IComparable<T>
    {
        private List<T> items;  // Хранение элементов контейнера
        private int capacity;   // Максимальная вместимость контейнера

        // Конструктор контейнера
        public StackContainer(int capacity = 10)
        {
            if (capacity <= 0)
            {
                // Проверка на корректность входных параметров
                throw new ArgumentException("Объем должен быть больше нуля.");
            }

            this.capacity = capacity;
            this.items = new List<T>(capacity);
        }

        // Метод для добавления элемента в контейнер (положить на вершину стека)
        public void Push(T item)
        {
            if (items.Count == capacity)
            {
                // Проверка на переполнение контейнера
                throw new ContainerOverflowException("Стек полон. Невозможно добавить больше элементов.");
            }

            items.Add(item);
        }

        // Метод для извлечения элемента из контейнера (взять с вершины стека)
        public T Pop()
        {
            if (items.Count == 0)
            {
                // Проверка на недостаток элементов в контейнере
                throw new ContainerUnderflowException("Стек пуст. Невозможно извлечь элементы.");
            }

            // Извлекаем элемент с вершины стека
            T poppedItem = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return poppedItem;
        }

        // Индексатор для доступа к элементу по индексу
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= items.Count)
                {
                    // Проверка на корректность индекса
                    throw new InvalidIndexException("Некорректный индекс.");
                }

                return items[index];
            }
        }

        // Обновленный метод для сортировки элементов контейнера (сортировка пузырьком)
        public void Sort()
        {
            // Внешний цикл проходит по всем элементам, кроме последнего
            for (int i = 0; i < items.Count - 1; i++)
            {
                // Внутренний цикл также проходит по элементам, но уже на один элемент меньше на каждой итерации внешнего цикла
                for (int j = 0; j < items.Count - 1 - i; j++)
                {
                    // Сравниваем текущий элемент соседний и, если текущий больше, меняем их местами
                    if (items[j].CompareTo(items[j + 1]) > 0)
                    {
                        // Обмен элементов, если они стоят в неправильном порядке
                        T temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
        }

        // Реализация интерфейса IEnumerable для использования цикла foreach
        public IEnumerator<T> GetEnumerator()
        {
            // Возвращаем Enumerator для типа T, который позволяет перебирать элементы
            // в коллекции типа List<T> (или любой другой коллекции, которая поддерживает IEnumerable<T>)
            return items.GetEnumerator();
        }

        // Реализация необобщенного интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            // Возвращаем необобщенный Enumerator, который вызывает обобщенную реализацию метода GetEnumerator()
            // для обеспечения совместимости с необобщенным циклом foreach
            return GetEnumerator();
        }
    }
}

/*
Добавить к обобщенному контейнерному классу ограничение типа, чтобы допускать только объекты классов, 
в которых определена операция сравнения.
Продемонстрировать работу ограничения, определив различные классы объектов-элементов контейнера и 
реализовав метод сортировки элементов.
Таким образом, разница заключается в том, что в задании 6 требуется расширить ваш обобщенный контейнерный класс, 
ндобавив ограничение типа и метод сортировки, чтобы демонстрировать работу сравнения элементов.
 */