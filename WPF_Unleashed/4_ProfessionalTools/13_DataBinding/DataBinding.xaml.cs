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

namespace WPF_Unleashed._4_ProfessionalTools._13_DataBinding
{
    /// <summary>
    /// Interaction logic for DataBinding.xaml
    /// </summary>
    public partial class DataBinding : Window
    {
        public DataBinding()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsingBinding window = new UsingBinding();
            window.Show();
        }
    }

    // Привязка к данным
    // WPF термин данные обычно употребляется для описания произвольного объекта .NET.
    // Примерами такого соглашения могут служить словосочетания привязка к данным, шаблоны данных и триггеры данных.
    // В роли данных может выступать объект-коллекция, XML-файл, веб-служба, таблица базы данных, объект написанного вами класса и даже элемент WPF, к примеру Button.
    // Классический пример – визуальное представление (например, в виде списка ListBox или сетки DataGrid) данных, хранящихся в XML-файле, в базе данных или в коллекции в памяти.
    
    // Знакомство с объектом Binding
    // Ключом к механизму привязки к данным является объект класса System.Windows.Data.Binding, который «склеивает» между собой два свойства и открывает коммуникационный канал между ними.
    // Объект Binding можно один раз настроить и поручить ему дальнейшую заботу о синхронизации.

    // Использование объекта Binding в процедурном коде
    // Для объекта Binding определены понятия свойства-источника и свойства-приемника.
    // Свойство-источник (в нашем случае treeView.SelectedItem.Header) задается в два приема:
    // запись ссылки на объект-источник в свойство Source и имени интересующего нас свойства (или цепочки, состоящей из имени свойства и его «субсвойств»), обернутого объектом PropertyPath, в свойство Path.
    // Чтобы ассоциировать Binding со свойством-приемником (в нашем случае currentFolder.Text) Binding, нужно вызвать метод SetBinding (унаследованный от класса FrameworkElement или FrameworkContentElement), передав ему нужное свойство зависимости и сам объект Binding.
    // СОВЕТ
    // На самом деле существует два способа настроить объект Binding в процедурном коде.
    // Первый – вызвать метод экземпляра SetBinding от имени объекта FrameworkElement или FrameworkContentElement, как было показано выше.
    // Второй – вызвать статический метод SetBinding класса BindingOperations. Ему передаются те же аргументы, что и методу экземпляра, плюс дополнительный первый аргумент, представляющий объект-приемник:
    // BindingOperations.SetBinding(currentFolder, TextBlock.TextProperty, binding);
    // Достоинство статического метода в том, что первый параметр имеет тип DependencyObject, то есть открывается возможность осуществлять привязку к объектам, не наследующим ни FrameworkElement, ни FrameworkContentElement (например, класса Freezable).
    // КОПНЕМ ГЛУБЖЕ
    // Удаление объекта Binding
    // Если вы не хотите, чтобы объект Binding существовал на протяжении всего времени работы приложения, то его можно в любой момент «отключить» с помощью статического метода BindingOperations.ClearBinding.
    // Методу передается объект-приемник и его свойство зависимости.
    // BindingOperations.ClearBinding(currentFolder, TextBlock.TextProperty);
    // Если к объекту-приемнику присоединено более одного объекта Binding, то можно очистить их все за один прием, вызвав метод BindingOperations.ClearAllBindings:
    // BindingOperations.ClearAllBindings(currentFolder);
    // Еще один способ убрать привязку – напрямую записать в свойство-приемник новое значение, например:
    // currentFolder.Text = "I am no longer receiving updates.";
    // Но такой способ очищает только односторонние привязки.
    // Подход на основе метода ClearBinding в любом случае более гибкий, поскольку оставляет свойству зависимости возможность принимать данные из других источников с более низким приоритетом (триггеров стилей, механизма наследования значений свойств и т. д.).
    
    // Использование объекта Binding в XAML
    // СОВЕТ
    // Помимо конструктора по умолчанию в классе Binding имеется конструктор, принимающий один аргумент Path.
    // Следовательно, можно оформить расширение разметки и по-другому, передав путь Path конструктору, а не устанавливая его явно в качестве свойства.
    // Иными словами, приведенный выше фрагмент XAML можно было бы переписать и в таком виде:
    // Text="{Binding SelectedItem.Header, ElementName=treeView}"
    // Впрочем, с появлением в WPF 4 расширения разметки x:Reference свойство Source можно задать следующим образом:
    // Text="{Binding Source={x:Reference TreeView}, Path=SelectedItem.Header}"
    // СОВЕТ
    // С помощью свойства TargetNullValue объекта Binding можно указать специальное значение, которое будет возвращаться, если значение реального свойства-источника равно null.
    // Например, в показанном ниже текстовом блоке будет отображаться не пустая строка, а строка «Nothing is selected.» (Ничегоне выбрано) в случае, когда значение-источник равно null:
    // <TextBlock Text="{Binding ... TargetNullValue=Nothing is selected.}" .../>
    // КОПНЕМ ГЛУБЖЕ
    // Свойство RelativeSource объекта Binding
    // Чтобы сделать элемент-источник равным элементу-приемнику:
    // {Binding RelativeSource={RelativeSource Self}}
    // Чтобы сделать элемент-источник равным свойству TemplatedParent элемента-приемника
    // {Binding RelativeSource={RelativeSource TemplatedParent}}
    // Чтобы сделать элемент-источник равным ближайшему родителю элемента-приемника, имеющему заданный тип:
    // {Binding RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type desiredType}}}
    // Чтобы сделать элемент- источник равным n-му ближайшему родителю элемента-приемника, имеющему заданный тип:
    // {Binding RelativeSource={RelativeSource FindAncestor, AncestorLevel=n, AncestorType={x:Type desiredType}}}
    // Чтобы сделать элемент-источник равным предыдущему объекту в коллекции, привязанной к данным:
    // {Binding RelativeSource={RelativeSource PreviousData}}
    // Особенно полезно свойство RelativeSource в контексте шаблонов элементов управления, которые мы будем обсуждать в следующей главе.
    // Однако использование RelativeSource в режиме Self удобно для привязки одного свойства элемента к другому его же свойству без указания имени элемента.
    
    // Привязка к обычным свойствам .NET
    // Однако на пути использования обычного свойства .NET в качестве источника привязки нас подстерегает неприятность.
    // Поскольку в таких свойствах не предусмотренысредства уведомления об изменениях, то приемник не будет автоматически синхронизироваться с источником – для этого нужна дополнительная работа.
    // Чтобы синхронизировать источник с приемником, объект- источник должен сделать одно из двух:
    // - Реализовать интерфейс System.ComponentModel.INotifyPropertyChanged, в котором определено единственное событие PropertyChanged.
    // - Реализовать событие XXXChanged, где XXX – имя свойства, значение которого изменяется.
    // Рекомендуется первое решение, поскольку WPF оптимизирована под него. 
    // Таким образом, для синхронизации привязки к photos.Count достаточно заменить в исходном коде строку 
    // public class Photos : Collection<Photo>
    // на такую
    // public class Photos : ObservableCollection<Photo>

    // КОПНЕМ ГЛУБЖЕ
    // Как работает привязка к обычным свойствам .NET
    // Чтобы получить значение свойства-источника, которое является обычным свойством .NET, WPF применяет отражение.
    // ПРЕДУПРЕЖДЕНИЕ
    // Источники и приемники данных обрабатываются по-разному!
    // Свойством-источником действительно может быть любое свойство любого объекта .NET, однако для свойства-приемника это уже не так.
    // Свойство-приемник обязано быть свойством зависимости.
    // Отметим также, что источник должен быть настоящим (и притом открытым) свойством, а не просто полем.

    // Привязка ко всему объекту
    // Можно привязать свойство-приемник ко всему объекту.
    // СОВЕТ
    // Привязка ко всему объекту удобна, когда нужно в XAML-коде задать некое свойство, требующее объекта, который нельзя получить ни с помощью конвертера типа, ни посредством расширения разметки.
    // ПРЕДУПРЕЖДЕНИЕ
    // Будьте осторожны с привязкой к объекту типа UIElement!
    // Привязывая некоторые свойства-приемники ко всему объекту UIElement, вы можете, сами того не желая, попытаться поместить один и тот же элемент в разные места визуального дерева.
    // Привязка к коллекции
    // Непосредственная привязка
    // Однако и ListBox, и все прочие многодетные элементы управления имеют свойство зависимости ItemsSource, специально предназначенное для привязки к данным.
    // <ListBox x:Name="pictureBox"
    // ItemsSource="{Binding Source={StaticResource photos}}" ...>
    // ...
    // </ListBox>
    // Чтобы свойство-приемник синхронизировалось с изменениями в коллекции-источнике (то есть отражало добавление и удаление объектов), коллекция-источник должна реализовывать интерфейс INotifyCollectionChanged.
    // Улучшение отображения
    // Один из способов навести порядок – воспользоваться свойством DisplayMemberPath, которое имеется у всех многодетных элементов управления.
    // Оно работает рука об руку со свойством ItemsSource.
    // <ListBox x:Name="pictureBox" DisplayMemberPath="Name"
    // ItemsSource="{Binding Source={StaticResource photos}}" ...>
    // ...
    // </ListBox>
    // ПРЕДУПРЕЖДЕНИЕ
    // Свойства Items и ItemsSource объекта ItemsControl нельзя модифицировать одновременно!
    // Вы должны заранее решить, как будете заполнять многодетный элемент управления: вручную с помощью свойства Items или путем привязки к данным с помощью свойства ItemsSource, потому что смешивать эти два приема нельзя.
    // Отметим, что вне зависимости от того, каким способом объекты помещаются в многодетный элемент управления, обращаться к ним для чтения всегда разрешается с помощью коллекции Items.

    // Управление выбранным объектом
    // Чтобы включить рассматриваемую поддержку, присвойте значение true свойству IsSynchronizedWithCurrentItem
    
    // ПРЕДУПРЕЖДЕНИЕ
    // Свойство IsSynchronizedWithCurrentItem не поддерживает множественный выбор!
    // Если в селекторе Selector выбрано несколько объектов (как в случае, когда свойство SelectionMode элемента ListBox равно Multiple или Extended), то все остальные синхронизированные с ним элементы увидят только первый выбранный объект, даже если сами поддерживают множественный выбор!
    
    // Обобществление источника с помощью DataContext
    // 428


    // !!!
    // - bind to a property
    // - bind to array
}
