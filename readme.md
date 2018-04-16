# 05 reflection

## общее

Наша задача &mdash; разработать IoC-контейнер
(следуя принципу
«Каждый программист должен разработать свой IoC/DI контейнер» &copy;).

В качестве примера мы возьмем _Managed Extensibility Framework (MEF)_,
в котором основная настройка контейнера
происходит за счёт расстановки атрибутов.

Но (!) весь код, включая объявление атрибутов, у нас будет свой.

## 01

Используя механизмы Reflection, создайте простейший IoC-контейнер,
который позволяет следующее:

* Разметить классы, требующие внедрения зависимостей,
    одним из следующих способов:

    * Через конструктор
        (тогда класс размечается атрибутом `[ImportConstructor]`):

        ```cs
        [ImportConstructor]
        public class CustomerBLL
        {
            public CustomerBLL(ICustomerDAL dal, Logger logger)
            { }
        }
        ```

    * Через публичные свойства
        (тогда каждое свойство, требующее инициализации,
        размечается атрибутом `[Import]`):

        ```cs
        public class CustomerBLL
        {
            [Import]
            public ICustomerDAL CustomerDAL { get; set; }

            [Import]
            public Logger logger { get; set; }
        }
        ```

        При этом конкретный класс, понятное дело,
        размечается только одним способом!

* Разметить зависимые классы:

    * Когда класс используется непосредственно:

        ```cs
        [Export]
        public class Logger
        { }
        ```

    * Когда в классах, требующих реализации зависимости,
        используется интерфейс или базовый класс:

        ```cs
        [Export(typeof(ICustomerDAL))]
        public class CustomerDAL : ICustomerDAL
        { }
        ```

* Явно указать классы, которые зависят от других
    или требуют внедрения зависимостей:

    ```cs
    var container = new Container();

    container.AddType(typeof(CustomerBLL));
    container.AddType(typeof(Logger));
    container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));
    ```

* Добавить в контейнер все классы,
    размеченные атрибутами `[ImportConstructor]`, `[Import]` и `[Export]`,
    указав сборку:

    ```cs
    var container = new Container();

    container.AddAssembly(Assembly.GetExecutingAssembly());
    ```

* Получить экземпляр ранее зарегистрированного класса со всеми зависимостями:

    ```cs
    var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));

    var customerBLL = container.CreateInstance<CustomerBLL>();
    ```

## 02 (non-obligatory)

Доработайте контейнер из [задания 1](#01) так,
чтобы для создания экземпляра использовался сгенерированный код
на основе механизм `System.Reflection.Emit`.