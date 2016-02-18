﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace WPF_Unleashed._2_WPFApplication_Creation._6_InputEvents
{
    /// <summary>
    /// Interaction logic for InputEvents.xaml
    /// </summary>
    public partial class InputEvents : Window
    {
        public InputEvents()
        {
            InitializeComponent();
        }

        //public class MyButton : ButtonBase
        //{
        //    // Маршрутизируемое событие
        //    public static readonly RoutedEvent ClickEvent;

        //    static MyButton()
        //    {
        //        // Зарегистрировать событие 
        //        MyButton.ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MyButton));
        //    }

        //    // Обертывающее событие .NET (необязательное)
        //    public event RoutedEventHandler Click
        //    {
        //        add { AddHandler(MyButton.ClickEvent, value); }
        //        remove { RemoveHandler(MyButton.ClickEvent, value); }
        //    }

        //    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        //    {
        //        // Сгенерировать событие
        //        RaiseEvent(new RoutedEventArgs(MyButton.ClickEvent, this));
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RoutedEvent window = new RoutedEvent();
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AttachedEvent window = new AttachedEvent();
            window.Show();
        }

        // проверяется, что нажаты клавиши Alt и A, но при этом не исключается нажатие комбинаций Alt+Shift+A, Alt+Ctrl+A и т. д.
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ((e.KeyboardDevice.Modifiers & ModifierKeys.Alt) ==
            ModifierKeys.Alt && (e.Key == Key.A || e.SystemKey == Key.A))
            {
                // Нажато сочетание Alt+A, возможно, с Ctrl, Shift или Windows
            }
            base.OnKeyDown(e);
        }

        // проверяется, что нажата в точности комбинация Alt+A без каких-либо дополнительных модификаторов:
        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt
        //    && (e.Key == Key.A || e.SystemKey == Key.A))
        //    {
        //        // Нажато сочетание Alt+A и только Alt+A
        //    }
        //    base.OnKeyDown(e);
        //}

        // проверяется нажатие комбинации [левая]Alt+A:
        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt
        //    && (e.Key == Key.A || e.SystemKey == Key.A)
        //    && e.KeyboardDevice.IsKeyDown(Key.LeftAlt))
        //    {
        //        // Нажато LeftAlt+A
        //    }
        //    base.OnKeyDown(e);
        //}


        // Маршрутизируемые события
        // с помощью свойств зависимости WPF реализует дополнительную инфраструктуру поверх хорошо известной идеи свойств .NET.
        // WPF еще и дополняет понятие события. Маршрутизируемые события предназначены для работы с деревьями элементов.
        // Сгенерированное маршрутизируемое событие может распространяться вверх или вниз по визуальному и логическому дереву, достигая каждого элемента простым и естественным образом без написания дополнительного кода.
        // Таким образом, внутрь любого элемента, к примеру Button, можно поместить сколь угодно сложное содержимое, но щелчок левой кнопкой мыши при наведении указателя на любой внутренний элемент все равно приведет к возникновению события Click для родительской кнопки.

        // Реализация маршрутизируемого события
        // Напомним, что свойства зависимости представлены открытыми статическими полями типа DependencyProperty с принимаемым по соглашению суффиксом Property. Точно так же маршрутизируемые события представлены открытыми статическими полями типа RoutedEvent с принимаемым по соглашению суффиксом Event.
        // Так же, как свойство зависимости, маршрутизируемое событие регистрируется в статическом конструкторе, и дополнительно определяется обычное событие .NET – обертывающее событие, – чтобы было проще писать процедурный код и добавлять обработчик события в XAML-коде с помощью стандартного синтаксиса атрибутов событий. Как и обертывающее свойство, обертывающее событие не должно делать в аксессорах ничего, кроме вызова методов AddHandler и RemoveHandler. 
        // Методы AddHandler и RemoveHandler наследуются не от класса DependencyObject, а от UIElement. Они соответственно присоединяют и отсоединяют делегат от маршрутизируемого события.

        // Стратегии маршрутизации и обработчики событий
        // В момент регистрации маршрутизируемого события задается одна из трех стратегий маршрутизации – вариантов распространения события по дереву элементов. Стратегии описываются перечислением RoutingStrategy:
        // - Tunneling – событие сначала возникает в корне дерева, а потом опускается вниз по дереву, заново возникая в каждом элементе на пути к источнику, включая его самого (если туннелирование не будет прервано по дороге в результате пометки события как обработанного).
        // - Bubbling – событие сначала возникает в элементе-источнике, а затем поднимается вверх по дереву, заново возникая в каждом элементе на пути к корню, включая сам корень (если всплытие не будет прервано по дороге в результате пометки событиякак обработанного). 
        // - Direct – событие возникает только в элементе-источнике. Точно так же ведут себя обычные события .NET; различие лишь в том, что к маршрутизируемому событию применяются и другие механизмы, в частности триггеры событий.
        // Сигнатуры обработчиков маршрутизируемых событий устроены так же, как сигнатуры всех обработчиков событий в .NET: первый параметр – объект типа System.Object, который обычно называют sender, второй (обычно называемый e) – экземпляр класса, производного от System.EventArgs. Передаваемый обработчику параметр sender – это всегда элемент, к которому присоединен данный обработчик. Параметр e является объектом класса RoutedEventArgs (или производного от него) – подкласса EventArgs, обладающего следующими полезными свойствами:
        // - Source – элемент логического дерева, первоначально сгенерировавший событие.
        // - OriginalSource – элемент визуального дерева, первоначально сгенерировавший событие (например, в случае стандартной кнопки Button это будет дочерний элемент TextBlock или ButtonChrome).
        // - Handled – булевский флаг, которому можно присвоить значение true и тем самым пометить, что событие обработано. Именно таким способом прерывается туннелирование и всплытие.
        // - RoutedEvent – сам объект маршрутизированного события (например, Button.ClickEvent), который может быть полезен для различения событий в случае, когда один и тот же обработчик используется для обработки разных событий.

        // Маршрутизируемые события в действии
        // В классе UIElement определено много маршрутизируемых событий для клавиатуры, мыши, мультисенсорных устройств и стилуса. Большая часть из них всплывающие, но для многих есть и парные туннелируемые. Туннелируемые события легко распознать, потому что по принятому соглашению их имена начинаются со слова Preview.
        // Такое событие – также по соглашению – генерируется непосредственно перед парным ему всплывающим. Например, туннелируемое событие PreviewMouseMove генерируется перед всплывающим событием MouseMove. 
        // Идея, стоящая за такими парами событий, заключается в том, чтобы дать элементам возможность отменить или иным способом модифицировать событие, которое еще только произойдет. По соглашению встроенные в WPF элементы предпринимают действия только в ответ на всплывающее событие (в случае если определена пара событий – всплывающее и туннелируемое), гарантируя тем самым, что туннелируемые события отвечают своему названию (preview означает «предварительный просмотр »).
        // Представим, к примеру, что требуется реализовать элемент TextBox, который позволяет вводить только строки, отвечающие некоторому образцу или регулярному выражению (например, номера телефонов и почтовые индексы). Если обрабатывать в нем событие KeyDown, то лучшее, что можно сделать, – удалить текст, который уже отображен в поле TextBox. Если же обрабатывать событие PreviewKeyDown, то можно пометить его как «обработанное» и тем самым не только прервать туннелирование, но и воспрепятствовать генерации всплывающего события KeyDown. В таком случае TextBox вообще не получит уведомления о событии KeyDown и введенный символ не появится в поле.
        // Если запустить эту программу и последовательно щелкнуть правой кнопкой мыши по всем элементам, то обнаружатся два любопытных эффекта:
        // - Window не получает событие MouseRightButtonDown, если щелкнуть по любому элементу списка ListBoxItem. Дело в том, что ListBoxItem сам обрабатывает это событие, равно как и MouseLeftButtonDown (и прерывает всплытие),– это нужно ему для реализации выбора элементов.
        // - Window получает событие MouseRightButtonDown при щелчке по кнопке Button, но никаких изменений во внешнем виде рамки не происходит. Это объясняется структурой стандартного визуального дерева Button. В отличие от элементов Window, Label, ListBox, ListBoxItem и StatusBar, в визуальном дереве Button нет элемента Border.
        // Прерывание маршрутизации события – иллюзия!
        // Хотя присваивание значения true свойству Handled объекта RoutedEventArgs в обработчике маршрутизируемого события должно приводить к прерыванию туннелирования или всплытия, обработчики, присоединенные к элементам, находящимся выше или ниже в дереве, все равно могут запросить получение событий! Сделать это можно только в процедурном коде с помощью перегруженного варианта метода AddHandler, который принимает дополнительный булевский параметр handledEventsToo.
        // public AboutDialog()
        // {
        // InitializeComponent();
        // this.AddHandler(Window.MouseRightButtonDownEvent,
        // new MouseButtonEventHandler(AboutDialog_MouseRightButtonDown), true);
        // } 
        // Однако лучше не прибегать к этому приему, потому что для пометки события как обработанного, очевидно, была какая-то причина. Более правильно было бы присоединить обработчик Preview-версии того же события. 
        // Но в целом мы хотели подчеркнуть, что прерывание туннелирования и всплытия – на самом деле иллюзия. Распространение события все равно продолжается, но по умолчанию обработчики видят только те события, которые не помечены как уже обработанные. 
        
        // Присоединенные события
        // WPF поддерживает туннелирование и всплытие маршрутизируемых событий даже для элементов, в которых данное событие не определено! Возможно это благодаря понятию присоединенного события.
        // Поскольку в классе Window не определены события SelectionChanged и Click, то имена атрибутов событий необходимо снабдить префиксами, содержащими имя класса, в котором соответствующее событие определено.
        // Любое маршрутизируемое событие можно использовать как присоединенное. Синтаксис присоединенных событий допустим потому, что компилятор XAML видит событие .NET SelectionChanged, определенное в классе ListBox, и событие .NET Click, определенное в классе Button. Однако во время выполнения вызывается метод AddHandler, который присоединяет оба события к элементу Window. Поэтому эти два атрибута события эквивалентны следующему коду в конструкторе Window:
        // this.AddHandler(ListBox.SelectionChangedEvent,
        // new SelectionChangedEventHandler(ListBox_SelectionChanged));
        // this.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));
        // Консолидация обработчиков маршрутизируемых событий
        // Поскольку вместе с маршрутизируемым событием передается достаточно много информации, при желании есть возможность обработать все туннелируемые и всплывающие события в одном «мегаобработчике» на верхнем уровне. Он мог бы исследовать объект RoutedEvent, определить, какое событие сгенерировано, привести параметр RoutedEventArgs к типу соответствующего подкласса (например, KeyEventArgs, MouseButtonEventArgs и т. д.), а потом предпринять соответствующие действия.
        // void GenericHandler(object sender, RoutedEventArgs e)
        // {
        //    if (e.RoutedEvent == Button.ClickEvent)
        //    {
        //       MessageBox.Show("You just clicked " + e.Source);
        //    }
        //    else if (e.RoutedEvent == ListBox.SelectionChangedEvent)
        //    {
        //        SelectionChangedEventArgs sce = (SelectionChangedEventArgs)e;
        //       if (sce.AddedItems.Count > 0)
        //            MessageBox.Show("You just selected " + sce.AddedItems[0]);
        //    }
        // } 
        // Это возможно благодаря встроенному в каркас .NET Framework механизму контравариантности делегатов, позволяющему использовать делегат с методом, в сигнатуре которого указан базовый класс ожидаемого параметра (например, RoutedEventArgs вместо SelectionChangedEventArgs). Метод GenericHandler просто приводит параметр RoutedEventArgs к нужному типу, когда ему необходимо получить дополнительную информацию, специфичную для события SelectionChanged.

        // События клавиатуры
        // Основные события клавиатуры, поддерживаемые всеми подклассами UIElement, – это всплывающие события KeyDown и KeyUp и парные им туннелируемые события PreviewKeyDown и PreviewKeyUp. Обработчикам событий клавиатуры передается аргумент типа KeyEventArgs, содержащий целый ряд свойств, в том числе:
        // - Key, ImeProcessedKey, DeadCharProcessedKey, SystemKey – четыре свойства, принадлежащие типу перечисления Key, в котором определены все возможные клавиши. Свойство Key определяет, какая клавиша вызвала генерацию события. Если клавиша обрабатывается или будет обрабатываться редактором метода ввода (Input Method Editor – IME), то можно проверить значение свойства ImeProcessedKey. Если клавиша является слепой в последовательности, то свойство Key будет равно DeadCharProcessed, тогда как реальную клавишу можно получить из свойства DeadCharProcessedKey. Если нажата системная клавиша, например Alt, то Key будет равно System, а сама клавиша берется из свойства SystemKey.
        // - IsUp, IsDown, IsToggled – булевские свойства, сообщающие дополнительную информацию о событии клавиатуры, хотя в некоторых случаях она избыточна. (Раз уж вы обрабатываете событие KeyDown, то точно знаете, что клавиша нажата!) Свойство IsToggled относится к клавишам с фиксируемым переключением состояния, таким как Caps Lock и Scroll Lock. 
        // Совет
        // Для получения информации о состоянии клавиатуры в любой момент времени, а не только внутри обработчика события от нее можно воспользоваться статическим классом System.Windows.Input и его свойством PrimaryDevice (типа KeyboardDevice). 
        // - KeyStates – свойство типа KeyStates, битового перечисления, состоящего из произвольной комбинации битов None, Down и Toggled. Эти значения отображаются на свойства IsUp, IsDown и IsToggled соответственно. Поскольку Toggled иногда комбинируется с Down, остерегайтесь определять значение KeyStates с помощью простой проверки на равенство. Лучше всего пользоваться свойствами IsXX
        // - IsRepeat – булевское свойство, равное true, когда нажатие клавиши повторяется. Например, так происходит, когда вы удерживаете нажатой пробельную клавишу и получаете лавину событий KeyDown. Свойство IsRepeat будет содержать true для всех событий KeyDown, кроме самого первого. 
        // - KeyboardDevice – свойство типа KeyboardDevice, которое позволяет работать с клавиатурой на более низком уровне, например узнать, какие клавиши сейчас нажаты, или потребовать передать фокус конкретному элементу.
        // Одна из причин для обращения к классу KeyboardDevice – получение его свойства Modifiers типа ModifierKeys (еще одно перечисление). Оно показывает, какие клавиши нажаты одновременно с основной. Возможны следующие значения: None, Alt, Control, Shift и Windows.
        // Как узнать, какая из клавиш Alt, Ctrl и Shift нажата: левая или правая?
        // можно воспользоваться методом IsKeyDown класса KeyboardDevice (или IsKeyUp либо IsKeyToggled), чтобы узнать о состоянии конкретной клавиши, например левой или правой Alt.
        // Элемент UIElement получает события клавиатуры, только если владеет фокусом. Указать, может ли некоторый элемент получать фокус, позволяет булевское свойство Focusable, по умолчанию равное true. При изменении значения этого свойства возникает событие FocusableChanged.
        // В классе UIElement определено еще много свойств и событий, относящихся к фокусу клавиатуры. Отметим из них свойство IsKeyboardFocused, которое сообщает, принадлежит ли фокус текущему элементу, и свойство IsKeyboardFocusWithin, сообщающее то же самое, но в отношении не только текущего элемента, но и его потомков. (Эти свойства доступны только для чтения; чтобы передать фокус клавиатуры, пользуйтесь методами Focus или MoveFocus.) Об изменении этих свойств уведомляют события IsKeyboardFocusedChanged, IsKeyboardFocusWithinChanged, GotKeyboardFocus, LostKeyboardFocus, PreviewGotKeyboardFocus и PreviewLostKeyboardFocus. 

        // События мыши
        // Все подклассы UIElement поддерживают следующие основные события мыши:
        // - MouseEnter и MouseLeave
        // - MouseMove и PreviewMouseMove
        // - MouseLeftButtonDown, MouseRightButtonDown, MouseLeftButtonUp, MouseRightButtonUp и более общие MouseDown и MouseUp, а также Preview-версии всех шести событий
        // - MouseWheel и PreviewMouseWheel
        // События MouseEnter и MouseLeave можно использовать для создания эффекта ролловера, хотя более предпочтительно использовать триггер со свойством IsMouseOver.
        // В подклассах UIElement имеется также свойство IsMouseDirectlyOver (и соответствующее ему событие IsMouseDirectlyOverChanged), которое позволяет исключить дочерние элементы. Оно используется в тех редких случаях, когда вы точно знаете, с каким визуальным деревом работаете.
        // А где же событие для обработки нажатия средней кнопки мыши?
        // Эту информацию можно получить с помощью обобщенных событий MouseDown и MouseUp (или их Preview-версий). Объект EventArgs, передаваемый их обработчиком, содержит свойства, показывающие, какая их следующих кнопок была нажата или отпущена: LeftButton, RightButton, MiddleButton, XButton1 или XButton2. 
        // Совет
        // Если вы не хотите, чтобы элемент генерировал события мыши (или блокировал события мыши, генерируемые лежащими под ним элементами), то можете присвоить значение false его свойству IsHitTestVisible.
        // Предупреждение
        // Прозрачные области генерируют события мыши, но null-области – нет!
        // Хотя и можно рассчитывать на то, что установка для свойства IsHitTestVisible значения false подавит события мыши, но условия, при которых эти события вообще генерируются, довольно запутанны. Если свойство Visibility элемента равно Collapsed, то событиямыши  подавляются, но установка для свойства Opacity значения 0 не влияет на генерацию событий. Еще одна тонкость касается областей, для которых любое из свойств Background, Fill или Stroke равно null.
        // В таких областях события мыши не генерируются. Однако, если явно присвоить любому из свойств Background, Fill или Stroke значение Transparent (или любой другой цвет), то в такой области события мыши будут генерироваться. (null- кисть внешне неотличима от прозрачной (Transparent) кисти, но с точки зрения проверки положения указателя мыши ведет себя иначе.)

        // Класс MouseEventArgs
        // Обработчикам всех вышеупомянутых событий мыши (кроме IsMouseDirectlyOverChanged) передается объект класса MouseEventArgs. В нем есть пять свойств типа MouseButtonState, содержащих информацию обо всех потенциально возможных нажатиях кнопок мыши: LeftButton, RightButton, MiddleButton, XButton1 и XButton2. MouseButtonState – это перечисление с двумя значениями: Pressed и Released. В классе MouseEventArgs определен также метод GetPosition, который возвращает структуру Point со свойствами X и Y, отражающими точные координаты указателя мыши.
        // GetPosition – это метод, а не просто свойство, поскольку он позволяет получить позицию указателя мыши несколькими способами: относительно левого верхнего угла экрана или левого верхнего угла произвольного нарисованного элемента UIElement. Чтобы узнать координаты относительно экрана, передайте в качестве единственного параметра null. А для получения координат относительно элемента передайте в качестве параметра интересующий вас элемент.
        // Обработчикам событий MouseWheel и PreviewMouseWheel передается объект класса MouseWheelEventArgs, производного от MouseEventArgs. Этот класс добавляет целочисленное свойство Delta, показывающее, на какой угол колесико мыши повернулось с момента последнего события. Обработчикам всех 12 событий семейства MouseUp/MouseDown передается объект класса MouseButtonEventArgs, еще одного подкласса MouseEventArgs. Этот класс добавляет свойство ChangedButton, которое сообщает, какая кнопка изменила состояние (значение принадлежит перечислению MouseButton); свойство ButtonState, которое информирует, нажата кнопка или отпущена; и свойство ClickCount.
        // Свойство ClickCount показывает, сколько раз подряд была нажата кнопка мыши, причем ведется подсчет нажатий, промежуток времени между которыми не превышает системного параметра, описывающего скорость выполнения двойного щелчка (задается на Панели управления). Класс Button генерирует событие Click, обрабатывая низкоуровневое событие MouseLeftButtonDown, а его базовый класс Control генерирует событие MouseDoubleClick, сравнивая значение ClickCount с 2 в обработчике MouseLeftButtonDown, и событие PreviewMouseDoubleClick, делая то же самое в обработчике PreviewMouseLeftButtonDown. Имея такую поддержку, вы легко сможете реагировать и на другие действия пользователя, например на тройное нажатие, двойное нажатие средней кнопки и т. д.
        // Предупреждение
        // Панель Canvas генерирует свои собственные события мыши только в области, определяемой ее свойствами Width и Height!
        // по умолчанию события мыши уровня Canvas генерируются только ее дочерними элементами.

        // Перетаскивание
        // Во всех подклассах UIElement определены события для работы с перетаскиванием:
        // - DragEnter, DragOver, DragLeave, а также PreviewDragEnter, PreviewDragOver и PreviewDragLeave
        // - Drop и PreviewDrop
        // - QueryContinueDrag и PreviewQueryContinueDrag(209)





        // !!!
        // - DependencyProperty
        // - RoutedEvent
    }
}
