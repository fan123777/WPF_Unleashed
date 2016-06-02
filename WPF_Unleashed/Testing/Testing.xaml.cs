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

namespace WPF_Unleashed.Testing
{
    /// <summary>
    /// Interaction logic for Testing.xaml
    /// </summary>
    public partial class Testing : Window
    {
        public Testing()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Panel.Panels window = new Panel.Panels();
            window.Show();
        }

        // 5 Панели
        // StackPanel
        // В отсутствие присоединенных свойств единственный способ организовать дочерние элементы – воспользоваться свойством панели Orientation

        // WrapPanel
        // Панель WrapPanel похожа на StackPanel. Но помимо организации дочерних элементов в стопку она создает новые строки или столбцы, когда для одной стопки не хватает места.
        // В классе WrapPanel определены три свойства, контролирующие его поведение:
        // - Orientation – аналогично одноименному свойству StackPanel с тем отличием, что по умолчанию подразумевается значение Horizontal.
        // - ItemHeight – единая высота для всех дочерних элементов. Каким образом каждый потомок распоряжается этой высотой, зависит от значений его свойств VerticalAlignment, Height и пр. Элементы, ширина которых превышает ItemHeight, отсекаются.
        // - ItemWidth – единая ширина для всех дочерних элементов. Каким образом каждый потомок распоряжается этой шириной, зависит от значений его свойств HorizontalAlignment, Width и пр. Элементы, высота которых превышает ItemWidth, отсекаются.

        // DockPanel
        // Панель DockPanel дает простой способ пристыковки элемента к одной из сторон, растягивая его на всю имеющуюся ширину или высоту.
        // В классе DockPanel определено присоединенное свойство Dock, с помощью которого дочерние элементы могут управлять своим положением.
        // Оно может принимать четыре значения: Left (подразумевается по умолчанию, если свойство Dock не задано явно), Top, Right и Bottom.
        // Отметим, что у свойства Dock нет значения Fill, означающего, что нужно заполнить оставшееся место.
        // Вместо этого действует соглашение о том, что все оставшееся место отдается последнему дочернему элементу, добавленному в DockPanel, если только свойство LastChildFill не равно false.

        // Grid
        // По умолчанию свойства RowSpan и ColumnSpan равны 1, но могут принимать любое значение, большее или равное 1, – соответственно количество строк или столбцов, занимаемых данной ячейкой.
        // Такой автоматический выбор размера достигается путем присваивания свойствам Height и Width соответственно в элементах RowDefinition и ColumnDefinition специального значения Auto, нечувствительного к регистру букв.
        // панель Grid поддерживает три способа задания размера в элементах RowDefinition и ColumnDefinition:
        // - Абсолютный размер – числовое значение Height или Width означает, что размер задан в независимых от устройства пикселах
        // - Автоматический выбор размера – если Height или Width равно Auto, то дочерним элементам выделяется столько места, сколько необходимо, но не больше.
        // - Пропорциональное изменение размера – предусмотрен специальный синтаксис задания свойств Height и Width, позволяющий распределить имеющееся пространство поровну или в соответствии с заданными пропорциями 
        // Звездочка работает следующим образом:
        // - Если высота строки или ширина столбца равна *, то соответствующему структурному элементу выделяется все оставшееся место.
        // - Если размер * задан для нескольких строк или столбцов, то все оставшееся место делится между ними поровну.
        // - Перед символом * можно указывать коэффициент (например, 2* или 5.5*), тогда соответствующей строке или столбцу будет выделено пропорционально больше места, чем остальным строкам или столбцам, в размере которых присутствует символ *.
        // Интерактивное задание размера с помощью GridSplitter
        // В классах RowDefinitions и ColumnDefinitions имеется свойство SharedSizeGroup, позволяющее задать режим, при котором линейные размеры нескольких строк и/или столбцов будут оставаться одинаковыми даже в случае, когда размер любой из них изменяется в процессе выполнения программы
        // Свойство IsSharedSizeScope следует установить потому, что группы размеров могут применяться сразу к нескольким сеткам!

        // Обработка переполнения содержимого
        // Для ее разрешения можно применять различные стратегии:
        // • отсечение
        // • прокрутку
        // • масштабирование
        // • оборачивание
        // • обрезку

        // Отсечение
        // Отсечение дочерних элементов – это тот способ, который панели применяют по умолчанию, когда потомков становится слишком много.
        // Во всех классах, производных от UIElement, есть булевское свойство ClipToBounds, которое управляет тем, можно ли рисовать дочерние элементы вне границ родителя.
        // Однако, если внешний край элемента совпадает с внешним краем Window или Page, отсечение все равно производится.

        // Прокрутка
        // стоит поместить элемент внутрь элемента управления System.Windows.Controls.ScrollViewer, как он сразу же становится прокручиваемым.
        // В классе ScrollViewer имеется еще ряд свойств и методов для манипулирования из программы, но самыми важными являются свойства VerticalScrollBarVisibility и HorizontalScrollBarVisibility.
        // Оба они имеют тип перечисления ScrollBarVisibility, которое определяет четыре состояния полосы прокрутки:
        // - Visible – полоса прокрутки всегда присутствует, даже если она не нужна. Если необходимости в ней нет, то она выглядит неактивной и не реагирует на события ввода.
        // - Auto – полоса прокрутки видна, если содержимое нуждается в прокрутке в данном направлении. В противном случае полоса прокрутки отсутствует.
        // - Hidden – полоса прокрутки всегда невидима, но логически существует, то есть содержимое можно прокручивать клавишами со стрелками. Поэтому содержимое полностью доступно в данном направлении.
        // - Disabled – полоса прокрутки не только невидима, но и вообще не существует, то есть прокрутка невозможна ни с помощью клавиатуры, ни посредством мыши. В таком случае доступна только та часть содержимого, которая видна в пределах родителя.
        
        // Масштабирование
        // Класс Viewbox относится к так называемым декораторам, то есть панелеподобным классам, у которых может быть только один дочерний элемент.
        // Но у него также имеется свойство Stretch, позволяющее указать, как в занимаемой области должен масштабироваться единственный дочерний элемент.
        // Это свойство имеет тип перечисления System.Windows.Media.Stretch и может принимать следующие значения:
        // - None – масштабирование не производится вовсе. Результат такой же, как если бы Viewbox вообще не было.
        // - Fill – размеры дочернего элемента устанавливаются такими же, как размеры самого Viewbox. Поэтому отношение сторон дочернего элемента может не сохраняться.
        // - Uniform – дочерний элемент масштабируется так, чтобы он целиком поместился внутри Viewbox с сохранением отношения сторон. Поэтому, если отношения сторон Viewbox и дочернего элемента не совпадают, то в одном направлении останется пустое место. Этот вариант подразумевается по умолчанию.
        // - UniformToFill – дочерний элемент масштабируется так, чтобы он целиком заполнял Viewbox с сохранением отношения сторон. Поэтому, если отношения сторон Viewbox и дочернего элемента не совпадают, то в одном направлении содержимое будет отсечено.
        // Второе свойство Viewbox позволяет указать, какие операции разрешены: только уменьшение содержимого, только увеличение или и то и другое.
        // Оно называется StretchDirection, имеет тип перечисления System.Windows.Controls.StretchDirection и может принимать следующие значения:
        // - UpOnly – увеличивает содержимое, если необходимо. Если содержимое уже слишком велико, то Viewbox оставляет текущий размер без изменения.
        // - DownOnly – уменьшает содержимое, если необходимо. Если содержимое уже достаточно мало, то Viewbox оставляет текущий размер без изменения.
        // - Both – увеличивает или уменьшает содержимое в соответствии с заданным значением описанного выше свойства Stretch. Этот вариант подразумевается по умолчанию.

        // Все вместе: создание сворачиваемой, стыкуемой, изменяющей размер панели
        // Все три сетки помещены (куда бы вы думали?) в сетку с одной строкой и одним столбцом, чтобы они могли перекрывать друг друга и вместе с тем растягиваться, занимая все отведенное им место.
        // Z-порядок слоя 0 всегда наименьший, но Z-порядок двух остальных слоев может меняться так, чтобы текущая непристыкованная панель всегда была наверху.
        //

    }
}
