   M S B A M L   `   `   `  ���� M  ISkillProfi.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SkillProfi.Desktop.ViewSkillProfi.Desktop.View  1    +SkillProfi.Desktop.View.CreateRequestWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7local%clr-namespace:SkillProfi.Desktop.View       � WindowStartupLocation$  CenterScreen=� � Title$# Создание заявки��$	��450��$	��500�� � 
ResizeMode$ NoResize=�.���� $#��Описание заявки��$
��10pxq�.���  � RowDefinitions �� $��* ��� $
��30px �.+��� $��0�� �� Orientation$ Vertical=�.+��� $ 
Horizontal=�$��0 5pxq�.+��� $��Имя��$
��14px(�$��120px���� -    ��Name$ tbName��$
��14px(�$��330px���� $ 
Horizontal=�$��0 5pxq�.+��� $��Контакты��$
��14px(�$��120px���� -   $ tbEmail��$
��14px(�$��330px���� $ Vertical=�$��0 5pxq�$
��Left=�.+��� $��Текст заявки��$
��14px(�$��120px��$
��Left=��� -   $ 	tbMessage��$
��14px(�$	��5pxq� �� TextWrapping$
 Wrap=�$��450px�� �� MaxLines$ 13���� $��1��$ 
Horizontal=�$��60px 0q�.+��� -    ��Name$ btnSave��$��Отправить��$��100px��$
��25px��$
��14px(�$��
0 0 30px 0q��� -   $��
Выход��$��100px��$
��25px��$
��14px(�